namespace AdSet.Domain.Entities
{
    public class Optional
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<VehicleOptional> VehicleOptionals { get; set; } = new List<VehicleOptional>();
    }
}
