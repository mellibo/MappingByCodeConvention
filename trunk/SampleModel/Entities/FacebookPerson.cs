namespace SampleModel.Entities
{
    public class FacebookPerson : Person, IFacebookPerson
    {
        public virtual string FacebookNick { get; set; }

        public virtual string FacebookId { get; set; }

        public virtual string FakeEmail { get; set; }

        public virtual string ProfileUrl { get; set; }

        public virtual string WallUrl { get; set; }
    }
}