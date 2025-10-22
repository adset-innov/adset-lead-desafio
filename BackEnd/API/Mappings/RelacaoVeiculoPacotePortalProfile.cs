using API.Dtos.RelacaoVeiculoPacotePortal;
using API.Entidades;
using AutoMapper;

namespace API.Mappings
{
    public class RelacaoVeiculoPacotePortalProfile : Profile
    {
        public RelacaoVeiculoPacotePortalProfile() {


            CreateMap<RelacaoVeiculoPacotePortal, RelacaoVeiculoPacotePortalDto>()
                .ForMember(dest => dest.PacoteId, opt => opt.MapFrom(src => src.PacoteId))
                .ForMember(dest => dest.NomePacote, opt => opt.MapFrom(src => src.Pacote.Nome))
                .ForMember(dest => dest.NomePortal, opt => opt.MapFrom(src => src.Pacote.Portal.Nome))
                .ForMember(dest => dest.IdPortal, opt => opt.MapFrom(src => src.Pacote.Portal.Id));

            CreateMap<AtualizarRelacaoVeiculoPacotePortalDto, RelacaoVeiculoPacotePortal>();

        }
    }
}
