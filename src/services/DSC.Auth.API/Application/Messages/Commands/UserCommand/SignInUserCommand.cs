using DSC.Core.Commands;
using FluentValidation;

namespace DSC.Auth.API.Application.Messages.Commands.UserCommand
{

    public class SignInUserCommand : Command
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public override bool Validate()
        {
            BaseResult.ValidationResult = new SignInUserValidation().Validate(this);
            return BaseResult.ValidationResult.IsValid;
        }

        public class SignInUserValidation : AbstractValidator<SignInUserCommand>
        {
            public SignInUserValidation()
            {
                RuleFor(c => c.Email)
                    .NotEmpty()
                    .WithMessage("O Email do usuário não foi informado");

                RuleFor(c => c.Password)
                    .NotEmpty()
                    .WithMessage("O senha do usuário foi informado");
            }
        }
    }
}