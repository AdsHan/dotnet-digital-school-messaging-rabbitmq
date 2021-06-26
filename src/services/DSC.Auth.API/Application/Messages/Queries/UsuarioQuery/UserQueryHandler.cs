using AutoMapper;
using DSC.Auth.API.Application.DTO;
using DSC.Auth.API.Application.Messages.Queries.UserQuery;
using DSC.Auth.Domain.Entities;
using DSC.Auth.Domain.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DSC.Auth.API.Application.Messages.Queries.AuthQuery
{
    public class UserQueryHandler :
        IRequestHandler<GetAllUserQuery, List<UserDTO>>,
        IRequestHandler<GetByIdUserQuery, UserModel>,
        IRequestHandler<GetByUserNAmeUserQuery, UserModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<List<UserDTO>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();

            var usersDTO = _mapper.Map<List<UserDTO>>(users);

            return usersDTO;
        }

        public async Task<UserModel> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);

            if (user == null) return null;

            return user;
        }
        public async Task<UserModel> Handle(GetByUserNAmeUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUserNameAsync(request.UserName);

            if (user == null) return null;

            return user;
        }

    }

}
