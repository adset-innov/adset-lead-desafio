using AdSet.Lead.Domain.Repositories;

namespace AdSet.Lead.Application.UseCases.Vehicle;

public class GetWithPhotosCountVehicle(IVehicleRepository repository)
{
    public async Task<GetWithPhotosCountVehicleOutput> Execute()
    {
        var count = await repository.GetWithPhotosCountAsync();
        return new GetWithPhotosCountVehicleOutput(count);
    }
}

public record GetWithPhotosCountVehicleOutput(int TotalCount);