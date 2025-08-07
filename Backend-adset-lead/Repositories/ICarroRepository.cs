using Backend_adset_lead.DTOs;
using Backend_adset_lead.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend_adset_lead.Repositories
{
    public interface ICarroRepository
    {
        Task<int> Add(Carro carro);
        Task<Carro?> GetById(int id);
        Task<PagedListDTO<Carro>> GetFiltered(BuscaCarroRequestDTO filtro);
        Task<int> Delete(int id);
        Task<int> Update(CarroUpdateRequestDTO carro);
    }
}
