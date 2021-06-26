using DSC.Core.DomainObjects;
using System;
using System.Collections.Generic;

namespace DSC.Student.Domain.Entities
{
    public class CourseModel : BaseEntity, IAggregateRoot
    {

        // EF Construtor
        public CourseModel()
        {
        }

        public CourseModel(string name, string note, Guid schoolId)
        {
            SchoolId = schoolId;
            Name = name;
            Note = new NoteModel(note);
            Students = new List<StudentModel>();
        }

        public string Name { get; private set; }
        public List<StudentModel> Students { get; private set; }

        public Guid? SchoolId { get; private set; }
        public Guid? NoteId { get; private set; }

        // EF Relação        
        public NoteModel Note { get; private set; }
        public SchoolModel School { get; private set; }

        public void UpdateStudents(List<StudentModel> students)
        {
            Students = students;
        }
    }
}
