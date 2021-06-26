using DSC.Core.DomainObjects;

namespace DSC.Auth.Domain.Entities
{
    public class UserModel : BaseIdentityEntity, IAggregateRoot
    {
        // EF Construtor
        public UserModel()
        {

        }
    }
}
