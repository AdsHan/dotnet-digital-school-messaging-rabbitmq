using DSC.Core.DomainObjects;
using System;
using System.Collections.Generic;

namespace DSC.Student.Domain.Entities
{
    public class SchoolModel : BaseEntity, IAggregateRoot
    {
        // EF Construtor
        public SchoolModel()
        {
        }

        public SchoolModel(string corporateName, string tradeName, string cnpj, string email, string phone, string cellPhone, string note)
        {
            CorporateName = corporateName;
            TradeName = tradeName;
            Cnpj = new Cnpj(cnpj);
            Email = new Email(email);
            Phone = new Phone(phone);
            CellPhone = new Phone(cellPhone);
            Note = new NoteModel(note);
            Adress = new AdressModel();
            Courses = new List<CourseModel>();
        }

        public string CorporateName { get; private set; }
        public string TradeName { get; private set; }
        public Cnpj Cnpj { get; private set; }
        public Email Email { get; private set; }
        public Phone Phone { get; private set; }
        public Phone CellPhone { get; private set; }
        public List<CourseModel> Courses { get; private set; }

        public Guid? NoteId { get; private set; }
        public Guid? AdressId { get; private set; }

        // EF Relação
        public NoteModel Note { get; private set; }
        public AdressModel Adress { get; private set; }

        public void UpdateAdress(AdressModel adress)
        {
            Adress = adress;
        }

        public void UpdateCourses(List<CourseModel> courses)
        {
            Courses = courses;
        }
    }
}
