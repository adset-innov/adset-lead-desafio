using AdSet.Lead.Core.Exceptions;
using AdSet.Lead.Domain.Entities;
using AdSet.Lead.Domain.Repositories;
using AdSet.Lead.Infrastructure.Data.Database;
using Microsoft.EntityFrameworkCore;

namespace AdSet.Lead.Infrastructure.Data.RepositoriesImpl;

public class VehicleOptionRepository(AppDbContext context) : IVehicleOptionRepository
{
    public async Task SaveAsync()
    {
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<VehicleOption>> GetAllAsync()
    {
        return await context.VehicleOptions.ToListAsync();
    }

    public async Task<VehicleOption> GetByIdAsync(Guid id)
    {
        var option = await context.VehicleOptions.FindAsync(id);

        if (option is null)
            throw new DbNotFoundException($"VehicleOption with id {id} not found.");

        return option;
    }

    public async Task AddAsync(VehicleOption option)
    {
        await context.VehicleOptions.AddAsync(option);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var option = await context.VehicleOptions.FindAsync(id);

        if (option is null)
            throw new DbNotFoundException($"VehicleOption with id {id} not found.");

        context.VehicleOptions.Remove(option);
    }

    public async Task<VehicleOption?> GetByNameAsync(string name)
    {
        return await context.VehicleOptions
            .FirstOrDefaultAsync(o => o.Name.ToLower() == name.ToLower());
    }

    public async Task<IEnumerable<VehicleOption>> SearchByNameAsync(string query)
    {
        return await context.VehicleOptions
            .Where(o => o.Name.ToLower().Contains(query.ToLower()))
            .OrderBy(o => o.Name)
            .ToListAsync();
    }
}