using DSC.Core.Enums;
using DSC.Student.Domain.Entities;
using System;
using Xunit;

namespace DSC.Student.UnitTests.Domain
{
    public class DayNoteModelTests
    {

        private DayNoteModel _dayNote;

        public DayNoteModelTests()
        {
            _dayNote = new DayNoteModel(DateTime.Now, "Tudo certo com nosso pequeno hoje. Fez todas as atividades. Dormiu pouco na hora do soninho. Profe Fê", Guid.NewGuid());
        }

        [Fact]
        public void CreateDayNoteModel()
        {
            Assert.Equal(EntityStatusEnum.Active, _dayNote.Status);
            Assert.NotNull(_dayNote.Id);
            Assert.NotNull(_dayNote.DateCreateAt);

            Assert.NotNull(_dayNote.DataNote);

            Assert.NotNull(_dayNote.StudentId);

            Assert.NotNull(_dayNote.Text);
            Assert.NotEmpty(_dayNote.Text);
        }

        [Fact]
        public void DeleteDayNoteModel()
        {
            _dayNote.Delete();

            Assert.Equal(EntityStatusEnum.Inactive, _dayNote.Status);
            Assert.NotNull(_dayNote.DateDeleteAt);
        }

    }
}
