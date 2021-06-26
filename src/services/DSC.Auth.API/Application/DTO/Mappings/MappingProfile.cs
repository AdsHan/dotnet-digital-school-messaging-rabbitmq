using AutoMapper;
using DSC.Auth.Domain.Entities;

namespace DSC.Auth.API.Application.DTO.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserModel, UserDTO>().ReverseMap();
        }
    }
}
