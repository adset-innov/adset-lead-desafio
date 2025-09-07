using static System.HashCode;

namespace AdSet.Lead.Domain.VOs;

public sealed class VehicleOptions
{
    public Dictionary<string, bool> Options { get; } = new(StringComparer.OrdinalIgnoreCase);

    private VehicleOptions()
    {
    }

    public VehicleOptions(Dictionary<string, bool>? options = null)
    {
        Options = options ?? new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);
    }

    public void AddOrUpdate(string key, bool value)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException("Option key cannot be null or empty", nameof(key));

        Options[key] = value;
    }

    public bool HasOption(string key) =>
        Options.TryGetValue(key, out var value) && value;

    public VehicleOptions WithOption(string key, bool value)
    {
        var clone = new Dictionary<string, bool>(Options, StringComparer.OrdinalIgnoreCase)
        {
            [key] = value
        };
        return new VehicleOptions(clone);
    }

    private sealed class KeyValuePairComparer : IEqualityComparer<KeyValuePair<string, bool>>
    {
        public bool Equals(KeyValuePair<string, bool> x, KeyValuePair<string, bool> y) =>
            StringComparer.OrdinalIgnoreCase.Equals(x.Key, y.Key) && x.Value == y.Value;

        public int GetHashCode(KeyValuePair<string, bool> obj) =>
            Combine(StringComparer.OrdinalIgnoreCase.GetHashCode(obj.Key), obj.Value);
    }

    public override bool Equals(object? obj) =>
        obj is VehicleOptions other &&
        Options.OrderBy(k => k.Key).SequenceEqual(other.Options.OrderBy(k => k.Key), new KeyValuePairComparer());

    public override int GetHashCode() =>
        Combine(Options.Count, string.Join(';', Options.Select(x => $"{x.Key}:{x.Value}")));
}