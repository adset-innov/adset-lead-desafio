using System.Text.RegularExpressions;
using AdSet.Lead.Core.Exceptions;
using AdSet.Lead.Core.Validators;

namespace AdSet.Lead.Domain.VOs;

public sealed partial class LicensePlate
{
    public string Value { get; }

    private LicensePlate()
    {
    }

    public LicensePlate(string licensePlate)
    {
        Value = Validate(licensePlate);
    }

    private static string Validate(string licensePlate)
    {
        StringValidator.Validate(licensePlate, "License Plate", 1, 10);

        if (!LicensePlateRegex().IsMatch(licensePlate))
            throw new DomainValidationException("License plate format is invalid.");

        return licensePlate.ToUpper();
    }


    [GeneratedRegex("^[A-Z]{3}-?[0-9][A-Z0-9][0-9]{2}$")]
    private static partial Regex LicensePlateRegex();

    private bool Equals(LicensePlate other) => Value == other.Value;

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((LicensePlate)obj);
    }

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value;
}