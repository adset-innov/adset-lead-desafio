using API.Dtos.Portal;
using API.Entidades;
using API.Exceptions;
using API.Repositorios.Interfaces;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services.Implementacoes
{
    public class PortalService : IPortalService
    {
        private IPortalRepository _repository;
        private IMapper _mapper;
        public PortalService(IPortalRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<List<PortalDto>> ListarPortaisAsync()
        {
            List<Portal> portais = await _repository.Listar();
            List<PortalDto> listaPortalDtos = _mapper.Map<List<PortalDto>>(portais);
            return listaPortalDtos;
        }

        public async Task<DetalharPortalDto> ObterPortalPorIDAsync(int id)
        {
            Portal? portal = await _repository.ObterPorId(id);
            if (portal == null)
            {
                throw new NotFoundException("Portal não encontrado");
            }

            DetalharPortalDto detalharPortalDto = _mapper.Map<DetalharPortalDto>(portal);
            return detalharPortalDto;
        }
    }
}
