export interface PortalPackageSelection {
  portal: string;
  packageName: string;
  selected: boolean;
}

export interface UpdateVehiclePortalPackages {
  vehicleId: number;
  portalSelections: PortalPackageSelection[];
}
