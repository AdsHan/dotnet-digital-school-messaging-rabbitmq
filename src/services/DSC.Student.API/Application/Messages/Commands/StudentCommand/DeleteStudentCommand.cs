using DSC.Core.Commands;
using FluentValidation;
using System;

namespace DSC.Student.API.Application.Messages.Commands.StudentCommand
{

    public class DeleteStudentCommand : Command
    {
        public DeleteStudentCommand(Guid studentId)
        {
            StudentId = studentId;
        }

        public Guid StudentId { get; set; }
        public override bool Validate()
        {
            BaseResult.ValidationResult = new DeleteStudentValidation().Validate(this);
            return BaseResult.ValidationResult.IsValid;
        }

        public class DeleteStudentValidation : AbstractValidator<DeleteStudentCommand>
        {
            public DeleteStudentValidation()
            {
                RuleFor(c => c.StudentId)
                    .NotEmpty()
                    .WithMessage("O código do aluno não foi informado");
            }
        }
    }
}