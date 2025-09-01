using Adset.Lead.Domain.Abstractions;

namespace Adset.Lead.Domain.Automobiles;

public sealed class PortalPackage : Entity
{
    public PortalPackage(
        Guid id,
        Guid automobileId,
        Portal portal,
        Package package) 
        : base(id)
    {
        Portal = portal;
        Package = package;
        AutomobileId = automobileId;
    }

    private PortalPackage()
    {
    }
    
    public Guid AutomobileId { get; private set; }
    public Portal Portal { get; private set; }
    public Package Package { get; private set; }
}