using AutoMapper;
using UserGroupManage.Service.Data.Entities;
using UserGroupManage.Service.Models;

namespace UserGroupManage.Service.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDto, User>();
            CreateMap<User, CreateUserDto>();
            CreateMap<User, UserDto>()
                .ForMember("TypeDesc", dest => dest.MapFrom(src => src.UserType.Description));
            CreateMap<UserDto, User>();
        }
    }
}
