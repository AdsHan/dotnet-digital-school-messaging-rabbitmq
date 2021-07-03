using DSC.Auth.Domain.Entities;
using DSC.Auth.Domain.Repositories;
using DSC.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RTO.Auth.API.Extensions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DSC.Auth.Infrastructure.Data.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly AuthDbContext _dbContext;
        private readonly TokenSettings _tokenSettings;

        public TokenRepository(AuthDbContext dbContext, TokenSettings tokenSettings)
        {
            _dbContext = dbContext;
            _tokenSettings = tokenSettings;
        }

        public async Task<TokenModel> GetByUserNameAsync(string userName)
        {
            return await _dbContext.Tokens
                .Where(a => a.Status == EntityStatusEnum.Active)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.UserName == userName);
        }

        public async Task<List<TokenModel>> GetAllAsync()
        {
            return await _dbContext.Tokens
                .Where(a => a.Status == EntityStatusEnum.Active)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TokenModel> GetByIdAsync(Guid id)
        {
            return await _dbContext.Tokens
                .Where(a => a.Status == EntityStatusEnum.Active)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public void Update(TokenModel token)
        {
            // Reforço que as entidades foram alteradas
            _dbContext.Entry(token).State = EntityState.Modified;
            _dbContext.Update(token);
        }

        public void Add(TokenModel token)
        {
            _dbContext.Add(token);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public string GenerateToken(string UserName)
        {
            // Define as claims do usuário (não é obrigatório, mas melhora a segurança (cria mais chaves no Payload))
            var claims = new[]
            {
                 new Claim(JwtRegisteredClaimNames.UniqueName, UserName),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
             };

            // Gera uma chave
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.SecretJWTKey));

            // Gera a assinatura digital do token
            var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Tempo de expiracão do token
            var expiracao = _tokenSettings.ExpireHours;
            var expiration = DateTime.UtcNow.AddHours(expiracao);

            // Gera o token
            JwtSecurityToken token = new JwtSecurityToken(
              issuer: _tokenSettings.Issuer,
              audience: _tokenSettings.Audience,
              claims: claims,
              expires: expiration,
              signingCredentials: credenciais);

            // Retorna o token e demais informações
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public async Task<string> RefreshToken(string token)
        {
            var result = await _dbContext.Tokens.AsNoTracking().FirstOrDefaultAsync(u => u.Token == token);

            return result != null && result.DateExpiration.ToLocalTime() > DateTime.Now ? GenerateToken(result.UserName) : null;
        }


    }
}
