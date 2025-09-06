using AdSet.Lead.Domain.Repositories;

namespace AdSet.Lead.Application.UseCases.Vehicle;

public class GetTotalCountVehicle(IVehicleRepository repository)
{
    public async Task<GetTotalCountVehicleOutput> Execute()
    {
        var count = await repository.GetTotalCountAsync();
        return new GetTotalCountVehicleOutput(count);
    }
}

public record GetTotalCountVehicleOutput(int TotalCount);