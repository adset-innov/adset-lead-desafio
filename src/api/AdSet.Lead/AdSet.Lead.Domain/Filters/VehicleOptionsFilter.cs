namespace AdSet.Lead.Domain.Filters;

public sealed class VehicleOptionsFilter
{
    private readonly Dictionary<string, bool> _options = new(StringComparer.OrdinalIgnoreCase);

    public IReadOnlyDictionary<string, bool> Options => _options;

    public VehicleOptionsFilter()
    {
    }

    public VehicleOptionsFilter(Dictionary<string, bool>? options)
    {
        if (options is not null)
        {
            foreach (var kv in options)
                _options[kv.Key] = kv.Value;
        }
    }

    public void AddOrUpdate(string key, bool value)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException("Option key cannot be null or empty", nameof(key));

        _options[key] = value;
    }

    public bool? Get(string key)
    {
        return _options.TryGetValue(key, out var value) ? value : null;
    }
}