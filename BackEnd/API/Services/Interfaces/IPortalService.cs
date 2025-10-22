using API.Dtos.Portal;

namespace API.Services.Interfaces
{
    public interface IPortalService
    {
        Task<List<PortalDto>> ListarPortaisAsync();
        Task<DetalharPortalDto> ObterPortalPorIDAsync(int id);
    }
}
