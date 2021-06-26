using DSC.Core.DomainObjects;
using System;

namespace DSC.Auth.Domain.Entities
{
    public class TokenModel : BaseEntity, IAggregateRoot
    {
        // EF Construtor
        public TokenModel()
        {

        }

        public TokenModel(string userName, string token, DateTime dateExpiration)
        {
            UserName = userName;
            Token = token;
            DateExpiration = dateExpiration;
        }

        public string UserName { get; private set; }
        public string Token { get; private set; }
        public DateTime DateExpiration { get; private set; }

    }
}
