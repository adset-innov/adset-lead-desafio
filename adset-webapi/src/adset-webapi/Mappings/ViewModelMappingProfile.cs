using AdSet.Application.ViewModels;
using AdSet.Domain.Entities;
using AutoMapper;

namespace adset_webapi.Mappings
{
    public class ViewModelMappingProfile : Profile
    {
        public ViewModelMappingProfile() 
        {
            CreateMap<CreateUpdateVehicleViewModel, Vehicle>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.VehicleOptionals, opt => opt.Ignore())
                .ForMember(dest => dest.VehicleImages, opt => opt.Ignore())
                .ForMember(dest => dest.VehiclePortalPackages, opt => opt.Ignore());


            CreateMap<Vehicle, VehicleResponseViewModel>()
                .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.VehicleImages.Select(img => img.ImageUrl).ToList()))
                .ForMember(dest => dest.OptionalNames, opt => opt.MapFrom(src => src.VehicleOptionals.Select(vo => vo.Optional.Name).ToList()))
                .ForMember(dest => dest.PortalPackages, opt => opt.MapFrom(src => src.VehiclePortalPackages.Select(vpp => new PortalPackageSelectionViewModel
                {
                    PortalName = vpp.Portal.Name,
                    PackageName = vpp.Package.Name
                }).ToList()));
        }
    }
}
