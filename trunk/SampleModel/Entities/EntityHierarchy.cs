namespace SampleModel.Entities
{
    public class Base : Entity
    {
        public virtual int POID { get; set; }

        public virtual int Desc { get; set; }
    }

    public class Child1 : Base
    {
        public virtual int DescChild1 { get; set; }
    }

    public class Child2 : Base
    {
        public virtual int DescChild2 { get; set; }
    }
}