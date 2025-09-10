namespace AdSet.Domain.Entities
{
    public class VehicleImage
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int Order { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public virtual Vehicle Vehicle { get; set; }
    }
}

