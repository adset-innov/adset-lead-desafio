using AdSet.Lead.Domain.Repositories;

namespace AdSet.Lead.Application.UseCases.Vehicle;

public class GetDistinctColorsVehicle(IVehicleRepository repository)
{
    public async Task<GetDistinctColorsVehicleOutput> Execute()
    {
        var colors = await repository.GetDistinctColorsAsync();
        return new GetDistinctColorsVehicleOutput(colors.ToList());
    }
}

public record GetDistinctColorsVehicleOutput(List<string> Colors);