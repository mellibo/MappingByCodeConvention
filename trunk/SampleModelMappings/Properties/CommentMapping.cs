namespace SampleModelMappings.Properties
{
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;
    using NHibernate.Type;

    using SampleModel.Entities;

    public class CommentMapping : ClassMapping<Comment>
    {
        public CommentMapping()
        {
            Property(x => x.Message, x => x.Column(y => y.Length(400)));
        }
    }
}