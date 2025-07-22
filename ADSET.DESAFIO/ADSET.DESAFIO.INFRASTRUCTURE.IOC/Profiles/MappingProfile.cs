using ADSET.DESAFIO.APPLICATION.DTOs;
using ADSET.DESAFIO.DOMAIN.Entities;
using AutoMapper;

namespace ADSET.DESAFIO.INFRASTRUCTURE.IOC.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CarCreateDTO, Car>().ForMember(dest => dest.Optionals, opt => opt.MapFrom(src => src.Optionals != null ? src.Optionals.Select(name => new CarOptional { Optional = new Optional(name) }) : Enumerable.Empty<CarOptional>()))
                                          .ForMember(dest => dest.PortalPackages, opt => opt.MapFrom(src => src.PortalPackages != null ? src.PortalPackages.Select(kv => new CarPortalPackage { Portal = kv.Key, Package = kv.Value }) : Enumerable.Empty<CarPortalPackage>()))
                                          .ForMember(dest => dest.Photos, opt => opt.Ignore()).ReverseMap();

            CreateMap<CarUpdateDTO, Car>().ForMember(dest => dest.Optionals, opt => opt.MapFrom(src => src.Optionals != null ? src.Optionals.Select(name => new CarOptional { Optional = new Optional(name) }) : Enumerable.Empty<CarOptional>()))
                                          .ForMember(dest => dest.PortalPackages, opt => opt.MapFrom(src => src.PortalPackages != null ? src.PortalPackages.Select(kv => new CarPortalPackage { Portal = kv.Key, Package = kv.Value }) : Enumerable.Empty<CarPortalPackage>()))
                                          .ForMember(dest => dest.Photos, opt => opt.Ignore()).ReverseMap();
        }
    }
}