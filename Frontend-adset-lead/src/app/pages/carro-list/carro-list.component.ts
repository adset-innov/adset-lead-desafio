import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CarroResponse, CarroService } from '../../services/carro.service';
import { RouterModule } from '@angular/router';
import { Router } from '@angular/router';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule, MatTableDataSource } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatRadioModule } from '@angular/material/radio';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSortModule, MatSort } from '@angular/material/sort';
import { NgxMaskDirective } from 'ngx-mask';

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './carro-list.component.html',
  styleUrls: ['./carro-list.component.css'],
  imports: [
    ReactiveFormsModule,
    CommonModule,
    RouterModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatIconModule,
    MatTableModule,
    MatPaginatorModule,
    MatRadioModule,
    MatTooltipModule,
    MatDividerModule,
    MatFormFieldModule,
    MatSortModule,
    NgxMaskDirective,
  ],
})
export class CarrosComponent implements OnInit, AfterViewInit {
  displayedColumns = [
    'acoes',
    'foto',
    'marcaModelo',
    'placa',
    'ano',
    'cor',
    'preco',
    'opcionais',
    'quilometragem',
  ];
  filtroForm: FormGroup;
  anos: number[] = [];
  totalCarrosCadastrados: number = 0;
  totalCarrosFiltrados: number = 0;
  totalCarrosComFotos: number = 0;
  totalCarrosSemFotos: number = 0;
  cores: string[] = [];
  totalPaginas: number = 0;
  pagina: number = 0;
  tamanhoPagina: number = 10;
  pacotes = [
    { label: 'Nenhum', value: null },
    { label: 'Bronze', value: 1 },
    { label: 'Diamante', value: 2 },
    { label: 'Platinum', value: 3 },
    { label: 'Básico', value: 4 },
  ];
  carros: CarroResponse[] = [];
  dataSource = new MatTableDataSource<CarroResponse>([]);

  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private fb: FormBuilder,
    private carroService: CarroService,
    private router: Router
  ) {
    this.filtroForm = this.fb.group({
      placa: [''],
      marca: [''],
      modelo: [''],
      anoMin: [''],
      anoMax: [''],
      preco: [''],
      precoMin: [''],
      precoMax: [''],
      hasPhotos: [''],
      opcionais: [''],
      cor: [''],
      pacoteIcarros: [''],
      pacoteWebmotors: [''],
      page: [0],
      pageSize: [10],
    });
  }

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
    this.sort.sortChange.subscribe(() => {
      this.pagina = 0;
      this.buscarCarros();
    });
  }

  ngOnInit(): void {
    const anoAtual = 2024;
    for (let ano = 2000; ano <= anoAtual; ano++) {
      this.anos.push(ano);
    }
    this.buscarCarros();
  }

  buscarCarros() {
    this.filtroForm.patchValue({
      precoMin: '',
      precoMax: '',
    });

    const faixaPreco = this.filtroForm.get('preco')?.value;
    if (faixaPreco) {
      const [min, max] = faixaPreco.split('-');
      this.filtroForm.patchValue({
        precoMin: min,
        precoMax: max,
      });
    }

    this.filtroForm.patchValue({
      page: this.pagina + 1,
      pageSize: this.tamanhoPagina,
    });

    const filtro = this.filtroForm.value;

    if (filtro.placa) {
      filtro.placa = filtro.placa.replace(/-/g, '');
    }

    this.carroService.getCarros(filtro).subscribe({
      next: (response: any) => {
        this.carros = response.content;
        this.totalCarrosCadastrados = response.totalCarrosCadastrados;
        this.totalCarrosFiltrados = response.totalCarrosFiltrados;
        this.totalCarrosComFotos = response.totalCarrosComFotos;
        this.totalCarrosSemFotos = response.totalCarrosSemFotos;
        this.cores = response.cores;
        this.totalPaginas = response.totalPages;
        this.tamanhoPagina = response.pageSize;
        if (typeof response.page === 'number') {
          const lastIndex = Math.max(
            Math.ceil(this.carros.length / this.tamanhoPagina) - 1,
            0
          );
          this.pagina = Math.min(Math.max(response.page - 1, 0), lastIndex);
        }

        this.dataSource.data = this.carros;
        this.dataSource.sortingDataAccessor = (item, property) => {
          if (property === 'marcaModelo') {
            return (item.marca + ' ' + item.modelo).toLowerCase();
          }

          if (property === 'foto') {
            return item.fotos && item.fotos.length > 0 ? 1 : 0;
          }
          return (item as any)[property];
        };
      },
      error: (err) => {
        console.error('Erro ao buscar carros:', err);
      },
    });
  }

  deletarCarro(id: number) {
    if (!confirm('Tem certeza que deseja excluir este carro?')) return;

    this.carroService.deletarCarro(id).subscribe({
      next: () => {
        this.carros = this.carros.filter((c) => c.id !== id);
        this.buscarCarros();
      },
      error: (err) => {
        console.error('Erro ao deletar carro:', err);
        alert('Erro ao deletar carro.');
      },
    });
  }

  editarCarro(id: number) {
    this.router.navigate(['/carros/editar', id]);
  }

  onPageChange(event: PageEvent) {
    this.pagina = event.pageIndex;
    this.tamanhoPagina = event.pageSize;
    this.buscarCarros();
  }

  limparFiltros() {
    this.filtroForm.reset();
    this.buscarCarros();
  }

  formatarPlaca(placa: string): string {
    if (!placa) {
      return '';
    }
    return `${placa.substring(0, 3)}-${placa.substring(3, 7)}`;
  }

  toUppercase(event: Event) {
    const input = event.target as HTMLInputElement;
    const value = input.value.toUpperCase();
    this.filtroForm.get('placa')?.setValue(value, { emitEvent: false });
  }
}
