namespace SampleModel.Entities
{
    using System.Collections.Generic;

    public class Page
    {
        public virtual Customer Customer { get; set; }

        public virtual string URL { get; set; }

        public virtual string Description { get; set; }

        public virtual bool Active { get; set; }

        public virtual IEnumerable<CommentRuleBase> Rules { get; set; }

        public virtual Campaign DefaultCampaign { get; set; }
    }
}