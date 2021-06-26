using DSC.Core.Commands;
using DSC.Student.API.Application.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;

namespace DSC.Student.API.Application.Messages.Commands.StudentCommand
{

    public class AddStudentCommand : Command
    {
        public string Name { get; set; }
        public DateTime DateBirth { get; set; }
        public string Rg { get; set; }
        public string Cpf { get; set; }
        public string Note { get; set; }
        public Guid CourseId { get; set; }
        public List<GuardianDTO> Guardians { get; set; }
        public AdressDTO Adress { get; set; }
        public override bool Validate()
        {
            BaseResult.ValidationResult = new AddStudentValidation().Validate(this);
            return BaseResult.ValidationResult.IsValid;
        }

        public class AddStudentValidation : AbstractValidator<AddStudentCommand>
        {
            public AddStudentValidation()
            {
                RuleFor(c => c.Name)
                    .NotEmpty()
                    .WithMessage("O nome do aluno não foi informado");

                RuleFor(c => c.Name)
                    .MaximumLength(200)
                    .WithMessage("Tamanho máximo do nome é de 200 caracteres.");

                RuleFor(c => c.Note)
                    .MaximumLength(8000)
                    .WithMessage("Tamanho máximo da observação é de 8000 caracteres.");

                RuleFor(c => c.Cpf)
                    .Must(TerCpfValido)
                    .WithMessage("O CPF informado não é válido.");

                RuleFor(c => c.Rg)
                    .Must(TerRgValido)
                    .WithMessage("O RG informado não é válido.");
            }

            protected static bool TerCpfValido(string cpf)
            {
                return Core.DomainObjects.Cpf.Validate(cpf);
            }

            protected static bool TerRgValido(string Rg)
            {
                return Core.DomainObjects.Rg.Validate(Rg);
            }
        }
    }
}