import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-vehicles-filters-bar',
  templateUrl: './vehicles-filters-bar.component.html',
  styleUrls: ['./vehicles-filters-bar.component.scss'],
})
export class VehiclesFiltersBarComponent {
  @Input() pageSize: number = 10;
  @Output() pageSizeChange = new EventEmitter<number>();

  onPageSizeChange(value: number) {
    this.pageSize = value;
    this.pageSizeChange.emit(this.pageSize);
  }
}
