using GerenciarCarros.Domain.Entities;
using GerenciarCarros.Domain.Repositories.Base;

namespace GerenciarCarros.Domain.Repositories
{
    public interface IImagemRepository : IBaseRepository<Imagem>
    {
        Task<IEnumerable<Imagem>> ObterPorIdCarro(Guid id);
    }
}
