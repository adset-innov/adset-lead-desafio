using Backend_adset_lead.DTOs;
using Backend_adset_lead.Models;
using Backend_adset_lead.Repositories;

namespace Backend_adset_lead.Services
{
    public class CarroService : ICarroService
    {
        private readonly ICarroRepository _repository;

        public CarroService(ICarroRepository repository) => _repository = repository;

        public async Task<int> AddAsync(Carro carro)
        {
            return await _repository.Add(carro);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _repository.Delete(id);
        }

        public async Task<List<Carro>> GetFilteredAsync(BuscaCarroRequestDTO filtro)
        {
            return await _repository.GetFiltered(filtro);
        }

        public async Task<int> UpdateAsync(Carro carro)
        {
            return await _repository.Update(carro);
        }
    }
}
