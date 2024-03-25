using GerenciarCarros.Domain.Entities;
using GerenciarCarros.Domain.Pagination;
using GerenciarCarros.Domain.Repositories;
using GerenciarCarros.Infrastructure.Context;
using GerenciarCarros.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace GerenciarCarros.Infrastructure.Repositories
{
    public class CarroRepository : BaseRepository<Carro>, ICarroRepository
    {
        public int Quantidade { get; set; }
        public CarroRepository(CarrosDBContext context) : base(context) { }

        public async Task<List<int>> Anos()
        {
            var anos = Db.Carros.OrderBy(x=> x.Ano).Select(c => c.Ano).Distinct().ToListAsync();
            return await anos;
        }

        public async Task<PaginacaoList<Carro>> Paginacao(int tamanhoPagina, int numeroPagina, string ordenacao = "", string marca = "", string modelo = "", string cor = "", decimal? preco = null, string opcionais = "", int? anoMin = null, int? anoMax = null, string placa = "")
        {

            Quantidade = Db.Carros.Count();

            var items = tamanhoPagina > 0 && numeroPagina > 0 ? ListaCarro(tamanhoPagina, numeroPagina, ordenacao, marca, modelo, cor, preco, opcionais, anoMin, anoMax, placa) :
                Db.Carros.Include(x => x.Opcionais).Include(x => x.Imagens).AsNoTracking().ToListAsync();
            
            return new PaginacaoList<Carro>(await items, Quantidade, numeroPagina, tamanhoPagina);
        }

        private async Task<List<Carro>> ListaCarro(int tamanhoPagina, int numeroPagina, string ordenacao = "", string marca = "", string modelo = "", string cor = "", decimal? preco = null, string opcionais = "", int? anoMin = null, int? anoMax = null, string placa= "")
        {
            var items = await Db.Carros.Include(x => x.Opcionais)
                .Include(x => x.Imagens).Include(x=> x.Pacotes)
                .Where(x=> !string.IsNullOrEmpty(marca) ? x.Marca.ToLower().Contains(marca.ToLower()): x.Marca != null)
                .Where(x => !string.IsNullOrEmpty(modelo) ? x.Modelo.ToLower().Contains(modelo.ToLower()) : x.Modelo != null)
                .Where(x=> !string.IsNullOrEmpty(cor) ? x.Cor.ToLower().Contains(cor.ToLower()) : x.Cor != null)
                .Where(x=> !string.IsNullOrEmpty(placa) ? x.Placa.ToLower().Contains(placa) : x.Placa.ToLower() != null)
                .Where(x=> preco != null ? x.Preco == preco : x.Preco != null)
                .Where(x=> !string.IsNullOrEmpty(opcionais) ? x.Opcionais.Any(x => x.Descricao.ToLower().Contains(opcionais.ToLower())): x.Opcionais == null || x.Opcionais != null)
                .Where(x=> anoMin != null && anoMax != null ? x.Ano >= anoMin && x.Ano <= anoMax : x.Ano != null)
                .Where(x=> anoMin != null && anoMax == null ? x.Ano == anoMin : x.Ano != null)
                .Where(x=> anoMax != null && anoMin == null ? x.Ano == anoMax: x.Ano!= null)
                .ToListAsync();            

            if (!string.IsNullOrEmpty(ordenacao))
            {
                if (ordenacao == "Marca")
                    items = items.OrderBy(x => x.Marca).ToList();
                if (ordenacao == "Ano")
                    items = items.OrderBy(x => x.Ano).ToList();
                if (ordenacao == "Preco")
                    items = items.OrderBy(x => x.Preco).ToList();
                if (ordenacao == "Foto")
                    items = items.OrderBy(x => x.Imagens.Any()).ToList();
            }

            Quantidade = items.Count();

            return items.Skip((numeroPagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina).ToList();
        }

        public async Task<int> Totais(string tipo)
        {
            int quantidade = 0;
            if(tipo == "totalComFoto")
                quantidade = await Db.Carros.Where(x=> x.Imagens.Any()).CountAsync();
            else if (tipo == "totalSemFoto")
                quantidade = await Db.Carros.Where(x => !x.Imagens.Any()).CountAsync();
            else
                quantidade = await Db.Carros.CountAsync();
            return quantidade;
        }
    }
}
