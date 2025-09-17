using AdSet.Lead.Domain.Repositories;

namespace AdSet.Lead.Application.UseCases.VehicleOption;

public class SearchVehicleOptions(IVehicleOptionRepository repository)
{
    public async Task<IEnumerable<SearchVehicleOptionOutput>> Execute(string query)
    {
        var results = await repository.SearchByNameAsync(query);

        return results.Select(o => new SearchVehicleOptionOutput(o.Id, o.Name));
    }
}

public record SearchVehicleOptionOutput(Guid Id, string Name);