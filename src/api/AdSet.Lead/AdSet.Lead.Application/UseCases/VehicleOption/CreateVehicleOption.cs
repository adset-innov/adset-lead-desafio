using AdSet.Lead.Domain.Repositories;

namespace AdSet.Lead.Application.UseCases.VehicleOption;

public class CreateVehicleOption(IVehicleOptionRepository repository)
{
    public async Task<CreateVehicleOptionOutput> Execute(CreateVehicleOptionInput input)
    {
        var existing = await repository.GetByNameAsync(input.Name);
        if (existing is not null)
            return new CreateVehicleOptionOutput(existing.Id, false);

        var option = new Domain.Entities.VehicleOption(input.Name);

        await repository.AddAsync(option);
        await repository.SaveAsync();

        return new CreateVehicleOptionOutput(option.Id, true);
    }
}

public record CreateVehicleOptionInput(string Name);

public record CreateVehicleOptionOutput(Guid Id, bool Created);