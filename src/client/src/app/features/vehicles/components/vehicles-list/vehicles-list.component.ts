import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Vehicle } from '../../../../core/models/vehicle';

@Component({
  selector: 'app-vehicles-list',
  templateUrl: './vehicles-list.component.html',
  styleUrls: ['./vehicles-list.component.scss'],
})
export class VehiclesListComponent {
  @Input() vehicles: Vehicle[] = [];
  @Output() edit = new EventEmitter<string>();
  @Output() delete = new EventEmitter<string>();
  @Output() viewPhotos = new EventEmitter<Vehicle>();
  @Output() viewOptions = new EventEmitter<Vehicle>();

  onEdit(id: string) {
    this.edit.emit(id);
  }

  onDelete(id: string) {
    this.delete.emit(id);
  }

  onViewPhotos(vehicle: Vehicle) {
    this.viewPhotos.emit(vehicle);
  }

  onViewOptions(vehicle: Vehicle) {
    this.viewOptions.emit(vehicle);
  }
}
