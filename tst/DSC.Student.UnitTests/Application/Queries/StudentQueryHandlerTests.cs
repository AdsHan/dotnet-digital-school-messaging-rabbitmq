using AutoMapper;
using DSC.Student.API.Application.DTO;
using DSC.Student.API.Application.Messages.Queries.StudentQuery;
using DSC.Student.Domain.Entities;
using DSC.Student.Domain.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DSC.Student.UnitTests.Application.Queries
{
    public class StudentQueryHandlerTests
    {
        [Fact]
        public async Task ThreeStudentsExist_Executed_ReturnThreeStudentsDTO()
        {
            // Arrange (config)
            var students = new List<StudentModel>
            {
                new StudentModel("Théo da Silva", DateTime.Now, "187923607", "35540251008", "Possui alergia a lactose", Guid.NewGuid()),
                new StudentModel("Anderson da Silva", DateTime.Now, "187923607", "35540251008", "Possui alergia a lactose", Guid.NewGuid()),
                new StudentModel("Camila da Silva", DateTime.Now, "187923607", "35540251008", "Possui alergia a lactose", Guid.NewGuid()),
            };

            var config = new MapperConfiguration(cfg => cfg.CreateMap<StudentModel, StudentDTO>());
            var mapper = new Mapper(config);

            var studentRepositoryMock = new Mock<IStudentRepository>();
            studentRepositoryMock.Setup(pr => pr.GetAllAsync().Result).Returns(students);

            var getAllStudentQuery = new GetAllStudentQuery();
            var studentQueryHandler = new StudentQueryHandler(studentRepositoryMock.Object, mapper);

            // Act
            var studentsList = await studentQueryHandler.Handle(getAllStudentQuery, new CancellationToken());

            // Assert
            Assert.NotNull(studentsList);
            Assert.NotEmpty(studentsList);
            Assert.Equal(studentsList.Count, students.Count);

            studentRepositoryMock.Verify(pr => pr.GetAllAsync().Result, Times.Once);
        }

        [Fact]
        public async Task StudentExist_Executed_ReturnStudentById()
        {
            // Arrange (config)            
            var student = new StudentModel("Théo da Silva", DateTime.Now, "187923607", "35540251008", "Possui alergia a lactose", Guid.NewGuid());

            var config = new MapperConfiguration(cfg => cfg.CreateMap<StudentModel, StudentDTO>());
            var mapper = new Mapper(config);

            var studentRepositoryMock = new Mock<IStudentRepository>();
            studentRepositoryMock.Setup(pr => pr.GetByIdAsync(student.Id).Result).Returns(student);

            var getByIdStudentQuery = new GetByIdStudentQuery(student.Id);
            var studentQueryHandler = new StudentQueryHandler(studentRepositoryMock.Object, mapper);

            // Act
            var studentsList = await studentQueryHandler.Handle(getByIdStudentQuery, new CancellationToken());

            // Assert
            Assert.NotNull(studentsList);
            studentRepositoryMock.Verify(pr => pr.GetByIdAsync(student.Id).Result, Times.Once);
        }
    }
}