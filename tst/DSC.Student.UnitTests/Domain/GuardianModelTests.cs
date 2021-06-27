using DSC.Core.Enums;
using DSC.Student.Domain.Entities;
using System;
using Xunit;

namespace DSC.Student.UnitTests.Domain
{
    public class GuardianModelTests
    {

        private GuardianModel _guardian;

        public GuardianModelTests()
        {
            _guardian = new GuardianModel("Maria Santos da Silva", DateTime.Now, "473075404", "31869845056", "51 8888-8888", "51 8888-8888", "maria@gmail.com", "Ligar para a empresa caso não atender");
        }

        [Fact]
        public void CreateGuardian()
        {
            Assert.Equal(EntityStatusEnum.Active, _guardian.Status);
            Assert.NotNull(_guardian.Id);
            Assert.NotNull(_guardian.DateCreateAt);

            Assert.NotNull(_guardian.DateBirth);

            Assert.NotNull(_guardian.Name);
            Assert.NotEmpty(_guardian.Name);

            Assert.NotNull(_guardian.Rg);
            Assert.NotEmpty(_guardian.Rg.Number);

            Assert.NotNull(_guardian.Cpf);
            Assert.NotEmpty(_guardian.Cpf.Number);

            Assert.NotNull(_guardian.Email);
            Assert.NotEmpty(_guardian.Email.Address);

            Assert.NotNull(_guardian.Phone);
            Assert.NotEmpty(_guardian.Phone.Number);

            Assert.NotNull(_guardian.CellPhone);
            Assert.NotEmpty(_guardian.CellPhone.Number);

            Assert.NotNull(_guardian.Note);
            Assert.NotEmpty(_guardian.Note.Text);
        }

        [Fact]
        public void DeleteGuardian()
        {
            _guardian.Delete();

            Assert.Equal(EntityStatusEnum.Inactive, _guardian.Status);
            Assert.NotNull(_guardian.DateDeleteAt);
        }

    }
}
