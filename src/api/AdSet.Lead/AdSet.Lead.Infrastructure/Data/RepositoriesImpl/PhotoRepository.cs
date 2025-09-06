using AdSet.Lead.Core.Exceptions;
using AdSet.Lead.Domain.Entities;
using AdSet.Lead.Domain.Repositories;
using AdSet.Lead.Infrastructure.Data.Database;
using Microsoft.EntityFrameworkCore;

namespace AdSet.Lead.Infrastructure.Data.RepositoriesImpl;

public class PhotoRepository(AppDbContext context) : IPhotoRepository
{
    public async Task SaveAsync()
    {
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Photo>> GetAllAsync()
    {
        return await context.Photos.ToListAsync();
    }

    public async Task<Photo> GetByIdAsync(Guid id)
    {
        var photo = await context.Photos.FirstOrDefaultAsync(p => p.Id == id);

        if (photo is null)
            throw new DbNotFoundException($"Photo with id {id} not found.");

        return photo;
    }

    public async Task AddAsync(Photo photo)
    {
        await context.Photos.AddAsync(photo);
    }

    public Task UpdateAsync(Photo entity)
    {
        context.Photos.Update(entity);
        return Task.CompletedTask;
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var photo = await context.Photos.FindAsync(id);

        if (photo is null)
            throw new DbNotFoundException($"Photo with id {id} not found.");

        context.Photos.Remove(photo);
    }
}