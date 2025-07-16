using Adsetdesafio.Application.DTOs;
using Adsetdesafio.Domain.Models.Entities;

namespace Adsetdesafio.Domain.Infraestructure.Interfaces
{
    public interface ICarRepository : IBaseRepository<Car>
    {
       Task<IList<Car>> GetListByFilterAsync(CarsFilterDTO dto);
        Task<ResumeVehicleDTO> GetResumoVeiculosAsync();
    }
}
