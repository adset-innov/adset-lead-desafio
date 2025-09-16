import { Component, EventEmitter, Input, Output } from '@angular/core';

export type SortDirection = 'asc' | 'desc' | null;

@Component({
  selector: 'app-vehicles-filters-bar',
  templateUrl: './vehicles-filters-bar.component.html',
  styleUrls: ['./vehicles-filters-bar.component.scss'],
})
export class VehiclesFiltersBarComponent {
  @Input() pageSize: number = 10;
  @Output() pageSizeChange = new EventEmitter<number>();

  @Output() sortChange = new EventEmitter<{
    field: string;
    direction: SortDirection;
  }>();

  activeSort: { field: string; direction: SortDirection } = {
    field: '',
    direction: null,
  };

  onPageSizeChange(value: number) {
    this.pageSize = value;
    this.pageSizeChange.emit(this.pageSize);
  }

  toggleSort(field: string, direction: 'asc' | 'desc') {
    if (
      this.activeSort.field === field &&
      this.activeSort.direction === direction
    ) {
      this.activeSort = { field: '', direction: null };
    } else {
      this.activeSort = { field, direction };
    }
    this.sortChange.emit(this.activeSort);
  }

  isActive(field: string, direction: 'asc' | 'desc'): boolean {
    return (
      this.activeSort.field === field && this.activeSort.direction === direction
    );
  }
}
