namespace SampleModel.Entities
{
    using System;

    public class Person : Entity, IPerson
    {
        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual string Email { get; set; }

        public virtual SexEnum Sex { get; set; }

        public virtual DateTime? FechaNacimiento { get; set; }

        public virtual string Work { get; set; }

        public virtual string About { get; set; }

        public virtual string City { get; set; }

        public virtual string PoliticalBeliefs { get; set; }

        public virtual string ReligiousBeliefs { get; set; }

        public virtual string RelationshipStatus { get; set; }

        public virtual IFacebookPerson AsFaceBook()
        {
            return (IFacebookPerson)this;
        }
    }

    public enum SexEnum
    {
        Desconocido, Masculino, Femenino
    }
}