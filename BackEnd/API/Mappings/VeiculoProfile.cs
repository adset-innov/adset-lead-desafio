
using API.Dtos.Veiculo;
using API.Entidades;
using AutoMapper;

namespace API.Mappings
{
    public class VeiculoProfile : Profile
    {
        public VeiculoProfile()
        {

            CreateMap<Veiculo, VeiculoDto>()
                .ForMember(dest => dest.Fotos, opt => opt.MapFrom(src => src.Foto))
                .ForMember(dest => dest.Opcionais, opt => opt.MapFrom(src => src.RelacaoVeiculoOpcional))
                .ForMember(dest => dest.PacotesPortal, opt => opt.MapFrom(src => src.RelacaoVeiculoPacotePortal));

            CreateMap<CadastrarVeiculoDto, Veiculo>()
                .ForMember(dest => dest.RelacaoVeiculoOpcional,
                 opt => opt.MapFrom(src => src.Opcionais != null
                     ? src.Opcionais.Select(id => new RelacaoVeiculoOpcional (id)).ToList()
                     : new List<RelacaoVeiculoOpcional>()));

         
            CreateMap<Veiculo, CadastrarVeiculoDto>()
                .ForMember(dest => dest.Opcionais, opt => opt.MapFrom(src => src.RelacaoVeiculoOpcional
                                                       .Select(r => r.OpcionalId)
                                                       .ToList()));

            CreateMap<Veiculo, AtualizarVeiculoDto>()
               .ForMember(dest => dest.Opcionais, opt => opt.MapFrom(src => src.RelacaoVeiculoOpcional
                                                      .Select(r => r.OpcionalId)
                                                      .ToList()));

            CreateMap<AtualizarVeiculoDto, Veiculo>()                  
                .ForMember(dest => dest.RelacaoVeiculoOpcional, opt => opt.Ignore())
                .ForMember(dest => dest.Foto, opt => opt.Ignore()); 

        }
    }
}
