import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { VehicleOption } from '../../../../core/models/vehicle-option';

@Component({
  selector: 'app-vehicle-options-modal',
  templateUrl: './vehicle-options-modal.component.html',
  styleUrls: ['./vehicle-options-modal.component.scss'],
})
export class VehicleOptionsModalComponent {
  constructor(
    private dialogRef: MatDialogRef<VehicleOptionsModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { options: VehicleOption[] },
  ) {}

  close() {
    this.dialogRef.close();
  }
}
