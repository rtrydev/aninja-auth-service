using aninja_auth_service.Dtos;
using aninja_auth_service.Models;
using AutoMapper;

namespace aninja_auth_service.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
