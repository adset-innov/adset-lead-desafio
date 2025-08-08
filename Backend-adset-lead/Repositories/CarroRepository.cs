using Backend_adset_lead.Contexts;
using Backend_adset_lead.DTOs;
using Backend_adset_lead.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend_adset_lead.Repositories
{
    public class CarroRepository : ICarroRepository
    {
        private readonly AdsetLeadContext _context;

        public CarroRepository(AdsetLeadContext context) => _context = context;

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
            return await _context.Carros
                .Include(c => c.PortalPacotes)
                .Include(c => c.Fotos)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<PagedListDTO<Carro>> GetFiltered(BuscaCarroRequestDTO filtro)
        {
            var query = _context.Carros
                .AsQueryable()
                .Include(c => c.Fotos)
                .Include(c => c.PortalPacotes)
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
            var result = await query.Skip(skip).Take(filtro.PageSize).ToListAsync();

            var totalItens = await query.CountAsync();
            var totalPaginas = (int)Math.Ceiling((double)totalItens / filtro.PageSize);

            return new PagedListDTO<Carro>
            {
                Items = result,
                TotalPages = totalPaginas
            };
        }

        public async Task<int> Update(CarroUpdateRequestDTO carroAtualizado)
        {
            var carroExistente = await _context.Carros
                .Include(c => c.Fotos)
                .Include(c => c.PortalPacotes)
                .FirstOrDefaultAsync(c => c.Id == carroAtualizado.Id);
            if (carroExistente == null)
                throw new Exception("Carro não encontrado.");

            _context.Entry(carroExistente).CurrentValues.SetValues(carroAtualizado);

            carroExistente.Fotos.RemoveAll(f => !carroAtualizado.Fotos.Any(nf => nf.Id == f.Id));
            foreach (var novaFoto in carroAtualizado.Fotos.Where(nf => nf.Id == 0))
            {
                carroExistente.Fotos.Add(new Foto { Url = novaFoto.Url });
            }
            
            foreach (var fotoAtualizada in carroAtualizado.Fotos.Where(nf => nf.Id != 0))
            {
                var fotoExistente = carroExistente.Fotos.FirstOrDefault(f => f.Id == fotoAtualizada.Id);
                if (fotoExistente != null)
                {
                    _context.Entry(fotoExistente).CurrentValues.SetValues(fotoAtualizada);
                }
            }

            carroExistente.PortalPacotes.RemoveAll(p => !carroAtualizado.PortalPacotes.Any(np => np.Id == p.Id));
            foreach (var novoPacote in carroAtualizado.PortalPacotes.Where(np => np.Id == 0))
            {
                carroExistente.PortalPacotes.Add(new PortalPacote
                {
                    Pacote = novoPacote.Pacote,
                    Portal = novoPacote.Portal
                });
            }

            foreach (var pacoteAtualizado in carroAtualizado.PortalPacotes.Where(np => np.Id != 0))
            {
                var pacoteExistente = carroExistente.PortalPacotes.FirstOrDefault(p => p.Id == pacoteAtualizado.Id);
                if (pacoteExistente != null)
                {
                    _context.Entry(pacoteExistente).CurrentValues.SetValues(pacoteAtualizado);
                }
            }

            return await _context.SaveChangesAsync();
        }

        private static void CarroNaoencontradoException()
        {
            throw new ArgumentNullException("Carro não encontrado");
        }
    }
}
