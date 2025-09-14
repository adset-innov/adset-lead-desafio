import { Portal } from '../../enums/portal';
import { Package } from '../../enums/package';

export interface AddOrUpdateVehiclePortalPackageRequest {
  vehicleId?: string;
  portal: Portal;
  package: Package;
}
