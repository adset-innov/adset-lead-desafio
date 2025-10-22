using API.Dtos.Foto;
using API.Entidades;
using AutoMapper;

namespace API.Mappings
{
    public class FotoProfile : Profile
    {
        public FotoProfile() {

            CreateMap<Foto, FotoDto>().ReverseMap();


        }
    }
}
