namespace AdSet.Domain.Entities
{
    public class VehicleOptional
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int OptionalId { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public virtual Optional Optional { get; set; }
    }
}
