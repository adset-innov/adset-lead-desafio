import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CarroResponse, CarroService } from '../../services/carro.service';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './carro-list.component.html',
  styleUrls: ['./carro-list.component.css'],
  imports: [ReactiveFormsModule, CommonModule, HttpClientModule, RouterModule],
})
export class CarrosComponent implements OnInit {
  filtroForm: FormGroup;
  anos: number[] = [];
  totalCarros: number = 0;
  totalCarrosComFotos: number = 0;
  totalCarrosSemFotos: number = 0;
  cores: string[] = [];
  totalPaginas: number = 0;
  pagina: number = 1;
  tamanhoPagina: number = 10;
  pacotes = [
    { label: 'Nenhum', value: null },
    { label: 'Bronze', value: 1 },
    { label: 'Diamante', value: 2 },
    { label: 'Platinum', value: 3 },
    { label: 'Básico', value: 4 },
  ];
  carros: CarroResponse[] = [];

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
      page: [1],
      pageSize: [10],
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
    const [min, max] = faixaPreco.split('-');
    this.filtroForm.patchValue({
      precoMin: min,
      precoMax: max,
    });

    const filtro = this.filtroForm.value;

    this.carroService.getCarros(filtro).subscribe({
      next: (response: any) => {
        this.carros = response.content;
        this.totalCarros = response.totalCarros;
        this.totalCarrosComFotos = response.totalCarrosComFotos;
        this.totalCarrosSemFotos = response.totalCarrosSemFotos;
        this.cores = response.cores;
        this.totalPaginas = response.totalPages;
        this.pagina = response.page;
        this.tamanhoPagina = response.pageSize;
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
}
