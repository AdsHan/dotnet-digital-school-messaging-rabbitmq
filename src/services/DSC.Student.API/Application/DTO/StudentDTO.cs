using DSC.Core.DomainObjects;
using System;

namespace DSC.Student.API.Application.DTO
{
    public class StudentDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateBirth { get; set; }
        public Rg Rg { get; set; }
        public Cpf Cpf { get; set; }

    }
}