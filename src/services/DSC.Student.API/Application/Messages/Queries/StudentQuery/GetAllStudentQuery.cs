
using DSC.Student.API.Application.DTO;
using MediatR;
using System.Collections.Generic;

namespace DSC.Student.API.Application.Messages.Queries.StudentQuery
{

    public class GetAllStudentQuery : IRequest<List<StudentDTO>>
    {
    }

}
