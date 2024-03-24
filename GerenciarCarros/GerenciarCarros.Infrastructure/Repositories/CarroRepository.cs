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
                .Include(x => x.Imagens).Include(x=> x.Pacotes).ToListAsync();

            if(!string.IsNullOrEmpty(marca))
            {
                items = items.Where(x => x.Marca.Contains(marca)).ToList();
            }

            if (!string.IsNullOrEmpty(modelo))
            {
                items = items.Where(x => x.Modelo.Contains(modelo)).ToList();
            }

            if (!string.IsNullOrEmpty(cor))
            {
                items = items.Where(x => x.Cor.Contains(cor)).ToList();
            }

            if (!string.IsNullOrEmpty(placa))
            {
                items = items.Where(x => x.Placa.Contains(placa)).ToList();
            }

            if (preco != null)
            {
                items = items.Where(x => x.Preco == preco).ToList();
            }

            if (!string.IsNullOrEmpty(opcionais))
            {
                items = items.Where(x => x.Opcionais.Any(x=> x.Descricao.Contains(opcionais))).ToList();
            }

            if (anoMin != null && anoMax != null)
            {
                items = items.Where(x => x.Ano >= anoMin && x.Ano <= anoMax).ToList();
            }
            else if (anoMin != null)
            {
                items = items.Where(x => x.Ano == anoMin).ToList();
            }
            else if (anoMax != null) {
                items = items.Where(x => x.Ano == anoMax).ToList();
            }

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
