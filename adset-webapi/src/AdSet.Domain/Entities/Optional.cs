namespace AdSet.Domain.Entities
{
    public class Optional
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<VehicleOptional> VehicleOptionals { get; set; } = new List<VehicleOptional>();
    }
}
