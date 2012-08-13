namespace MappingByCodeConvention.Test
{
    using System;
    using System.Linq;

    using ARSoft.NH.MappingByCodeConvention;

    using NHibernate;
    using NHibernate.Cfg.MappingSchema;
    using NHibernate.Mapping.ByCode;

    using NUnit.Framework;

    using SampleModel.Entities;

    using SampleModelMappings.Properties;

    using SharpTestsEx;

    [TestFixture]
    public class ModelMappingFixture
    {
        private ConventionModelMapper mapper;

        private HbmMapping hbms;

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
        }
    }
}