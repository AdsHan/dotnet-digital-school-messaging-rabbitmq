using DSC.Core.Commands;
using FluentValidation;

namespace DSC.Auth.API.Application.Messages.Commands.TokenCommand
{

    public class RefreshTokenCommand : Command
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }

        public override bool Validate()
        {
            BaseResult.ValidationResult = new RefreshTokenValidation().Validate(this);
            return BaseResult.ValidationResult.IsValid;
        }

        public class RefreshTokenValidation : AbstractValidator<RefreshTokenCommand>
        {
            public RefreshTokenValidation()
            {
                RuleFor(c => c.Email)
                    .NotEmpty()
                    .WithMessage("O Email do usuário não foi informado");

                RuleFor(c => c.Password)
                    .NotEmpty()
                    .WithMessage("O senha do usuário foi informado");

                RuleFor(c => c.Token)
                    .NotEmpty()
                    .WithMessage("O token não foi informado");

            }
        }
    }
}