namespace MappingByCodeConvention.Test
{
    using System;
    using System.Linq;

    using ARSoft.NH.MappingByCodeConvention;

    using NHibernate;
    using NHibernate.Cfg;
    using NHibernate.Cfg.MappingSchema;
    using NHibernate.Mapping.ByCode;
    using NHibernate.Tool.hbm2ddl;

    using NUnit.Framework;

    using SampleModel.Entities;

    using SampleModelMappings.Properties;

    using SharpTestsEx;

    [TestFixture]
    public class ModelMappingFixture
    {
        private ConventionModelMapper mapper;

        private HbmMapping hbms;

        private class Entidad
        {
            public int Id { get; set; }
        }

        [TestFixtureSetUp]
        public void Setup()
        {
            this.mapper = new ConventionModelMapper();
            var mappingTypes = typeof(ComentarioMapping).Assembly.GetTypes().Where(x => x.Name.EndsWith("Mapping"));
            this.mapper.AddMappings(mappingTypes);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            Console.WriteLine(this.hbms.AsString());
            var config = new Configuration().Configure("hibernate.cfg.xml");
            config.AddMapping(this.hbms);
            var schemaExport = new SchemaExport(config);
            schemaExport.SetOutputFile("db.sql");
            schemaExport.Create(true, true);
            var sf = config.BuildSessionFactory();
            sf.Dispose();
        }

        [Test]
        public void AllIdNamedPOIDAndHilo()
        {
            // arrange
            this.mapper.BeforeMapClass += IdConventions.AllIdNamedPOIDAndHilo;
            
            // act
            this.hbms = this.mapper.CompileMappingForAllExplicitlyAddedEntities();

            // assert
            foreach (HbmClass hbm in this.hbms.Items)
            {
                hbm.Id.Columns.Count().Should().Be.EqualTo(1);
                hbm.Id.Columns.ToArray()[0].name.Should().Be.EqualTo("POID");
                hbm.Id.generator.@class.Should().Be.EqualTo("hilo");
            }
        }

        [Test]
        public void AllIdIdentity()
        {
            // arrange
            this.mapper.BeforeMapClass += IdConventions.AllIdIdentity;

            // act
            this.hbms = this.mapper.CompileMappingFor(new[] { typeof(Entidad) });

            // assert
            foreach (HbmClass hbm in this.hbms.Items)
            {
                hbm.Id.generator.@class.Should().Be.EqualTo("identity");
            }
        }

        [Test]
        public void AllIdHilo()
        {
            // arrange
            this.mapper.BeforeMapClass += IdConventions.AllIdHilo;

            // act
            this.hbms = this.mapper.CompileMappingFor(new[] { typeof(Entidad) });

            // assert
            foreach (HbmClass hbm in this.hbms.Items)
            {
                hbm.Id.generator.Should().Not.Be.Null();
                hbm.Id.generator.@class.Should().Be.EqualTo("hilo");
            }
        }

        [Test]
        public void AllTableNameEnglishPluralized()
        {
            // arrange
            this.mapper.BeforeMapClass += EntityConventions.TableNameEnglishPluralizedConvention;
            EntityConventions.ExcludeBaseEntity(this.mapper, typeof(Entity));

            // act
            this.hbms = this.mapper.CompileMappingFor(new[] { typeof(Comment), typeof(Person), typeof(Category) });

            // assert
            this.hbms.Items.OfType<HbmClass>().Count(x => x.table == "Comments").Should().Be.EqualTo(1);
            this.hbms.Items.OfType<HbmClass>().Count(x => x.table == "People").Should().Be.EqualTo(1);
            this.hbms.Items.OfType<HbmClass>().Count(x => x.table == "Categories").Should().Be.EqualTo(1);
        }

        [Test]
        public void AllBagWithCascadeAll()
        {
            // arrange
            this.mapper.BeforeMapBag += EntityConventions.MapBagsWithCascadeAll;
            EntityConventions.ExcludeBaseEntity(this.mapper, typeof(Entity));

            // act
            this.hbms = this.mapper.CompileMappingFor(new[] { typeof(Comment), typeof(Post) });

            // assert
            var postMap = this.hbms.Items.OfType<HbmClass>().First(x => x.Name == "Post");
            postMap.Items.OfType<HbmBag>().First().Cascade.Should().Be.EqualTo("all");
        }

        [Test]
        public void AllBagWithCascadeSaveUpdate()
        {
            // arrange
            this.mapper.BeforeMapBag += EntityConventions.MapBagsWithCascadePersist;
            EntityConventions.ExcludeBaseEntity(this.mapper, typeof(Entity));

            // act
            this.hbms = this.mapper.CompileMappingFor(new[] { typeof(Comment), typeof(Post) });

            // assert
            var postMap = this.hbms.Items.OfType<HbmClass>().First(x => x.Name == "Post");
            postMap.Items.OfType<HbmBag>().First().Cascade.Should().Be.EqualTo("save-update, persist");
        }

        [Test]
        public void AllSetWithCascadeSaveUpdate()
        {
            // arrange
            this.mapper.BeforeMapSet += EntityConventions.MapSetWithCascadePersist;
            EntityConventions.ExcludeBaseEntity(this.mapper, typeof(Entity));

            // act
            this.hbms = this.mapper.CompileMappingFor(new[] { typeof(Comment), typeof(Post), typeof(Page) });

            // assert
            var postMap = this.hbms.Items.OfType<HbmClass>().First(x => x.Name == "Page");
            postMap.Items.OfType<HbmSet>().First().Cascade.Should().Be.EqualTo("save-update, persist");
        }

        [Test]
        public void AllManyToOneCascade()
        {
            // arrange
            this.mapper.BeforeMapManyToOne += EntityConventions.MapManyToOneWithCascade;
            EntityConventions.ExcludeBaseEntity(this.mapper, typeof(Entity));

            // act
            this.hbms = this.mapper.CompileMappingFor(new[] { typeof(Comment), typeof(Post), typeof(Page) });

            // assert
            var postMap = this.hbms.Items.OfType<HbmClass>().First(x => x.Name == "Post");
            postMap.Items.OfType<HbmManyToOne>().First().cascade.Should().Be.EqualTo("save-update, persist");
        }

        [Test]
        public void ExcludeBaseEntityConvention()
        {
            // arrange
            EntityConventions.ExcludeBaseEntity(this.mapper, typeof(Entity));

            // act
            this.hbms = this.mapper.CompileMappingFor(new[] { typeof(Post) });

            // assert
            this.hbms.Items.First().Should().Be.OfType<HbmClass>();
        }

        [Test]
        public void MapStringAsVarcharConvention()
        {
            // arrange
            EntityConventions.ExcludeBaseEntity(this.mapper, typeof(Entity));
            this.mapper.BeforeMapProperty += PropertiesConvention.MapStringAsVarchar;

            // act
            this.hbms = this.mapper.CompileMappingFor(new[] { typeof(Post) });

            // assert
            this.hbms.Items.OfType<HbmClass>().First().Items.OfType<HbmProperty>().First(x => x.Name == "Message").Type.
                name.Should().Be.EqualTo("AnsiString");
        }

        [Test]
        public void MapStringLengthConventionFromCustomAttribute()
        {
            // arrange
            EntityConventions.ExcludeBaseEntity(this.mapper, typeof(Entity));
            this.mapper.BeforeMapProperty += PropertiesConvention.MapStringLengthFromAttribute;

            // act
            this.hbms = this.mapper.CompileMappingFor(new[] { typeof(Post) });

            // assert
            this.hbms.Items.OfType<HbmClass>().First().Items.OfType<HbmProperty>().First(x => x.Name == "Message").length.Should().Be.EqualTo("400");
        }

        [Test]
        public void MapStringLengthConventionFromDataAnnotation()
        {
            // arrange
            EntityConventions.ExcludeBaseEntity(this.mapper, typeof(Entity));
            this.mapper.BeforeMapProperty += PropertiesConvention.MapStringLengthFromAttribute;

            // act
            this.hbms = this.mapper.CompileMappingFor(new[] { typeof(Post) });

            // assert
            this.hbms.Items.OfType<HbmClass>().First().Items.OfType<HbmProperty>().First(x => x.Name == "Caption").length.Should().Be.EqualTo("100");
        }

        [Test]
        public void MapStringLengthConventionFromNHValidator()
        {
            // arrange
            EntityConventions.ExcludeBaseEntity(this.mapper, typeof(Entity));
            this.mapper.BeforeMapProperty += PropertiesConvention.MapStringLengthFromAttribute;

            // act
            this.hbms = this.mapper.CompileMappingFor(new[] { typeof(Comment) });

            // assert
            this.hbms.Items.OfType<HbmClass>().First().Items.OfType<HbmProperty>().First(x => x.Name == "Message").length.Should().Be.EqualTo("300");
        }
    }
}