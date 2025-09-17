import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-vehicles-stats-bar',
  templateUrl: './vehicles-stats-bar.component.html',
  styleUrls: ['./vehicles-stats-bar.component.scss'],
})
export class VehiclesStatsBarComponent implements OnInit {
  @Input() totalCount = 0;
  @Input() withPhotosCount = 0;
  @Input() withoutPhotosCount = 0;

  @Output() createVehicle = new EventEmitter<void>();
  @Output() save = new EventEmitter<void>();

  constructor() {}

  ngOnInit(): void {}

  onCreateClick(): void {
    this.createVehicle.emit();
  }

  onSaveClick(): void {
    this.save.emit();
  }
}
