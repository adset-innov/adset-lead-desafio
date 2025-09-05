using AdSet.Lead.Domain.Entities;
using AdSet.Lead.Domain.Repositories;
using AdSet.Lead.Infrastructure.Data.Database;

namespace AdSet.Lead.Infrastructure.Data.RepositoriesImpl;

public class PhotoRepository(AppDbContext context) : IPhotoRepository
{
    public Task SaveAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Photo>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Photo> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Photo photo)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Photo entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}