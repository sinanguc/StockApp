using Assessment.Dto.Stock.Authentication;
using Assessment.Dto.Stock.User;
using Assessment.Stock.Entities.Concrete.Users;
using AutoMapper;

namespace Assessment.Stock.Business.DependencyResolvers.AutoMapper
{
    public class AuthenticationProfile : Profile
    {
        public AuthenticationProfile()
        {
            CreateMap<LoginResponseDto, UserResponseDto>().ReverseMap();
            CreateMap<UserUpdateDto, User>().ReverseMap();
            CreateMap<UserResponseDto, User>().ReverseMap();
            CreateMap<LoginResponseDto, UserUpdateDto>().ReverseMap();
        }
    }
}
