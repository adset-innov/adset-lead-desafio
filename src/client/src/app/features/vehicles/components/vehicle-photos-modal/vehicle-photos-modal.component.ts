import { Component, EventEmitter, Inject, Output } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Photo } from '../../../../core/models/photo';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-vehicle-photos-modal',
  templateUrl: './vehicle-photos-modal.component.html',
  styleUrls: ['./vehicle-photos-modal.component.scss'],
})
export class VehiclePhotosModalComponent {
  @Output() photoAdded = new EventEmitter<File>();
  @Output() photoRemoved = new EventEmitter<string>();

  currentIndex = 0;

  constructor(
    private dialogRef: MatDialogRef<VehiclePhotosModalComponent>,
    @Inject(MAT_DIALOG_DATA)
    public data: { vehicleId: string; photos: Photo[] },
    private sanitizer: DomSanitizer,
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

  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      const file = input.files[0];
      this.photoAdded.emit(file);

      const preview = URL.createObjectURL(file);
      const safePreview: SafeUrl =
        this.sanitizer.bypassSecurityTrustUrl(preview);

      this.data.photos.push({
        id: 'temp-' + Date.now(),
        url: safePreview as string,
        createdOn: new Date(),
        updatedOn: new Date(),
      });

      this.currentIndex = this.data.photos.length - 1;
    }
  }

  removeCurrentPhoto() {
    const photo = this.data.photos[this.currentIndex];
    if (!photo) return;

    this.photoRemoved.emit(photo.id);
    this.data.photos.splice(this.currentIndex, 1);

    if (this.currentIndex > 0) {
      this.currentIndex--;
    }
  }
}
