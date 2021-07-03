using DSC.Core.Commands;
using FluentValidation;
using System;

namespace DSC.Student.API.Application.Messages.Commands.StudentCommand
{

    public class CheckStudentUsersCreatedCommand : Command
    {
        public CheckStudentUsersCreatedCommand(Guid studentId)
        {
            StudentId = studentId;
        }

        public Guid StudentId { get; set; }
        public override bool Validate()
        {
            BaseResult.ValidationResult = new CheckStudentUsersCreatedValidation().Validate(this);
            return BaseResult.ValidationResult.IsValid;
        }

        public class CheckStudentUsersCreatedValidation : AbstractValidator<CheckStudentUsersCreatedCommand>
        {
            public CheckStudentUsersCreatedValidation()
            {
                RuleFor(c => c.StudentId)
                    .NotEmpty()
                    .WithMessage("O código do aluno não foi informado");
            }
        }
    }
}