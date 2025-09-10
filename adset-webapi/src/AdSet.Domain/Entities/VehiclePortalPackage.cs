namespace AdSet.Domain.Entities
{
    public class VehiclePortalPackage
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public PortalName Portal { get; set; }
        public PackageType Package { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }

    public enum PortalName 
    { 
        iCarros = 0,
        WebMotors = 1 
    }

    public enum PackageType 
    { 
        Basico = 0, 
        Bronze = 1,
        Diamante = 2, 
        Platinum = 3 
    }
}
