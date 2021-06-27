using DSC.Auth.Domain.Entities;
using DSC.Core.Enums;
using Xunit;

namespace DSC.Auth.UnitTests.Domain
{
    public class UserModelTests
    {

        private UserModel _user;

        public UserModelTests()
        {
            _user = new UserModel()
            {
                UserName = "mario@gmail.com",
                Email = "mario@gmail.com",
                PhoneNumber = "99 9999-9999",
                EmailConfirmed = true
            };
        }

        [Fact]
        public void CreateUser()
        {
            Assert.Equal(EntityStatusEnum.Active, _user.Status);
            Assert.NotNull(_user.Id);
            Assert.NotNull(_user.DateCreateAt);
        }

        [Fact]
        public void DeleteUser()
        {
            _user.Delete();

            Assert.Equal(EntityStatusEnum.Inactive, _user.Status);
            Assert.NotNull(_user.DateDeleteAt);
        }

    }
}
