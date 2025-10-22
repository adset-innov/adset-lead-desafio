using API.Dtos.Pacote;
using API.Entidades;
using AutoMapper;

namespace API.Mappings
{
    public class PacoteProfile : Profile
    {
        public PacoteProfile() {

            CreateMap<Pacote, PacoteDto>().ReverseMap();

        }
    }
}
