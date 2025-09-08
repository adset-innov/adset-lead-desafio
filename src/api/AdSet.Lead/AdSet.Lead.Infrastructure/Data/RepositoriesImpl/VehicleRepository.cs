using AdSet.Lead.Core.Exceptions;
using AdSet.Lead.Domain.Entities;
using AdSet.Lead.Domain.Filters;
using AdSet.Lead.Domain.Repositories;
using AdSet.Lead.Domain.VOs;
using AdSet.Lead.Infrastructure.Data.Database;
using AdSet.Lead.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AdSet.Lead.Infrastructure.Data.RepositoriesImpl;

public class VehicleRepository(AppDbContext context) : IVehicleRepository
{
    public async Task SaveAsync()
    {
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Vehicle>> GetAllAsync()
    {
        return await context.Vehicles
            .Include(v => v.Photos)
            .ToListAsync();
    }

    public async Task<Vehicle> GetByIdAsync(Guid id)
    {
        var vehicle = await context.Vehicles
            .Include(v => v.Photos)
            .FirstOrDefaultAsync(v => v.Id == id);

        if (vehicle is null)
            throw new DbNotFoundException($"Vehicle with id {id} not found.");

        return vehicle;
    }

    public async Task AddAsync(Vehicle vehicle)
    {
        await context.Vehicles.AddAsync(vehicle);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var vehicle = await context.Vehicles.FindAsync(id);

        if (vehicle is null)
            throw new DbNotFoundException($"Vehicle with id {id} not found.");

        context.Vehicles.Remove(vehicle);
    }

    public async Task<int> GetTotalCountAsync()
    {
        return await context.Vehicles.CountAsync();
    }

    public async Task<int> GetWithPhotosCountAsync()
    {
        return await context.Vehicles
            .CountAsync(v => v.Photos.Any());
    }

    public async Task<int> GetWithoutPhotosCountAsync()
    {
        return await context.Vehicles
            .CountAsync(v => !v.Photos.Any());
    }

    public async Task<IEnumerable<string>> GetDistinctColorsAsync()
    {
        return await context.Vehicles
            .Select(v => v.Color.Value)
            .Distinct()
            .ToListAsync();
    }

    public async Task<PagedResult<Vehicle>> SearchAsync(VehicleSearchFilter filter)
    {
        var query = context.Vehicles
            .Include(v => v.Photos)
            .ApplyFilters(filter);

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((filter.Pagination.PageNumber - 1) * filter.Pagination.PageSize)
            .Take(filter.Pagination.PageSize)
            .ToListAsync();

        return new PagedResult<Vehicle>(
            items,
            totalCount,
            filter.Pagination.PageNumber,
            filter.Pagination.PageSize
        );
    }
}