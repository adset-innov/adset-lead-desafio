namespace AdSet.Application.ViewModels
{
    public class UpdateVehiclePortalPackagesViewModel
    {
        public int VehicleId { get; set; }
        public List<PortalPackageSelectionViewModel> PortalSelections { get; set; } = new List<PortalPackageSelectionViewModel>();
    }
}
