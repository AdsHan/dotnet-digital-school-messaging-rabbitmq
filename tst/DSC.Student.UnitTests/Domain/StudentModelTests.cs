using DSC.Core.Enums;
using DSC.Student.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DSC.Student.UnitTests.Domain
{
    public class StudentModelTests
    {

        private StudentModel _student;

        public StudentModelTests()
        {
            _student = new StudentModel("Théo da Silva", DateTime.Now, "187923607", "35540251008", "Possui alergia a lactose", Guid.NewGuid());
        }

        [Fact]
        public void CreateStudent()
        {

            Assert.Equal(EntityStatusEnum.Active, _student.Status);
            Assert.NotNull(_student.Id);
            Assert.NotNull(_student.DateCreateAt);

            Assert.NotNull(_student.Name);
            Assert.NotEmpty(_student.Name);

            Assert.NotNull(_student.DateBirth);

            Assert.NotNull(_student.Rg);
            Assert.NotEmpty(_student.Rg.Number);

            Assert.NotNull(_student.Cpf);
            Assert.NotEmpty(_student.Cpf.Number);

            Assert.NotNull(_student.Note);
            Assert.NotEmpty(_student.Note.Text);

            Assert.NotNull(_student.CourseId);

        }

        [Fact]
        public void DeleteStudent()
        {
            _student.Delete();

            Assert.Equal(EntityStatusEnum.Inactive, _student.Status);
            Assert.NotNull(_student.DateDeleteAt);
        }

        [Fact]
        public void UpdateAdress()
        {
            var adress = new AdressModel("Rua Sol Nascente", "1111", null, "Vista Alegre", "93900-000", "Ivoti", "RS");
            Assert.Equal(EntityStatusEnum.Active, _student.Status);
            Assert.NotNull(_student.Id);
            Assert.NotNull(_student.DateCreateAt);

            _student.UpdateAdress(adress);

            Assert.NotNull(_student.Adress.Street);
            Assert.NotEmpty(_student.Adress.Street);

            Assert.NotNull(_student.Adress.Number);
            Assert.NotEmpty(_student.Adress.Number);

            Assert.NotNull(_student.Adress.District);
            Assert.NotEmpty(_student.Adress.District);

            Assert.NotNull(_student.Adress.ZipCode);
            Assert.NotEmpty(_student.Adress.ZipCode);

            Assert.NotNull(_student.Adress.City);
            Assert.NotEmpty(_student.Adress.City);

            Assert.NotNull(_student.Adress.State);
            Assert.NotEmpty(_student.Adress.State);
        }

        [Fact]
        public void UpdateGuardians()
        {
            var guardians = new[]
            {
                new
                {
                    name = "João da Silva",
                    rg = "187923607",
                    cpf = "98283526057",
                    email = "joao@gmail.com",
                    phone = "51 99999-9999",
                    cellPhone = "51 99999-9999",
                    note = "Ligar para a empresa caso não atender"
                },

                new {
                    name = "Maria Santos da Silva",
                    rg = "473075404",
                    cpf = "31869845056",
                    email = "maria@gmail.com",
                    phone = "51 8888-8888",
                    cellPhone = "51 8888-8888",
                    note = "Ligar para a empresa caso não atender"
                }
            }.Select(r => new GuardianModel(r.name, DateTime.Now, r.rg, r.cpf, r.phone, r.cellPhone, r.email, r.note)).ToList();

            var studentsGuardians = new List<StudentGuardianModel>();

            foreach (var item in guardians)
            {
                studentsGuardians.Add(new StudentGuardianModel(_student, item));

            }

            _student.UpdateGuardians(studentsGuardians);

            Assert.NotEmpty(_student.StudentsGuardians);

            Assert.Equal(EntityStatusEnum.Active, _student.StudentsGuardians[0].Guardian.Status);
            Assert.NotNull(_student.StudentsGuardians[0].Guardian.Id);
            Assert.NotNull(_student.StudentsGuardians[0].Guardian.DateCreateAt);

            Assert.NotNull(_student.StudentsGuardians[0].Guardian.DateBirth);

            Assert.NotNull(_student.StudentsGuardians[0].Guardian.Name);
            Assert.NotEmpty(_student.StudentsGuardians[0].Guardian.Name);

            Assert.NotNull(_student.StudentsGuardians[0].Guardian.Rg);
            Assert.NotEmpty(_student.StudentsGuardians[0].Guardian.Rg.Number);

            Assert.NotNull(_student.StudentsGuardians[0].Guardian.Cpf);
            Assert.NotEmpty(_student.StudentsGuardians[0].Guardian.Cpf.Number);

            Assert.NotNull(_student.StudentsGuardians[0].Guardian.Email);
            Assert.NotEmpty(_student.StudentsGuardians[0].Guardian.Email.Address);

            Assert.NotNull(_student.StudentsGuardians[0].Guardian.Phone);
            Assert.NotEmpty(_student.StudentsGuardians[0].Guardian.Phone.Number);

            Assert.NotNull(_student.StudentsGuardians[0].Guardian.CellPhone);
            Assert.NotEmpty(_student.StudentsGuardians[0].Guardian.CellPhone.Number);
        }
    }
}
