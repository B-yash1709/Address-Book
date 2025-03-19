using AutoMapper;
using RepositoryLayer.Entity;
using ModelLayer.Model;

namespace BusinessLayer.Mappings
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            //  Map between UserRequest and UserEntity
            CreateMap<UserRequest, UserEntity>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());  // Hashing is done separately

            //  Map between UserEntity and UserResponse (Only Name and Email)
            CreateMap<UserEntity, UserResponse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
        }
    }
}
