namespace SampleModel.Entities
{
    public interface IFacebookPerson : IPerson
    {
        string FacebookNick { get; set; }

        string FacebookId { get; set; }

        string FakeEmail { get; set; }

        string ProfileUrl { get; set; }
        
        string WallUrl { get; set; }
    }
}