import { Component, EventEmitter, Input, Output } from '@angular/core';
import { SearchVehiclesRequest } from '../../../../core/dtos/requests/search-vehicles-request';

type PriceRange = '' | '10-50' | '50-90' | '90+';

@Component({
  selector: 'app-vehicles-search-bar',
  templateUrl: './vehicles-search-bar.component.html',
  styleUrls: ['./vehicles-search-bar.component.scss'],
})
export class VehiclesSearchBarComponent {
  @Input() colors: string[] = [];
  @Output() search = new EventEmitter<SearchVehiclesRequest>();
  @Output() reset = new EventEmitter<void>();

  filters: SearchVehiclesRequest = {};
  readonly years = Array.from({ length: 2024 - 2000 + 1 }, (_, i) => 2000 + i);

  selectedPriceRange: PriceRange = '';
  collapsed = false;

  private readonly priceRanges: Record<
    Exclude<PriceRange, ''>,
    { min: number; max?: number }
  > = {
    '10-50': { min: 10000, max: 50000 },
    '50-90': { min: 50000, max: 90000 },
    '90+': { min: 90000 },
  };

  onPriceChange(value: PriceRange): void {
    this.selectedPriceRange = value;

    if (value && this.priceRanges[value]) {
      const { min, max } = this.priceRanges[value];
      this.filters.priceMin = min;
      this.filters.priceMax = max;
    } else {
      this.filters.priceMin = undefined;
      this.filters.priceMax = undefined;
    }
  }

  onSearch(): void {
    console.log('oi');
    this.search.emit(this.filters);
  }

  onReset(): void {
    this.filters = {};
    this.selectedPriceRange = '';
    this.reset.emit();
  }

  toggleCollapse(): void {
    this.collapsed = !this.collapsed;
  }
}
