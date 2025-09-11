namespace AdSet.Domain.Entities
{
    public class VehiclePortalPackage
    {
        public int VehicleId { get; set; }
        public int PortalId { get; set; }
        public int PackageId { get; set; }

        public virtual Vehicle Vehicle { get; set; }
        public virtual Portal Portal { get; set; }
        public virtual Package Package { get; set; }
    }
}
