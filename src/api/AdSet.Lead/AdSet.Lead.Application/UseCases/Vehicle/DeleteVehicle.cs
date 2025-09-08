using AdSet.Lead.Domain.Repositories;

namespace AdSet.Lead.Application.UseCases.Vehicle;

public class DeleteVehicle(IVehicleRepository repository)
{
    public async Task<DeleteVehicleOutput> Execute(Guid id)
    {
        await repository.DeleteByIdAsync(id);
        await repository.SaveAsync();

        return new DeleteVehicleOutput(id.ToString());
    }
}

public record DeleteVehicleOutput(string Id);