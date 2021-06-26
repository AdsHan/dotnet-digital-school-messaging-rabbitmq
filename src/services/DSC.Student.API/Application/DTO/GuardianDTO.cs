using System;

namespace DSC.Student.API.Application.DTO
{
    public class GuardianDTO
    {
        public Guid id { get; set; }
        public string Name { get; set; }
        public DateTime DateBirth { get; set; }
        public string Rg { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CellPhone { get; set; }
        public string Note { get; set; }
    }
}