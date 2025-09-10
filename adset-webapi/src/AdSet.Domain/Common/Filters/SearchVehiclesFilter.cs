namespace AdSet.Domain.Common.Filters
{
    [ExcludeFromCodeCoverage]
    public record SearchVehiclesFilter
    {
        public string Plate { get; set; }
        public string Model { get; set; }
        public string Make { get; set; }
        public int? YearMin { get; set; }
        public int? YearMax { get; set; }
        public int? Km { get; set; }
        public List<string> Colors { get; set; } = new List<string>();
        public string Price { get; set; }
        public bool? Images { get; set; }
        public List<string>? Optionais { get; set; } = new List<string>();
    }
}
