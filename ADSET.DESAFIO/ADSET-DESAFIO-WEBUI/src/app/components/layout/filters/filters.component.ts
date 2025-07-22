import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';

export interface Filters {
  placa: string;
  marca: string;
  modelo: string;
  anoMin: number;
  anoMax: number;
  preco: number;
  fotos: boolean;
  opcionais: string;
  cor: string;
}

@Component({
  selector: 'app-filters',
  templateUrl: './filters.component.html',
  styleUrls: ['./filters.component.scss']
})
export class FiltersComponent implements OnInit {
  filters: Filters = {
    placa: '',
    marca: '',
    modelo: '',
    anoMin: 0,
    anoMax: 0,
    preco: 0,
    fotos: true,
    opcionais: '',
    cor: ''
  };

  filtersCollapsed = true;
  years: number[] = [];
  precoFaixas: string[] = [];
  cores: string[] = [];

  @Input() hideActions = false;

  @Output() search = new EventEmitter<Filters>();

  @Output() collapseToggle = new EventEmitter<boolean>();

  ngOnInit(): void {
    const currentYear = new Date().getFullYear() - 1;
    this.years = Array.from({ length: 25 }, (_, i) => currentYear - i);
    this.precoFaixas = ['<20k', '20k-50k', '50k-100k', '>100k'];
    this.cores = ['Branco', 'Preto', 'Prata', 'Vermelho', 'Azul'];
  }

  clearFilters(): void {
    this.filters = {
      placa: '',
      marca: '',
      modelo: '',
      anoMin: 0,
      anoMax: 0,
      preco: 0,
      fotos: true,
      opcionais: '',
      cor: ''
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