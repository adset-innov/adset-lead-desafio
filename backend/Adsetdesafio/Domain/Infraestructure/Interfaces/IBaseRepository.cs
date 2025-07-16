using System.Linq.Expressions;

namespace Adsetdesafio.Domain.Infraestructure.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        T GetEntity(Expression<Func<T, bool>> expression);

        Task<T> GetEntityAsync(Expression<Func<T, bool>> expression);
        Task<T> GetEntityAsync(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IQueryable<T>>? include = null);

        IList<T> GetListOfEntity(Expression<Func<T, bool>> expression);

        Task<IList<T>> GetListOfEntityAsync(Expression<Func<T, bool>> expression);

        bool EntityExist(Expression<Func<T, bool>> expression);

        Task<bool> EntityExistAsync(Expression<Func<T, bool>> expression);
        void Add(T entity);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
