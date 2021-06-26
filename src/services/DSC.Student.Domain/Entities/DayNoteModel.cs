using DSC.Core.DomainObjects;
using System;

namespace DSC.Student.Domain.Entities
{
    public class DayNoteModel : BaseEntity, IAggregateRoot
    {

        // EF Construtor
        public DayNoteModel()
        {
        }

        public DayNoteModel(DateTime date, string text, Guid studentId)
        {
            DataNote = date;
            Text = text;
            StudentId = studentId;
        }

        public DateTime DataNote { get; private set; }
        public string Text { get; private set; }

        public Guid? StudentId { get; private set; }

        // EF Relação
        public StudentModel Student { get; private set; }

    }
}
