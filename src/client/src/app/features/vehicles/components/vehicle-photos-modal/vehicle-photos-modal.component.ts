import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-vehicle-photos-modal',
  templateUrl: './vehicle-photos-modal.component.html',
  styleUrls: ['./vehicle-photos-modal.component.scss'],
})
export class VehiclePhotosModalComponent {
  currentIndex = 0;

  constructor(
    private dialogRef: MatDialogRef<VehiclePhotosModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { photos: { url: string }[] },
  ) {}

  get currentPhoto(): string {
    return this.data.photos[this.currentIndex]?.url ?? '';
  }

  next() {
    if (this.currentIndex < this.data.photos.length - 1) {
      this.currentIndex++;
    }
  }

  prev() {
    if (this.currentIndex > 0) {
      this.currentIndex--;
    }
  }

  close() {
    this.dialogRef.close();
  }
}
