using DSC.Core.Enums;
using DSC.Student.Domain.Entities;
using System;
using Xunit;

namespace DSC.Student.UnitTests.Domain
{
    public class CourseModelTests
    {

        private CourseModel _course;

        public CourseModelTests()
        {
            _course = new CourseModel("Berçário II - 2021", "Vamos manter o máximo de 10 alunos neste ano", Guid.NewGuid());
        }

        [Fact]
        public void CreateCourse()
        {
            Assert.Equal(EntityStatusEnum.Active, _course.Status);
            Assert.NotNull(_course.Id);
            Assert.NotNull(_course.DateCreateAt);

            Assert.NotNull(_course.Name);
            Assert.NotEmpty(_course.Name);

            Assert.NotNull(_course.Note);
            Assert.NotEmpty(_course.Note.Text);

            Assert.NotNull(_course.SchoolId);
        }

        [Fact]
        public void DeleteCourse()
        {
            _course.Delete();

            Assert.Equal(EntityStatusEnum.Inactive, _course.Status);
            Assert.NotNull(_course.DateDeleteAt);
        }

    }
}
