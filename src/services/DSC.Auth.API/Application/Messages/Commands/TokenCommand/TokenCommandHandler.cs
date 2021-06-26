using DSC.Auth.Domain.Repositories;
using DSC.Core.Commands;
using DSC.Core.Communication;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DSC.Auth.API.Application.Messages.Commands.TokenCommand
{
    public class TokenCommandHandler : CommandHandler,
        IRequestHandler<RefreshTokenCommand, BaseResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenRepository _tokenRepository;

        public TokenCommandHandler(IUserRepository userRepository, ITokenRepository tokenRepository)
        {
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
        }

        public async Task<BaseResult> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
        {
            if (!command.Validate()) return command.BaseResult;

            var isIncluded = await _userRepository.GetByUserNameAsync(command.Email);

            if (isIncluded == null)
            {
                AddError("Não foi possível localizar o usuário!");
                return BaseResult;
            }

            var result = await _userRepository.CheckPasswordAsync(isIncluded.UserName, command.Password);

            if (result)
            {

                var newToken = await _tokenRepository.RefreshToken(command.Token);

                if (newToken == null)
                {
                    AddError("O Token ainda não expirou!");
                    return BaseResult;

                }
                BaseResult.response = newToken;
                return BaseResult;

            }
            AddError("Usuário ou Senha incorretos");
            return BaseResult;
        }
    }
}