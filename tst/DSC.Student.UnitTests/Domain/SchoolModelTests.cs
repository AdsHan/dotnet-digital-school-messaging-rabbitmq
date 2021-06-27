using DSC.Core.Enums;
using DSC.Student.Domain.Entities;
using Xunit;

namespace DSC.Student.UnitTests.Domain
{
    public class SchoolModelTests
    {

        private SchoolModel _school;

        public SchoolModelTests()
        {
            _school = new SchoolModel("Miranda Escola", "Escola dos Pequeninos", "33904180000195", "pequeninos@gmail.com", "51 99999-9999", "51 99999-9999", "Nossa missão consiste em estimular o educando a desenvolver suas potencialidades");
        }

        [Fact]
        public void CreateSchool()
        {

            Assert.Equal(EntityStatusEnum.Active, _school.Status);
            Assert.NotNull(_school.Id);
            Assert.NotNull(_school.DateCreateAt);

            Assert.NotNull(_school.CorporateName);
            Assert.NotEmpty(_school.CorporateName);

            Assert.NotNull(_school.TradeName);
            Assert.NotEmpty(_school.TradeName);

            Assert.NotNull(_school.Cnpj);
            Assert.NotEmpty(_school.Cnpj.Number);

            Assert.NotNull(_school.Email);
            Assert.NotEmpty(_school.Email.Address);

            Assert.NotNull(_school.Phone);
            Assert.NotEmpty(_school.Phone.Number);

            Assert.NotNull(_school.CellPhone);
            Assert.NotEmpty(_school.CellPhone.Number);

            Assert.NotNull(_school.Note);
            Assert.NotEmpty(_school.Note.Text);
        }

        [Fact]
        public void DeleteSchool()
        {
            _school.Delete();

            Assert.Equal(EntityStatusEnum.Inactive, _school.Status);
            Assert.NotNull(_school.DateDeleteAt);
        }

    }
}
