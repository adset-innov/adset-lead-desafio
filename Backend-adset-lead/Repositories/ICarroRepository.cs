using Backend_adset_lead.DTOs;
using Backend_adset_lead.Models;

namespace Backend_adset_lead.Repositories
{
    public interface ICarroRepository
    {
        Task<int> Add(Carro carro);
        Task<Carro?> GetById(int id);
        Task<List<Carro>> GetFiltered(CarroRequestDTO filtro);
        Task<int> Delete(Carro carro);
        Task<int> Update(Carro categoria);
    }
}
