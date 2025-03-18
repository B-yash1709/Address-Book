using AutoMapper;
using ModelLayer.Model;
using RepositoryLayer.UserEntity;

namespace BusinessLayer.Mappings
{
    public class AddressBookProfile : Profile
    {
        public AddressBookProfile()
        {
            // Simple mapping between Entity and DTO
            CreateMap<AddressBookEntity, ResponseUserModel>().ReverseMap();

            // Mapping RequestModel to Entity
            CreateMap<RequestModel, AddressBookEntity>().ReverseMap();

            // Mapping Entity to ResponseModelSMT
            CreateMap<AddressBookEntity, ResponseModelSMT<RequestModel>>().ReverseMap();
        }
    }
}
