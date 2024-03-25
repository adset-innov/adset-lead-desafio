using GerenciarCarros.Application.Models;
using GerenciarCarros.Domain.Entities;

namespace GerenciarCarros.Application.Interfaces
{
    public interface IImagemService
    {
        Task Adicionar(ImagemModel entity); 
        Task Remover(Guid id);
        Task<Imagem> ObterPorId(Guid id);
        Task<List<ImagemModel>> ObterPorIdCarro(Guid id);
    }
}
