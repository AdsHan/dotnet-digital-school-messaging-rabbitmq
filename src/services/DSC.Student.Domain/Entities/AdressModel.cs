using DSC.Core.DomainObjects;

namespace DSC.Student.Domain.Entities
{
    public class AdressModel : BaseEntity
    {
        public AdressModel()
        {
        }

        public AdressModel(string street, string number, string complement, string district, string zipCode, string city, string state)
        {
            Street = street;
            Number = number;
            Complement = complement;
            District = district;
            ZipCode = zipCode;
            City = city;
            State = state;
        }

        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Complement { get; private set; }
        public string District { get; private set; }
        public string ZipCode { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }

        public void Update(string street, string number, string complement, string district, string zipCode, string city, string state)
        {
            Street = street;
            Number = number;
            Complement = complement;
            District = district;
            ZipCode = zipCode;
            City = city;
            State = state;
        }

    }
}