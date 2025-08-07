using Backend_adset_lead.DTOs;
using Backend_adset_lead.Models;

namespace Backend_adset_lead.Services
{
    public interface ICarroService
    {
        Task<int> AddAsync(NovoCarroRequestDTO carro);
        Task<PagedListDTO<CarroResponseDTO>> GetFilteredAsync(BuscaCarroRequestDTO filtro);
        Task<int> DeleteAsync(int id);
        Task<int> UpdateAsync(CarroUpdateRequestDTO carro);
    }
}
