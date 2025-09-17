using AdSet.Lead.Core.Validators;
using AdSet.Lead.Domain.Interfaces;

namespace AdSet.Lead.Domain.Entities;

public sealed class VehicleOption : IEntity
{
    public Guid Id { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public DateTime UpdatedOn { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public ICollection<Vehicle> Vehicles { get; private set; } = new List<Vehicle>();

    private VehicleOption()
    {
    }

    public VehicleOption(string name)
    {
        Validate(name);

        Id = Guid.NewGuid();
        Name = name;
        CreatedOn = DateTime.UtcNow;
        UpdatedOn = DateTime.UtcNow;
    }

    private static void Validate(string name)
    {
        StringValidator.Validate(name, "Option name", 1, 100);
    }

    public void UpdateName(string name)
    {
        Validate(name);
        Name = name;
        UpdatedOn = DateTime.UtcNow;
    }
}