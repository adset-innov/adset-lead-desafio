namespace AdSet.Application.ViewModels
{
    public class CreateUpdateVehicleViewModel
    {
        public int Id { get; set; }
        public string Plate { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Make { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Color { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int? Km { get; set; }
        public IFormFileCollection? Images { get; set; }
        public List<int> Optionals { get; set; } = new List<int>();
    }
}
