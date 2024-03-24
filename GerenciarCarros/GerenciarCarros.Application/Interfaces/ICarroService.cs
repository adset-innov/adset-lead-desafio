using GerenciarCarros.Application.Models;
using GerenciarCarros.Domain.Entities;
using GerenciarCarros.Domain.Entities.Enums;
using GerenciarCarros.Domain.Pagination;

namespace GerenciarCarros.Application.Interfaces
{
    public interface ICarroService
    {
        Task<Carro> Adicionar(CarroModel entity);
        Task<Carro> ObterPorId(Guid id);
        Task<List<Carro>> ObterTodos();
        Task<int> ObterTotais(string tipo);
        Task<Carro> Atualizar(CarroModel entity);
        Task Remover(Guid id);
        Task RemoverImagem(Guid id);
        Task RemoverOpcional(Guid id);
        Task<PaginacaoList<Carro>> Paginacao(PaginacaoCarroModel model);
        Task<bool> UploadImagem(ImagemModel imagens);
        Task<List<int>> Anos();
        Task<PacoteModel> VincularPacote(PacoteModel entity);
        Task RemoverPacote(Guid id, int tipoPacote);

    }
}
