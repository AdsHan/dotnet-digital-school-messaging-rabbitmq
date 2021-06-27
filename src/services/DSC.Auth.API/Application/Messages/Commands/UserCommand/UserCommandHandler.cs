using DSC.Auth.API.Application.DTO;
using DSC.Auth.Domain.Entities;
using DSC.Auth.Domain.Repositories;
using DSC.Auth.Infrastructure.Data;
using DSC.Core.Commands;
using DSC.Core.Communication;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DSC.Auth.API.Application.Messages.Commands.UserCommand
{
    public class UserCommandHandler : CommandHandler,
        IRequestHandler<AddUserCommand, BaseResult>,
        IRequestHandler<SignInUserCommand, BaseResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly IConfiguration _configuration;


        private readonly AuthDbContext _dbContext;

        public UserCommandHandler(IUserRepository userRepository, ITokenRepository tokenRepository, IConfiguration configuration, AuthDbContext dbContext)
        {
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
            _configuration = configuration;
            _dbContext = dbContext;
        }

        public async Task<BaseResult> Handle(AddUserCommand command, CancellationToken cancellationToken)
        {
            if (!command.Validate()) return command.BaseResult;

            var isIncluded = await _userRepository.GetByUserNameAsync(command.Email);

            if (isIncluded != null)
            {
                AddError("Este nome de usuário já está em uso por outro usuário!");
                return BaseResult;
            }

            var user = new UserModel()
            {
                UserName = command.Email,
                Email = command.Email,
                PhoneNumber = command.Phone,
                EmailConfirmed = true
            };

            var result = await _userRepository.CreateAsync(user, command.Password);

            if (!result.Succeeded)
            {
                AddError("Não foi possível incluir o usuário!");
                return BaseResult;
            }

            return BaseResult;
        }

        public async Task<BaseResult> Handle(SignInUserCommand command, CancellationToken cancellationToken)
        {
            if (!command.Validate()) return command.BaseResult;

            var isIncluded = await _userRepository.GetByUserNameAsync(command.Email);

            if (isIncluded == null)
            {
                AddError("Este usuário não existe!");
                return BaseResult;
            }

            var result = await _userRepository.SignInAsync(command.Email, command.Password);

            if (result.Succeeded)
            {
                var expiration = _configuration["TokenConfiguration:ExpireHours"];
                var token = _tokenRepository.GenerateToken(command.Email);
                var expirationDate = DateTime.UtcNow.AddHours(double.Parse(expiration));

                var tokenModel = new TokenModel(command.Email, token, expirationDate);
                _tokenRepository.Add(tokenModel);
                await _tokenRepository.SaveAsync();

                // Retorna o token e demais informações
                var response = new LoginTokenDTO
                {
                    Authenticated = true,
                    Token = token,
                    Expiration = expirationDate,
                    Message = "Token JWT OK",
                };

                BaseResult.response = response;
                return BaseResult;
            }

            if (result.IsLockedOut)
            {
                AddError("Usuário temporariamente bloqueado por tentativas inválidas");
                return BaseResult;
            }

            AddError("Usuário ou Senha incorretos");
            return BaseResult;
        }

    }
}