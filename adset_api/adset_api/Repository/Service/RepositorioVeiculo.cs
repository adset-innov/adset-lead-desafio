using adset_api.Data;
using adset_api.DTOs;
using adset_api.Enum;
using adset_api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace adset_api.Repository.Service
{
    public class RepositorioVeiculo : IVeiculoRepositorio
    {
        private readonly ContextAdSet _context;

        public RepositorioVeiculo(ContextAdSet context) => _context = context;


        public async Task<Veiculo?> ConsultarPorId(int id)
        {
            return await _context.Veiculos
                .Include(c => c.Pacote)
                .Include(c => c.FotosVeiculo)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.IdVeiculo == id);
        }
        public async Task<ListaPaginasDTO<Veiculo>> ConsultarComFiltro(BuscarVeiculoRequestDTO filtro)
        {
            var veiculos = _context.Veiculos
                .AsQueryable()
                .Include(c => c.FotosVeiculo)
                .Include(c => c.Pacote)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(filtro.Placa))
            {
                var placa = filtro.Placa.Trim().ToUpper();
                veiculos = veiculos.Where(c => c.Placa.ToUpper() == placa);
            }

            if (!string.IsNullOrWhiteSpace(filtro.Marca))
            {
                var marca = filtro.Marca.Trim().ToUpper();
                veiculos = veiculos.Where(c => c.Marca.ToUpper() == marca);
            }

            if (!string.IsNullOrWhiteSpace(filtro.Modelo))
            {
                var modelo = filtro.Modelo.Trim().ToUpper();
                veiculos = veiculos.Where(c => c.Modelo.ToUpper() == modelo);
            }

            if (filtro.AnoMin.HasValue) veiculos = veiculos.Where(c => c.Ano >= filtro.AnoMin.Value);

            if (filtro.AnoMax.HasValue) veiculos = veiculos.Where(c => c.Ano <= filtro.AnoMax.Value);

            if (filtro.PrecoMin.HasValue) veiculos = veiculos.Where(c => c.Preco >= filtro.PrecoMin.Value);

            if (filtro.PrecoMax.HasValue) veiculos = veiculos.Where(c => c.Preco <= filtro.PrecoMax.Value);

            if (filtro.PacoteIcarros.HasValue)
                veiculos = veiculos.Where(c => c.Pacote.Any(p => p.TipoPortal == ETipoPortal.iCarros && p.TipoPacote == filtro.PacoteIcarros.Value));

            if (filtro.PacoteWebmotors.HasValue)
                veiculos = veiculos.Where(c => c.Pacote.Any(p => p.TipoPortal == ETipoPortal.WebMotors && p.TipoPacote == filtro.PacoteWebmotors.Value));


            if (filtro.HasPhotos.HasValue)
            {
                veiculos = filtro.HasPhotos.Value ?
                    veiculos.Where(c => c.FotosVeiculo.Any()) :
                    veiculos.Where(c => !c.FotosVeiculo.Any());
            }

            if (!string.IsNullOrWhiteSpace(filtro.Cor))
            {
                var cor = filtro.Cor.Trim().ToUpper();
                veiculos = veiculos.Where(c => c.Cor.ToUpper() == cor);
            }

            if (!string.IsNullOrWhiteSpace(filtro.Opcionais))
            {
                var opcionalBusca = filtro.Opcionais!.Trim();

                veiculos = veiculos.Where(c =>
                    EF.Functions.Like(" " + (c.ListaOpcionais ?? "") + " ", $"% {opcionalBusca} %")
                    || EF.Functions.Like(c.ListaOpcionais ?? "", $"%{opcionalBusca}%")
                );
            }

            veiculos = veiculos.OrderBy(c => c.IdVeiculo);
            var skip = (Math.Max(filtro.Page, 1) - 1) * Math.Max(filtro.PageSize, 1);
            var result = await veiculos.Skip(skip).Take(filtro.PageSize).ToListAsync();

            var totalVeiculos = await veiculos.CountAsync();
            var veiculosComFotos = await veiculos.Where(c => c.FotosVeiculo.Any()).CountAsync();
            var veiculosSemFotos = await veiculos.Where(c => !c.FotosVeiculo.Any()).CountAsync();

            var listaCores = await veiculos.Select(c => c.Cor).Distinct().OrderBy(c => c).ToListAsync();

            var totalCarrosFiltrados = await veiculos.CountAsync();
            int totalPaginas = (totalCarrosFiltrados + filtro.PageSize - 1)
                 / filtro.PageSize;

            return new ListaPaginasDTO<Veiculo>
            {
                Items = result,
                TotalPaginas = totalPaginas,
                TotalCarrosCadastrados = totalVeiculos,
                TotalCarrosFiltrados = totalCarrosFiltrados,
                TotalCarrosComFotos = veiculosComFotos,
                TotalCarrosSemFotos = veiculosSemFotos,
                Cores = listaCores!,
            };
        }

        public async Task<int> AddContext(Veiculo veiculo)
        {
            _context.Veiculos.Add(veiculo);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateContext(VeiculoUpdateRequestDTO veiculoNovo)
        {
            var veiculoExistente = _context.Veiculos
                .Include(c => c.Pacote)
                .Include(c => c.FotosVeiculo)
                .FirstOrDefault(c => c.IdVeiculo == veiculoNovo.IdVeiculo);

            _context.Entry(veiculoExistente).CurrentValues.SetValues(veiculoNovo);

            if(veiculoNovo.Fotos.Any())
            {
                // Remove fotos que não estão mais na lista
                var fotosParaRemover = veiculoExistente!.FotosVeiculo
                    .Where(f => !veiculoNovo.Fotos.Any(nf => nf.IdFotoVeiculo == f.IdFoto))
                    .ToList();
                _context.FotosVeiculo.RemoveRange(fotosParaRemover);
                // Adiciona ou atualizar fotos
                foreach (var novaFoto in veiculoNovo.Fotos)
                {
                    var fotoExistente = veiculoExistente.FotosVeiculo
                        .FirstOrDefault(f => f.IdFoto == novaFoto.IdFotoVeiculo);
                    if (fotoExistente != null)
                    {
                        // Atualiza foto existente
                        _context.Entry(fotoExistente).CurrentValues.SetValues(novaFoto);
                    }
                    else
                    {
                        // Adiciona nova foto
                        var fotoToAdd = new FotoVeiculo
                        {
                            IdVeiculo = veiculoExistente.IdVeiculo,
                            CaminhoUrl = novaFoto.CaminhoUrl
                        };
                        veiculoExistente.FotosVeiculo.Add(fotoToAdd);
                    }
                }
            }
            else
            {
                // Se a lista de fotos for vazia, remove todas as fotos existentes
                _context.FotosVeiculo.RemoveRange(veiculoExistente!.FotosVeiculo);
            }

            if(veiculoNovo.PortalPacotes.Any())
            {
                // Remove pacotes que não estão mais na lista
                var pacotesParaRemover = veiculoExistente!.Pacote
                    .Where(p => !veiculoNovo.PortalPacotes.Any(np => np.IdPacote == p.IdPacote))
                    .ToList();
                _context.Pacotes.RemoveRange(pacotesParaRemover);
                // Adiciona ou atualizar pacotes
                foreach (var novoPacote in veiculoNovo.PortalPacotes)
                {
                    var pacoteExistente = veiculoExistente.Pacote
                        .FirstOrDefault(p => p.IdPacote == novoPacote.IdPacote);
                    if (pacoteExistente != null)
                    {
                        // Atualiza pacote existente
                        _context.Entry(pacoteExistente).CurrentValues.SetValues(novoPacote);
                    }
                    else
                    {
                        // Adiciona novo pacote
                        var pacoteToAdd = new Pacote
                        {
                            IdVeiculo = veiculoExistente.IdVeiculo,
                            TipoPacote = novoPacote.TipoPacote,
                            TipoPortal = novoPacote.TipoPortal
                        };
                        veiculoExistente.Pacote.Add(pacoteToAdd);
                    }
                }
            }
            else
            {
                // Se a lista de pacotes for vazia, remove todos os pacotes existentes
                _context.Pacotes.RemoveRange(veiculoExistente!.Pacote);
            }

            return await _context.SaveChangesAsync();
        }
        public async Task<int> Delete(int id)
        {
            var veiculo = await _context.Veiculos.Where(c => c.IdVeiculo == id).FirstOrDefaultAsync();
            if(veiculo == null)
                throw new Exception("Veículo não existe ou não foi encontrado para exclusão.");

            _context.Veiculos.Remove(veiculo);
            return await _context.SaveChangesAsync();
        }
    }
}
