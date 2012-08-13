namespace SampleModel.Entities
{
    public class Campaign : Entity
    {
        public virtual string Email { get; set; }
        
        public virtual string Name { get; set; }
    }
}