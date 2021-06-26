using DSC.Auth.Domain.Entities;
using DSC.Core.Data;
using System.Threading.Tasks;

namespace DSC.Auth.Domain.Repositories
{
    public interface ITokenRepository : IRepository<TokenModel>
    {
        string GenerateToken(string UserName);
        Task<string> RefreshToken(string token);
    }
}
