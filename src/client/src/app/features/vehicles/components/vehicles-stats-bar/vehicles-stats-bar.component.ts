import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-vehicles-stats-bar',
  templateUrl: './vehicles-stats-bar.component.html',
  styleUrls: ['./vehicles-stats-bar.component.scss'],
})
export class VehiclesStatsBarComponent implements OnInit {
  @Input() totalCount = 0;
  @Input() withPhotosCount = 0;
  @Input() withoutPhotosCount = 0;

  constructor() {}

  ngOnInit(): void {}
}
