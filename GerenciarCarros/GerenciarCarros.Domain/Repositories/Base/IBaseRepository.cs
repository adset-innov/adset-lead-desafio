
using GerenciarCarros.Domain.Entities.Base;

namespace GerenciarCarros.Domain.Repositories.Base
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : BaseEntity
    {
        Task Adicionar(TEntity entity);
        Task<TEntity> ObterPorId(Guid id);
        Task<List<TEntity>> ObterTodos();
        Task Atualizar(TEntity entity);
        Task Remover(Guid id);
        Task<int> SaveChanges();
    }
}
