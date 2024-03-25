using GerenciarCarros.Application.Models;
using GerenciarCarros.Domain.Entities;

namespace GerenciarCarros.Application.Interfaces
{
    public interface IOptionalService
    {
        Task Adicionar(OpcionalModel entity);
        Task Remover(Guid id);
        Task<Opcionais> ObterPorId(Guid id);
        Task<List<OpcionalModel>> ObterPorIdCarro(Guid id);
    }
}
