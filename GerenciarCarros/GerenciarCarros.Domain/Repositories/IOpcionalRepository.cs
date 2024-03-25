using GerenciarCarros.Domain.Entities;
using GerenciarCarros.Domain.Repositories.Base;

namespace GerenciarCarros.Domain.Repositories
{
    public interface IOpcionalRepository: IBaseRepository<Opcionais>
    {
        Task<IEnumerable<Opcionais>> ObterPorIdCarro(Guid id);
    }
}
