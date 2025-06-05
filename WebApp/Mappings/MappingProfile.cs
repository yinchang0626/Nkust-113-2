using AutoMapper;
using WebApp.Models;
using WebApp.Models.Dtos;

namespace WebApp.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Card Mappings
            CreateMap<Card, CardDto>()
                .ForMember(dest => dest.AccessGrants, opt => opt.MapFrom(src => src.AccessGrants));
            CreateMap<CreateCardDto, Card>();
            CreateMap<UpdateCardDto, Card>();
            CreateMap<Card, CardBaseDto>(); // For nested DTOs

            // Device Mappings
            CreateMap<Device, DeviceDto>()
                .ForMember(dest => dest.AccessGrants, opt => opt.MapFrom(src => src.AccessGrants));
            CreateMap<CreateDeviceDto, Device>();
            CreateMap<UpdateDeviceDto, Device>();
            CreateMap<Device, DeviceBaseDto>(); // For nested DTOs

            // CardAccessGrant Mappings
            CreateMap<CardAccessGrant, CardAccessGrantDto>()
                .ForMember(dest => dest.Card, opt => opt.MapFrom(src => src.Card))
                .ForMember(dest => dest.Device, opt => opt.MapFrom(src => src.Device));
            CreateMap<CreateCardAccessGrantDto, CardAccessGrant>();
            CreateMap<UpdateCardAccessGrantDto, CardAccessGrant>();
            CreateMap<CardAccessGrant, CardAccessGrantBaseDto>(); // For nested DTOs in CardDto/DeviceDto

        }
    }
}
