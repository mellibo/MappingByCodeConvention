namespace SampleModel.Entities
{
    using System.Collections.Generic;

    public class Page: Entity
    {
        public virtual string URL { get; set; }

        public virtual string Description { get; set; }

        public virtual bool Active { get; set; }

        public virtual Iesi.Collections.Generic.ISet<Post> Posts { get; set; } 
    }
}