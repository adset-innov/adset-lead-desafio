namespace AdSet.Lead.Domain.Interfaces;

public interface IRepository<T>
{
    Task SaveAsync();
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(Guid id);
    Task AddAsync(T entity);
    Task DeleteByIdAsync(Guid id);
}