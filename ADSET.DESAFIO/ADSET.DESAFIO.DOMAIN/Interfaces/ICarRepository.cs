using ADSET.DESAFIO.DOMAIN.Entities;

namespace ADSET.DESAFIO.DOMAIN.Interfaces
{
    public interface ICarRepository
    {
        Task CreateAsync(Car car);
        Task DeleteAsync(Car car);
        Task<List<Car>> GetAllAsync();
        Task<Car?> GetByIdAsync(int id);
        Task UpdateAsync(Car car);
    }
}