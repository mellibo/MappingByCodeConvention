namespace SampleModel.Entities
{
    public abstract class CommentRuleBase
    {
        public virtual Tag AssignTagOnMath { get; set; }

        public virtual Campaign AssignCampaignOnMath { get; set; }

        public abstract bool Math(Comment comment);

        public virtual Page Page { get; set; }

        public virtual bool Active { get; set; }
    }
}