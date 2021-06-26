using DSC.Core.DomainObjects;
using System;
using System.Collections.Generic;

namespace DSC.Student.Domain.Entities
{
    public class GuardianModel : BaseEntity, IAggregateRoot
    {
        // EF Construtor
        public GuardianModel()
        {
        }

        public GuardianModel(string name, DateTime dateBirth, string rg, string cpf, string phone, string cellPhone, string email, string note)
        {
            Name = name;
            DateBirth = dateBirth;
            Rg = new Rg(rg);
            Cpf = new Cpf(cpf);
            Email = new Email(email);
            Phone = new Phone(phone);
            CellPhone = new Phone(cellPhone);
            Note = new NoteModel(note);
            StudentsGuardians = new List<StudentGuardianModel>();
        }

        public string Name { get; private set; }
        public DateTime DateBirth { get; private set; }
        public Rg Rg { get; private set; }
        public Cpf Cpf { get; private set; }
        public Email Email { get; private set; }
        public Phone Phone { get; private set; }
        public Phone CellPhone { get; private set; }

        public Guid? NoteId { get; private set; }

        public List<StudentGuardianModel> StudentsGuardians { get; set; }

        // EF Relação
        public NoteModel Note { get; private set; }

        public void UpdateStudents(List<StudentGuardianModel> studentsGuardians)
        {
            StudentsGuardians = studentsGuardians;
        }

        public void Update(string name, DateTime dateBirth, string rg, string cpf, string phone, string cellPhone, string email, string note)
        {
            Name = name;
            DateBirth = dateBirth;
            Rg.Update(rg);
            Cpf.Update(cpf);
            Phone.Update(phone);
            CellPhone.Update(cellPhone);
            Email.Update(email);
            Note.Update(note);
        }
    }
}
