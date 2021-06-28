using AutoMapper;
using DSC.Auth.API.Application.DTO;
using DSC.Auth.API.Application.Messages.Queries.AuthQuery;
using DSC.Auth.API.Application.Messages.Queries.UserQuery;
using DSC.Auth.Domain.Entities;
using DSC.Auth.Domain.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DSC.Auth.UnitTests.Application.Queries
{
    public class UserQueryHandlerTests
    {
        [Fact]
        public async Task ThreeUsersExist_Executed_ReturnThreeUsersDTO()
        {
            // Arrange (config)
            var users = new List<UserModel> {
                new UserModel() { UserName = "mario@gmail.com", Email = "mario@gmail.com", PhoneNumber = "99 9999-9999", EmailConfirmed = true},
                new UserModel() { UserName = "maria@gmail.com", Email = "maria@gmail.com", PhoneNumber = "99 9999-9999", EmailConfirmed = true}
            };

            var config = new MapperConfiguration(cfg => cfg.CreateMap<UserModel, UserDTO>());
            var mapper = new Mapper(config);

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(pr => pr.GetAllAsync().Result).Returns(users);

            var getAllUserQuery = new GetAllUserQuery();
            var userQueryHandler = new UserQueryHandler(userRepositoryMock.Object, mapper);

            // Act
            var usersList = await userQueryHandler.Handle(getAllUserQuery, new CancellationToken());

            // Assert
            Assert.NotNull(usersList);
            Assert.NotEmpty(usersList);
            Assert.Equal(usersList.Count, usersList.Count);

            userRepositoryMock.Verify(pr => pr.GetAllAsync().Result, Times.Once);
        }

        [Fact]
        public async Task UserExist_Executed_ReturnUserById()
        {
            // Arrange (config)            
            var user = new UserModel() { UserName = "mario@gmail.com", Email = "mario@gmail.com", PhoneNumber = "99 9999-9999", EmailConfirmed = true };

            var config = new MapperConfiguration(cfg => cfg.CreateMap<UserModel, UserDTO>());
            var mapper = new Mapper(config);

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(pr => pr.GetByIdAsync(user.Id).Result).Returns(user);

            var getByIdUserQuery = new GetByIdUserQuery(user.Id);
            var userQueryHandler = new UserQueryHandler(userRepositoryMock.Object, mapper);

            // Act
            var usersList = await userQueryHandler.Handle(getByIdUserQuery, new CancellationToken());

            // Assert
            Assert.NotNull(usersList);
            userRepositoryMock.Verify(pr => pr.GetByIdAsync(user.Id).Result, Times.Once);
        }
    }
}