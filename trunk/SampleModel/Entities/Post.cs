namespace SampleModel.Entities
{
    using System;
    using System.Collections.Generic;

    using ARSoft.NH.MappingByCodeConvention;

    public class Post : Entity
    {
        public virtual Page Page { get; set; }
        
        public virtual IEnumerable<Comment> Comments { get; set; }

        public virtual Person From { get; set; }
        
        public virtual Person To { get; set; }

        [Length(Length = 400)]
        public virtual string Message { get; set; }

        public virtual string Caption { get; set; }

        public virtual DateTime PublishedTime { get; set; }

        public virtual DateTime UpdatedTime { get; set; }

        public virtual int Likes { get; set; }

        public virtual string Type { get; set; }

        public virtual string Description { get; set; }
    }
}