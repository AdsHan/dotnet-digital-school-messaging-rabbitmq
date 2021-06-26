using AutoMapper;
using DSC.Student.API.Application.DTO;
using DSC.Student.Domain.Entities;
using DSC.Student.Domain.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DSC.Student.API.Application.Messages.Queries.StudentQuery
{
    public class StudentQueryHandler :
        IRequestHandler<GetAllStudentQuery, List<StudentDTO>>,
        IRequestHandler<GetByIdStudentQuery, StudentModel>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentQueryHandler(IStudentRepository studentRepository, IMapper mapper)
        {
            _mapper = mapper;
            _studentRepository = studentRepository;
        }

        public async Task<List<StudentDTO>> Handle(GetAllStudentQuery request, CancellationToken cancellationToken)
        {
            var students = await _studentRepository.GetAllAsync();

            var studentsDTO = _mapper.Map<List<StudentDTO>>(students);

            return studentsDTO;
        }

        public async Task<StudentModel> Handle(GetByIdStudentQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByIdAsync(request.Id);

            if (student == null) return null;

            return student;
        }
    }

}
