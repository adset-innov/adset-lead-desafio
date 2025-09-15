export interface PortalPackageSelection {
  portalName: string;
  packageName: string;
  selected: boolean;
}

export interface UpdateVehiclePortalPackages {
  vehicleId: number;
  portalSelections: PortalPackageSelection[];
}
