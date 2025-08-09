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
  totalPaginas: number = 0;
  pagina: number = 1;
  tamanhoPagina: number = 10;
  pacotes = [
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
    this.buscarCarros();
  }

  buscarCarros() {
    const filtro = this.filtroForm.value;

    this.carroService.getCarros(filtro).subscribe({
      next: (response: any) => {
        this.carros = response.content;
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
