using System.Text.RegularExpressions;
using AdSet.Lead.Core.Exceptions;
using AdSet.Lead.Core.Validators;

namespace AdSet.Lead.Domain.VOs;

public sealed partial class Color
{
    public string Value { get; }

    private Color()
    {
    }

    public Color(string color)
    {
        Value = Validate(color);
    }

    private static string Validate(string color)
    {
        StringValidator.Validate(color, "Color", 1, 50);

        if (!ColorRegex().IsMatch(color))
            throw new DomainValidationException("Color format is invalid.");

        return color;
    }


    [GeneratedRegex("^[a-zA-ZÀ-ÿ ]+$")]
    private static partial Regex ColorRegex();

    private bool Equals(Color other) => Value == other.Value;

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((Color)obj);
    }

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value;
}