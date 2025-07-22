import { Component } from '@angular/core';
import { Car } from './models/car.model';
import { CarService } from './services/car.service';
import { Filters } from './components/layout/filters/filters.component';
import { CarFilterDto } from './dto/car-filter.dto';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})


export class AppComponent {
  title = 'ADSET-DESAFIO-WEBUI';
  currentSortBy = '';
  currentSortDir: 'asc' | 'desc' = 'asc';
  currentPageSize = 10;
  currentPage = 1;
  totalPagesFromApi = 1;

  cars: Car[] = [];
  private filters: Filters | null = null;

  constructor(private carService: CarService) { }

  ngOnInit(): void {
    this.loadCars();
  }

  handleExportExcel() { }
  handleExportCsv() { }
  handleRegister() { }
  handleSave() { }
  onSortByChange(sort: string) {
    this.currentSortBy = sort;
    this.loadCars();
  }

  onSortDirChange(dir: 'asc' | 'desc') {
    this.currentSortDir = dir;
    this.loadCars();
  }

  onPageSizeChange(size: number) {
    this.currentPageSize = size;
    this.currentPage = 1;
    this.loadCars();
  }

  onSearch(filters: Filters) {
    this.filters = filters;
    this.currentPage = 1;
    this.loadCars();
  }

  onPageChange(page: number) {
    this.currentPage = page;
    this.loadCars();
  }

  onDelete(id: number) {
    if (!confirm('Confirma a exclusão do veículo?')) {
      return;
    }
    this.carService.delete(id).subscribe(() => {
      this.cars = this.cars.filter(c => c.id !== id);
      this.loadCars();
    });
  }

  private loadCars() {
    const dto: CarFilterDto = {
      plate: this.filters?.placa,
      brand: this.filters?.marca,
      model: this.filters?.modelo,
      yearMin: this.filters?.anoMin ? +this.filters.anoMin : undefined,
      yearMax: this.filters?.anoMax ? +this.filters.anoMax : undefined,
      priceMin: this.filters?.preco,
      priceMax: this.filters?.preco,
      hasPhotos: this.filters?.fotos,
      optionals: this.filters?.opcionais,
      color: this.filters?.cor,
      pageNumber: this.currentPage,
      sortBy: this.currentSortBy,
      sortDir: this.currentSortDir
    };

    this.carService.list(dto, this.currentPage, this.currentPageSize).subscribe(result => {
      this.cars = result.items;
      this.totalPagesFromApi = result.totalPages;
    });
  }
}
