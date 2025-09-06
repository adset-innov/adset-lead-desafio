using AdSet.Lead.Domain.Repositories;

namespace AdSet.Lead.Application.UseCases.Vehicle;

public class GetWithoutPhotosCountVehicle(IVehicleRepository repository)
{
    public async Task<GetWithoutPhotosCountVehicleOutput> Execute()
    {
        var count = await repository.GetWithoutPhotosCountAsync();
        return new GetWithoutPhotosCountVehicleOutput(count);
    }
}

public record GetWithoutPhotosCountVehicleOutput(int TotalCount);