namespace SampleModel.Entities
{
    public abstract class CommentRuleWordsBase : CommentRuleBase
    {
        public virtual string ComaDelimitedWords { get; set; }
    }
}