namespace AdSet.Domain.Entities
{
    public class Portal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<VehiclePortalPackage> VehiclePortalPackages { get; set; } = new List<VehiclePortalPackage>();
    }
}
