using DSC.Auth.Domain.Entities;
using DSC.Core.Enums;
using System;
using Xunit;

namespace DSC.Auth.UnitTests.Domain
{
    public class TokenModelTests
    {

        private TokenModel _token;

        public TokenModelTests()
        {
            _token = new TokenModel("'mario@gmail.com", "'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Im1hcmlvQGdtYWlsLmNvbSIsImp0aSI6ImM3MTBjZjlhLTU4YzEtNDAxNy04ZTFlLWE2YjI2ZDUzZTRjOSIsImV4cCI6MTYyMzI2OTUzMSwiaXNzIjoiVGVzdGUiLCJhdWQiOiJUZXN0ZSJ9.15vHaGHq6Fi9wwqNssEVAAbydItTYNVpgPsnrAPPBFU", DateTime.Now);
        }

        [Fact]
        public void CreateToken()
        {
            Assert.Equal(EntityStatusEnum.Active, _token.Status);
            Assert.NotNull(_token.Id);
            Assert.NotNull(_token.DateCreateAt);

            Assert.NotNull(_token.DateExpiration);

            Assert.NotNull(_token.UserName);
            Assert.NotEmpty(_token.UserName);

            Assert.NotNull(_token.Token);
            Assert.NotEmpty(_token.Token);

        }

        [Fact]
        public void DeleteToken()
        {
            _token.Delete();

            Assert.Equal(EntityStatusEnum.Inactive, _token.Status);
            Assert.NotNull(_token.DateDeleteAt);
        }

    }
}
