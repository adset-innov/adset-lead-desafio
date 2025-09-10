using AdSet.Application.ViewModels;
using AdSet.Domain.Entities;
using AutoMapper;

namespace adset_webapi.Mappings
{
    public class ViewModelMappingProfile : Profile
    {
        public ViewModelMappingProfile() 
        {
            CreateMap<Vehicle, VehiclesViewModel>();
        }
    }
}
