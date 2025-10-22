using API.Contexto;
using API.Dtos.Veiculo;
using API.Entidades;
using API.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace API.Repositorios.Implementacoes
{
    public class VeiculoRepository : IVeiculoRepository
    {
        private readonly AdSetContext _context;

        public VeiculoRepository(AdSetContext context)
        {
            _context = context;
        }

        public async Task<Veiculo> Inserir(Veiculo veiculo)
        {
            await _context.Veiculo.AddAsync(veiculo);
            await _context.SaveChangesAsync();
            return veiculo;
        }

        public async Task Atualizar(Veiculo veiculo)
        {
            _context.Veiculo.Update(veiculo);
            await _context.SaveChangesAsync();
        }

        public async Task Deletar(int id)
        {
            Veiculo veiculoAtual = await _context.Veiculo
                    .Include(v => v.RelacaoVeiculoOpcional)
                    .Include(v => v.RelacaoVeiculoPacotePortal)
                    .FirstAsync(v => v.Id.Equals(id));
        
            _context.Veiculo.Remove(veiculoAtual);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Veiculo>> Listar()
        {
            return await _context.Veiculo
                    .Include(v => v.RelacaoVeiculoOpcional)
                        .ThenInclude(r => r.Opcional)
                    .Include(v => v.Foto)
                    .Include(v => v.RelacaoVeiculoPacotePortal)
                    .ThenInclude(p => p.Pacote)
                    .ThenInclude(p => p.Portal)
                    .ToListAsync();

        }

        public async Task<Veiculo?> ObterPeloId(int id)
        {
            Veiculo? veiculo = await _context.Veiculo
                    .Include(v => v.RelacaoVeiculoOpcional)
                        .ThenInclude(r => r.Opcional)
                    .Include(v => v.Foto)
                    .Include(v => v.RelacaoVeiculoPacotePortal)
                        .ThenInclude(r => r.Pacote)
                        .ThenInclude(r => r.Portal)
                    .FirstOrDefaultAsync(v => v.Id == id);
            return veiculo;
        }

        public async Task<(IEnumerable<Veiculo>, int totalRegistros)> ListarPaginadoAsync(VeiculoFiltroDto filtro)
        {
            IQueryable<Veiculo> query = _context.Veiculo
                .Include(v => v.Foto)
                .Include(v => v.RelacaoVeiculoOpcional)
                    .ThenInclude(r => r.Opcional)
                .Include(v => v.RelacaoVeiculoPacotePortal)
                    .ThenInclude(r => r.Pacote)
                    .ThenInclude(r => r.Portal)
                .AsQueryable();


            if (!string.IsNullOrEmpty(filtro.Marca))
                query = query.Where(v => v.Marca.Contains(filtro.Marca));

            if (!string.IsNullOrEmpty(filtro.Modelo))
                query = query.Where(v => v.Modelo.Contains(filtro.Modelo));

            if (!string.IsNullOrEmpty(filtro.Placa))
                query = query.Where(v => v.Placa.Contains(filtro.Placa));

            if (!string.IsNullOrEmpty(filtro.Opcional))
                query = query.Where(v => v.RelacaoVeiculoOpcional
                             .Any(op => op.Opcional.Descricao.Contains(filtro.Opcional)));

            if (filtro.AnoMin.HasValue)
                query = query.Where(v => v.Ano >= filtro.AnoMin.Value);

            if (filtro.AnoMax.HasValue)
                query = query.Where(v => v.Ano <= filtro.AnoMax.Value);

            if (!string.IsNullOrEmpty(filtro.Cor))
                query = query.Where(v => v.Cor.Contains(filtro.Cor));

            if (filtro.PrecoMin.HasValue)
                query = query.Where(v => v.Preco >= filtro.PrecoMin.Value);

            if (filtro.PrecoMax.HasValue)
                query = query.Where(v => v.Preco <= filtro.PrecoMax.Value);

            if (filtro.Fotos.HasValue) 
            {
                query = query.Where(v => v.Foto.Any() == filtro.Fotos.Value);
            }
           


            int totalRegistros = await query.CountAsync();


            List<Veiculo> veiculos = await query
                .OrderBy(v => v.Id)
                .Skip((filtro.Pagina - 1) * filtro.TamanhoPagina)
                .Take(filtro.TamanhoPagina)
                .ToListAsync();

            return (veiculos, totalRegistros);
        }

        public async Task<List<Opcional>> ListarOpcionais()
        {
            return await _context.Opcional.ToListAsync();
        }

   

        public async Task<List<RelacaoVeiculoPacotePortal>> ObterRelacoesPorVeiculoIds(List<int> veiculoIds)
        {
            return await _context.RelacaoVeiculoPacotePortal
                .Where(r => veiculoIds.Contains(r.VeiculoId))
                .ToListAsync();
        }

   
        public async Task AtualizarRelacoesEmMassa(
            List<RelacaoVeiculoPacotePortal> paraAdicionar,
            List<RelacaoVeiculoPacotePortal> paraRemover)
        {
            if (paraAdicionar.Any())
            {
                await _context.RelacaoVeiculoPacotePortal.AddRangeAsync(paraAdicionar);
            }

            if (paraRemover.Any())
            {
                _context.RelacaoVeiculoPacotePortal.RemoveRange(paraRemover);
            }

            await _context.SaveChangesAsync();
        }

        public async Task AdicionarFoto(Foto foto)
        {
            await _context.Foto.AddAsync(foto);   
        }

        public async Task SalvarAlteracoesAsync()
        {        
            await _context.SaveChangesAsync();
        }

        public async Task<List<string>> ListarCoresDisponiveis()
        {
            List<string> cores = await _context.Veiculo
                                    .Select(v => v.Cor)
                                    .Distinct()
                                    .ToListAsync();
            return cores;
        }

        public async Task<List<Foto>> ListarFotos(int idVeiculo)
        {
            List<Foto> fotos = await _context.Foto.Where(foto=> foto.VeiculoId == idVeiculo).ToListAsync();
            return fotos;
        }

        public async Task EditarFoto(Foto foto)
        {
            _context.Foto.Update(foto);
            await _context.SaveChangesAsync();
        }

        public async Task<Veiculo> ObterVeiculoPorPlaca(string placa)
        {
            Veiculo? veiculo = await _context.Veiculo.Where(v => v.Placa.Equals(placa)).FirstOrDefaultAsync();
            return veiculo;
        }
    }

}
