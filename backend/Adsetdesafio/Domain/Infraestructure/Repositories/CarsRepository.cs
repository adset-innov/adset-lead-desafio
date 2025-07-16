using Adsetdesafio.Application.DTOs;
using Adsetdesafio.Data;
using Adsetdesafio.Domain.Infraestructure.Interfaces;
using Adsetdesafio.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Adsetdesafio.Domain.Infraestructure.Repositories
{
    public class CarsRepository : BaseRepository<Car>, ICarRepository
    {
        public CarsRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IList<Car>> GetListByFilterAsync(CarsFilterDTO dto)
        {
            return await _dbSet
          .Include(car => car.PortaisAnuncio)
          .Where(car =>
              (!dto.AnoMin.HasValue || car.Ano >= dto.AnoMin.Value) &&
              (!dto.AnoMax.HasValue || car.Ano <= dto.AnoMax.Value) &&
              (!dto.Preco.HasValue || car.Preco <= dto.Preco.Value) &&
              (!dto.Cor.HasValue || car.Cor == dto.Cor.Value) &&
              (!dto.SomenteComFotos.HasValue ||
                  (dto.SomenteComFotos.Value
                      ? car.Fotos != null && car.Fotos.Any(f => !string.IsNullOrWhiteSpace(f))
                      : car.Fotos == null || !car.Fotos.Any(f => !string.IsNullOrWhiteSpace(f)))) &&

              (dto.Opcionais == null || dto.Opcionais.All(op => car.OpcionaisVeiculo.Contains(op))) &&

              (dto.PortaisECategorias == null || dto.PortaisECategorias.All(pc =>
                  car.PortaisAnuncio.Any(pa =>
                      pa.NomePortal == pc.Key && pa.Categoria == pc.Value)))
          )
          .ToListAsync();

        }

        public async Task<ResumeVehicleDTO> GetResumoVeiculosAsync()
        {
            var query = _dbSet.Select(car => new
            {
                TemFoto = car.Fotos != null && car.Fotos.Any(f => !string.IsNullOrWhiteSpace(f))
            });

            var total = await query.CountAsync();
            var comFotos = await query.CountAsync(c => c.TemFoto);

            return new ResumeVehicleDTO
            {
                Total = total,
                ComFotos = comFotos,
                SemFotos = total - comFotos
            };
        }
    }
}
