using GerenciarCarros.Domain.Entities;
using GerenciarCarros.Domain.Pagination;
using GerenciarCarros.Domain.Repositories.Base;

namespace GerenciarCarros.Domain.Repositories
{
    public interface ICarroRepository: IBaseRepository<Carro>
    {
        Task<PaginacaoList<Carro>> Paginacao(int tamanhoPagina, int numeroPagina, 
            string ordenacao = "", string marca = "", string modelo ="", 
            string cor="", decimal? preco = null, string opcionais = ""
            ,int? anoMin = null, int? anoMax = null, string placa="");
        Task<int> Totais(string tipo);
        Task<List<int>> Anos();
    }
}
