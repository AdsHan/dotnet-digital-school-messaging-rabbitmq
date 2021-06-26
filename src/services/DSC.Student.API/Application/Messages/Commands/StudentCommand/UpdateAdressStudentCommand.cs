using DSC.Core.Commands;
using FluentValidation;
using System;

namespace DSC.Student.API.Application.Messages.Commands.StudentCommand
{
    public class UpdateAdressStudentCommand : Command
    {
        public Guid StudentId { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public override bool Validate()
        {
            BaseResult.ValidationResult = new UpdateAdressStudentValidation().Validate(this);
            return BaseResult.ValidationResult.IsValid;
        }

        public class UpdateAdressStudentValidation : AbstractValidator<UpdateAdressStudentCommand>
        {
            public UpdateAdressStudentValidation()
            {
                RuleFor(c => c.StudentId)
                    .NotEmpty()
                    .WithMessage("O código do endereço não foi informado");

                RuleFor(c => c.Street)
                    .NotEmpty()
                    .WithMessage("Informe o Logradouro");

                RuleFor(c => c.Number)
                    .NotEmpty()
                    .WithMessage("Informe o Número");

                RuleFor(c => c.ZipCode)
                    .NotEmpty()
                    .WithMessage("Informe o CEP");

                RuleFor(c => c.District)
                    .NotEmpty()
                    .WithMessage("Informe o Bairro");

                RuleFor(c => c.City)
                    .NotEmpty()
                    .WithMessage("Informe o Cidade");

                RuleFor(c => c.State)
                    .NotEmpty()
                    .WithMessage("Informe o Estado");
            }
        }
    }
}