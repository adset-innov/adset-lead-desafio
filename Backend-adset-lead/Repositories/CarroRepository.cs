using Backend_adset_lead.Contexts;
using Backend_adset_lead.DTOs;
using Backend_adset_lead.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Backend_adset_lead.Repositories
{
    public class CarroRepository : ICarroRepository
    {
        private readonly AdsetLeadContext _context;

        public CarroRepository(AdsetLeadContext context)
        {
            _context = context;
        }

        public async Task<int> Add(Carro carro)
        {
            _context.Carros.Add(carro);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            var carroEncontrado = await GetById(id);
            if (carroEncontrado is null) CarroNaoencontradoException();

            _context.Carros.Remove(carroEncontrado!);
            return await _context.SaveChangesAsync();
        }

        public async Task<Carro?> GetById(int id)
        {
            return await _context.Carros.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Carro>> GetFiltered(CarroRequestDTO filtro)
        {
            var query = _context.Carros
                .AsQueryable()
                .Include(c => c.Fotos)
                .Include(c => c.ListaOpcionais)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(filtro.Placa))
            {
                var placa = filtro.Placa.Trim().ToUpper();
                query = query.Where(c => c.Placa.ToUpper() == placa);
            }

            if (!string.IsNullOrWhiteSpace(filtro.Marca))
            {
                var marca = filtro.Marca.Trim().ToUpper();
                query = query.Where(c => c.Marca.ToUpper() == marca);
            }

            if (!string.IsNullOrWhiteSpace(filtro.Modelo))
            {
                var modelo = filtro.Modelo.Trim().ToUpper();
                query = query.Where(c => c.Modelo.ToUpper() == modelo);
            }

            if (filtro.AnoMin.HasValue) query = query.Where(c => c.Ano >= filtro.AnoMin.Value);

            if (filtro.AnoMax.HasValue) query = query.Where(c => c.Ano <= filtro.AnoMax.Value);

            if (filtro.PrecoMin.HasValue) query = query.Where(c => c.Preco >= filtro.PrecoMin.Value);

            if (filtro.PrecoMax.HasValue) query = query.Where(c => c.Preco <= filtro.PrecoMax.Value);

            if (filtro.HasPhotos.HasValue)
            {
                query = filtro.HasPhotos.Value ?
                    query.Where(c => c.Fotos.Any()) :
                    query.Where(c => !c.Fotos.Any());
            }

            if (!string.IsNullOrWhiteSpace(filtro.Cor))
            {
                var cor = filtro.Cor.Trim().ToUpper();
                query = query.Where(c => c.Cor.ToUpper() == cor);
            }

            if (!string.IsNullOrWhiteSpace(filtro.Opcionais))
            {
                var opcionalBusca = filtro.Opcionais!.Trim();

                query = query.Where(c =>
                    EF.Functions.Like(" " + (c.ListaOpcionais ?? "") + " ", $"% {opcionalBusca} %")
                    || EF.Functions.Like(c.ListaOpcionais ?? "", $"%{opcionalBusca}%")
                );
            }

            query = query.OrderBy(c => c.Id);
            var skip = (Math.Max(filtro.Page, 1) - 1) * Math.Max(filtro.PageSize, 1);
            return await query.Skip(skip).Take(filtro.PageSize).ToListAsync();
        }

        public async Task<int> Update(Carro carro)
        {
            var carroEncontrado = await GetById(carro.Id);
            if (carroEncontrado is null) CarroNaoencontradoException();

            _context.Carros.Update(carroEncontrado!);
            return await _context.SaveChangesAsync();
        }

        private static void CarroNaoencontradoException()
        {
            throw new ArgumentNullException("Carro não encontrado");
        }
    }
}
