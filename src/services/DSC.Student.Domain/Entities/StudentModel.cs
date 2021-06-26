using DSC.Core.DomainObjects;
using System;
using System.Collections.Generic;

namespace DSC.Student.Domain.Entities
{
    public class StudentModel : BaseEntity, IAggregateRoot
    {
        // EF Construtor
        public StudentModel()
        {
        }

        public StudentModel(string name, DateTime dateBirth, string rg, string cpf, string note, Guid courseId)
        {
            Name = name;
            DateBirth = dateBirth;
            CourseId = courseId;
            Rg = new Rg(rg);
            Cpf = new Cpf(cpf);
            Note = new NoteModel(note);
            Adress = new AdressModel();
            StudentsGuardians = new List<StudentGuardianModel>();
            DayNotes = new List<DayNoteModel>();
        }

        public string Name { get; private set; }
        public DateTime DateBirth { get; private set; }
        public Rg Rg { get; private set; }
        public Cpf Cpf { get; private set; }

        public Guid? CourseId { get; private set; }
        public Guid? AdressId { get; private set; }
        public Guid? NoteId { get; private set; }

        public List<StudentGuardianModel> StudentsGuardians { get; set; }
        public List<DayNoteModel> DayNotes { get; private set; }

        // EF Relação
        public NoteModel Note { get; private set; }
        public AdressModel Adress { get; private set; }
        public CourseModel Course { get; private set; }

        public void UpdateAdress(AdressModel adress)
        {
            Adress = adress;
        }

        public void UpdateGuardians(List<StudentGuardianModel> guardians)
        {
            StudentsGuardians = guardians;
        }

        public void UpdateDayNotes(List<DayNoteModel> dayNotes)
        {
            DayNotes = dayNotes;
        }

        public void Update(string name, DateTime dateBirth, string rg, string cpf, string note, Guid courseId)
        {
            Name = name;
            DateBirth = dateBirth;
            Rg.Update(rg);
            Cpf.Update(cpf);
            Note.Update(note);
            CourseId = courseId;
        }

    }
}
