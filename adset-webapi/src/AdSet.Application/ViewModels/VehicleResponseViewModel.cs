namespace AdSet.Application.ViewModels
{
    public class VehicleResponseViewModel
    {
        public int Id { get; set; }
        public string Plate { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Make { get; set; } = string.Empty;
        public int Year { get; set; }
        public int? Km { get; set; }
        public string Color { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public List<string> ImageUrls { get; set; } = new List<string>();
        public List<string> OptionalNames { get; set; } = new List<string>();

        public List<PortalPackageSelectionViewModel> PortalPackages { get; set; } = new List<PortalPackageSelectionViewModel>();
    }
}
