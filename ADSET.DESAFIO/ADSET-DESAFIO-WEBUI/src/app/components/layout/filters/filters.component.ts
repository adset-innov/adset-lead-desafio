import { Component, OnInit, Output, EventEmitter } from '@angular/core';

export interface Filters {
  placa:     string;
  marca:     string;
  modelo:    string;
  anoMin:    string;
  anoMax:    string;
  preco:     string;
  fotos:     string;
  opcionais: string;
  cor:       string;
}

@Component({
  selector: 'app-filters',
  templateUrl: './filters.component.html',
  styleUrls: ['./filters.component.scss']
})
export class FiltersComponent implements OnInit {
  filters: Filters = {
    placa:     '',
    marca:     '',
    modelo:    '',
    anoMin:    '',
    anoMax:    '',
    preco:     '',
    fotos:     '',
    opcionais: '',
    cor:       ''
  };

  filtersCollapsed = false;
  years:       number[] = [];
  precoFaixas: string[] = [];
  cores:       string[] = [];

  @Output() search = new EventEmitter<Filters>();

  @Output() collapseToggle = new EventEmitter<boolean>();

  ngOnInit(): void {
    const currentYear = new Date().getFullYear() - 1;
    this.years       = Array.from({ length: 25 }, (_, i) => currentYear - i);
    this.precoFaixas = ['<20k', '20k-50k', '50k-100k', '>100k'];
    this.cores       = ['Branco', 'Preto', 'Prata', 'Vermelho', 'Azul'];
  }

  clearFilters(): void {
    this.filters = {
      placa:     '',
      marca:     '',
      modelo:    '',
      anoMin:    '',
      anoMax:    '',
      preco:     '',
      fotos:     '',
      opcionais: '',
      cor:       ''
    };
  }

  onSearch(): void {
    this.search.emit(this.filters);
  }

  toggleFilters(): void {
    this.filtersCollapsed = !this.filtersCollapsed;
    this.collapseToggle.emit(this.filtersCollapsed);
  }
}