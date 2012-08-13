namespace SampleModel.Entities
{
    using System.Collections.Generic;

    public class CommentRuleFromPersonList : CommentRuleBase
    {
        public virtual IEnumerable<Person> Persons { get; set; }

        public override bool Math(Comment comment)
        {
            throw new System.NotImplementedException();
        }
    }
}