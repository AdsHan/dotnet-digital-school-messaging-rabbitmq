using System;

namespace DSC.Student.API.Application.DTO
{
    public class AdressDTO
    {
        public Guid id { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}