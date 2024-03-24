using GerenciarCarros.Domain.Entities;
using GerenciarCarros.Domain.Entities.Enums;
using GerenciarCarros.Domain.Repositories.Base;

namespace GerenciarCarros.Domain.Repositories
{
    public interface IPacoteRepository : IBaseRepository<Pacote>
    {
        Task<IEnumerable<Pacote>> ObterPorIdCarro(Guid id);
        Task<IEnumerable<Pacote>> ObterPorIdCarroEPacote(Guid id, TipoPacote tipo);
    }
}
