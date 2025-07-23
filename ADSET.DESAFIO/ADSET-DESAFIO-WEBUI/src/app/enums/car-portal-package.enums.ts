export enum Portal {
  iCarros = 'iCarros',
  WebMotors = 'WebMotors'
}

export enum PackageType {
  Bronze = 'Bronze',
  Diamante = 'Diamante',
  Platinum = 'Platinum',
  Basico = 'Básico'
}

export const PackageTypeApiMap: Record<PackageType, string> = {
  [PackageType.Bronze]: 'Bronze',
  [PackageType.Diamante]: 'Diamond',
  [PackageType.Platinum]: 'Platinum',
  [PackageType.Basico]: 'Basic'
};