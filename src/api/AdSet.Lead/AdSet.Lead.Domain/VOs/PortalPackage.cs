using AdSet.Lead.Core.Exceptions;
using AdSet.Lead.Domain.Enums;

namespace AdSet.Lead.Domain.VOs;

public sealed class PortalPackage
{
    public Portal Portal { get; }
    public Package Package { get; }

    private PortalPackage()
    {
    }

    public PortalPackage(Portal portal, Package package)
    {
        Validate(portal, package);
        Portal = portal;
        Package = package;
    }

    private static void Validate(Portal portal, Package package)
    {
        if (!Enum.IsDefined(portal))
            throw new DomainValidationException("Invalid portal value.");

        if (!Enum.IsDefined(package))
            throw new DomainValidationException("Invalid package value.");
    }

    private bool Equals(PortalPackage other)
        => Portal == other.Portal
           && Package == other.Package;

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((PortalPackage)obj);
    }

    public override int GetHashCode() => HashCode.Combine(Portal, Package);

    public override string ToString() => $"{Portal} - {Package}";
}