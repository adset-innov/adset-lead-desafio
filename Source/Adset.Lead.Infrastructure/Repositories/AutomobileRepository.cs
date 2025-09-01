using Adset.Lead.Domain.Automobiles;
using Microsoft.EntityFrameworkCore;

namespace Adset.Lead.Infrastructure.Repositories;

internal sealed class AutomobileRepository : Repository<Automobile>, IAutomobileRepository
{
    private readonly DataContext _context;
    
    public AutomobileRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Automobile>> SearchAsync(string? plate = null, string? brand = null, string? model = null, int? yearMin = null,
        int? yearMax = null, string? priceRange = null, bool? hasPhotos = null, string? optionals = null,
        string? color = null, string orderBy = "MarcaModelo", bool orderAsc = true, int page = 1, int pageSize = 100)
    {
        var query = _context.Set<Automobile>().AsQueryable();

        // Aplicar filtros
        if (!string.IsNullOrWhiteSpace(plate))
            query = query.Where(a => a.Plate.Contains(plate));

        if (!string.IsNullOrWhiteSpace(brand))
            query = query.Where(a => a.Brand.Contains(brand));

        if (!string.IsNullOrWhiteSpace(model))
            query = query.Where(a => a.Model.Contains(model));

        if (yearMin.HasValue)
            query = query.Where(a => a.Year >= yearMin.Value);

        if (yearMax.HasValue)
            query = query.Where(a => a.Year <= yearMax.Value);

        if (!string.IsNullOrWhiteSpace(color))
            query = query.Where(a => a.Color.Contains(color));

        if (hasPhotos.HasValue)
        {
            if (hasPhotos.Value)
                query = query.Where(a => a.Photos.Any());
            else
                query = query.Where(a => !a.Photos.Any());
        }

        // Filtro por faixa de preço (formato esperado: "min-max" ou "min+" ou "-max")
        if (!string.IsNullOrWhiteSpace(priceRange))
        {
            if (priceRange.Contains("-"))
            {
                var parts = priceRange.Split('-');
                if (decimal.TryParse(parts[0], out var minPrice))
                    query = query.Where(a => a.Price >= minPrice);
                if (parts.Length > 1 && decimal.TryParse(parts[1], out var maxPrice))
                    query = query.Where(a => a.Price <= maxPrice);
            }
            else if (priceRange.EndsWith("+"))
            {
                var priceStr = priceRange.Replace("+", "");
                if (decimal.TryParse(priceStr, out var minPrice))
                    query = query.Where(a => a.Price >= minPrice);
            }
        }

        // Filtro por opcionais
        if (!string.IsNullOrWhiteSpace(optionals))
        {
            var optionalsList = optionals.Split(',')
                .Select(o => o.Trim())
                .Where(o => Enum.TryParse<OptionalFeatures>(o, true, out _))
                .Select(o => Enum.Parse<OptionalFeatures>(o, true))
                .ToList();

            if (optionalsList.Any())
                query = query.Where(a => optionalsList.All(opt => a.Features.Contains(opt)));
            
        }

        // Ordenação
        query = orderBy.ToLower() switch
        {
            "marcamodelo" => orderAsc ? query.OrderBy(a => a.Brand).ThenBy(a => a.Model) : query.OrderByDescending(a => a.Brand).ThenByDescending(a => a.Model),
            "ano" => orderAsc ? query.OrderBy(a => a.Year) : query.OrderByDescending(a => a.Year),
            "preco" => orderAsc ? query.OrderBy(a => a.Price) : query.OrderByDescending(a => a.Price),
            "km" => orderAsc ? query.OrderBy(a => a.Km) : query.OrderByDescending(a => a.Km),
            _ => orderAsc ? query.OrderBy(a => a.Brand).ThenBy(a => a.Model) : query.OrderByDescending(a => a.Brand).ThenByDescending(a => a.Model)
        };

        // Paginação
        query = query.Skip((page - 1) * pageSize).Take(pageSize);

        return await query.ToListAsync();
    }

    public async Task<int> CountAsync(string? plate = null, string? brand = null, string? model = null, int? yearMin = null,
        int? yearMax = null, string? priceRange = null, bool? hasPhotos = null, string? optionals = null,
        string? color = null)
    {
        var query = _context.Set<Automobile>().AsQueryable();

        // Aplicar filtros (mesmo que no SearchAsync)
        if (!string.IsNullOrWhiteSpace(plate))
            query = query.Where(a => a.Plate.Contains(plate));

        if (!string.IsNullOrWhiteSpace(brand))
            query = query.Where(a => a.Brand.Contains(brand));

        if (!string.IsNullOrWhiteSpace(model))
            query = query.Where(a => a.Model.Contains(model));

        if (yearMin.HasValue)
            query = query.Where(a => a.Year >= yearMin.Value);

        if (yearMax.HasValue)
            query = query.Where(a => a.Year <= yearMax.Value);

        if (!string.IsNullOrWhiteSpace(color))
            query = query.Where(a => a.Color.Contains(color));

        if (hasPhotos.HasValue)
        {
            if (hasPhotos.Value)
                query = query.Where(a => a.Photos.Any());
            else
                query = query.Where(a => !a.Photos.Any());
        }

        // Filtro por faixa de preço
        if (!string.IsNullOrWhiteSpace(priceRange))
        {
            if (priceRange.Contains("-"))
            {
                var parts = priceRange.Split('-');
                if (decimal.TryParse(parts[0], out var minPrice))
                    query = query.Where(a => a.Price >= minPrice);
                if (parts.Length > 1 && decimal.TryParse(parts[1], out var maxPrice))
                    query = query.Where(a => a.Price <= maxPrice);
            }
            else if (priceRange.EndsWith("+"))
            {
                var priceStr = priceRange.Replace("+", "");
                if (decimal.TryParse(priceStr, out var minPrice))
                    query = query.Where(a => a.Price >= minPrice);
            }
        }

        // Filtro por opcionais
        if (!string.IsNullOrWhiteSpace(optionals))
        {
            var optionalsList = optionals.Split(',')
                .Select(o => o.Trim())
                .Where(o => Enum.TryParse<OptionalFeatures>(o, true, out _))
                .Select(o => Enum.Parse<OptionalFeatures>(o, true))
                .ToList();

            if (optionalsList.Any())
            {
                foreach (var optional in optionalsList)
                {
                    query = query.Where(a => a.Features.Contains(optional));
                }
            }
        }

        return await query.CountAsync();
    }

    public async Task<IEnumerable<string>> GetAvailableColorsAsync()
    {
        return await _context.Set<Automobile>()
            .Select(a => a.Color)
            .Distinct()
            .OrderBy(c => c)
            .ToListAsync();
    }

    public async Task<IEnumerable<string>> GetAvailableOptionalsAsync()
    {
        var optionals = await _context.Set<Automobile>()
            .Select(a => a.Features)
            .Distinct()
            .ToListAsync();

        return optionals.Select(o => o.ToString()).OrderBy(o => o);
    }

    public async Task<PortalPackage> GetPortalPackageAsync(Guid automobileId, Portal portal)
    {
        return await _context.Set<PortalPackage>()
            .FirstOrDefaultAsync(pp => pp.AutomobileId == automobileId && pp.Portal == portal)
            ?? throw new InvalidOperationException($"Portal package not found for automobile {automobileId} and portal {portal}");
    }

    public async Task SetPortalPackageAsync(Guid automobileId, Portal portal, Package package)
    {
        var existingPortalPackage = await _context.Set<PortalPackage>()
            .FirstOrDefaultAsync(pp => pp.AutomobileId == automobileId && pp.Portal == portal);
        
        if (existingPortalPackage != null)
        {
            _context.Set<PortalPackage>().Remove(existingPortalPackage);
        }

        var newPortalPackage = new PortalPackage(Guid.NewGuid(), automobileId, portal, package);
        await _context.Set<PortalPackage>().AddAsync(newPortalPackage);
    }

    public Task<IEnumerable<PortalPackage>> GetAvailablePackagesForPortalAsync(PortalPackage portalPackage)
    {
        // Retorna todos os pacotes disponíveis para um portal específico
        // Como Package é um enum, vamos criar uma lista com todos os valores
        var packages = Enum.GetValues<Package>();
        var portalPackages = new List<PortalPackage>();

        foreach (var package in packages)
        {
            portalPackages.Add(new PortalPackage(Guid.NewGuid(), Guid.Empty, portalPackage.Portal, package));
        }

        return Task.FromResult<IEnumerable<PortalPackage>>(portalPackages);
    }

    public async Task AddAsync(Automobile automobile)
    {
        await _context.Set<Automobile>().AddAsync(automobile);
    }

    public Task UpdateAsync(Automobile automobile)
    {
        _context.Set<Automobile>().Update(automobile);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id)
    {
        var automobile = await _context.Set<Automobile>()
            .FirstOrDefaultAsync(a => a.Id == id);
        
        if (automobile != null)
        {
            _context.Set<Automobile>().Remove(automobile);
        }
    }
}