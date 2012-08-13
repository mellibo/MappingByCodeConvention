namespace SampleModel.Entities
{
    using System;

    public interface IPerson
    {
        string FirstName { get; set; }

        string LastName { get; set; }

        string Email { get; set; }

        SexEnum Sex { get; set; }

        DateTime? FechaNacimiento { get; set; }

        string Work { get; set; }
        
        string About { get; set; }

        string City { get; set; }

        string PoliticalBeliefs { get; set; }

        string ReligiousBeliefs { get; set; }

        string RelationshipStatus { get; set; }
    }
}