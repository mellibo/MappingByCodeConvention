namespace SampleModel.Entities
{
    using System;
    using System.Collections.Generic;

    using NHibernate.Validator.Constraints;

    public class Comment : Entity
    {
        public virtual string NativeId { get; set; }

        [Length(300)]
        public virtual string Message { get; set; }

        public virtual Post Post { get; set; }

        public virtual Person Person { get; set; }
    
        public virtual DateTime PublishedTime { get; set; }

        public virtual DateTime UpdatedTime { get; set; }

        public virtual Campaign Campaign { get; set; }

        public virtual IEnumerable<Tag> Tags { get; set; }
    }
}