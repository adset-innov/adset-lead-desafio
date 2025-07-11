using Adsetdesafio.Data;
using Adsetdesafio.Domain.Infraestructure.Interfaces;
using Adsetdesafio.Domain.Models.Entities;

namespace Adsetdesafio.Domain.Infraestructure.Repositories
{
    public class CarsRepository : BaseRepository<Car>, ICarRepository
    {
        public CarsRepository(AppDbContext context) : base(context)
        {
        }
    }
}
