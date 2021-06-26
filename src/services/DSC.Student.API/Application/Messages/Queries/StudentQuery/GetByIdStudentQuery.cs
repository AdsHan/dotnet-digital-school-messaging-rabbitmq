using DSC.Student.Domain.Entities;
using MediatR;
using System;

namespace DSC.Student.API.Application.Messages.Queries.StudentQuery
{
    public class GetByIdStudentQuery : IRequest<StudentModel>
    {
        public GetByIdStudentQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
