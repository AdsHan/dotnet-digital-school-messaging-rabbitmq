
using System;
namespace DSC.Auth.API.Application.DTO
{
    public class LoginTokenDTO
    {
        public bool Authenticated { get; set; }
        public DateTime Expiration { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
    }

}
