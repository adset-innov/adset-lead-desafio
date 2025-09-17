using AdSet.Lead.Domain.Entities;
using AdSet.Lead.Domain.Repositories;

namespace AdSet.Lead.Application.Services;

public class VehicleOptionService(IVehicleOptionRepository optionRepository)
{
    public async Task<List<VehicleOption>> ResolveOptionsAsync(IEnumerable<string>? optionNames)
    {
        var options = new List<VehicleOption>();

        var names = optionNames?.ToList();

        if (names is null || names.Count == 0)
            return options;

        foreach (var name in names)
        {
            var existing = await optionRepository.GetByNameAsync(name);
            if (existing is not null)
            {
                options.Add(existing);
            }
            else
            {
                var newOption = new VehicleOption(name);
                await optionRepository.AddAsync(newOption);
                options.Add(newOption);
            }
        }

        await optionRepository.SaveAsync();
        return options;
    }
}