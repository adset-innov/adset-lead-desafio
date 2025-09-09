namespace AdSet.Domain.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Plate { get; set; }
        public string Model { get; set; }
        public string Make { get; set; }
        public int Year { get; set; }
        public int? Km { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public virtual ICollection<VehicleOptional> VehicleOptionals { get; set; } = new List<VehicleOptional>();
        public virtual ICollection<VehicleImage> VehicleImages { get; set; } = new List<VehicleImage>();
        public virtual ICollection<VehiclePortalPackage> VechiclePortalPackages { get; set; } = new List<VehiclePortalPackage>();
    }
}
