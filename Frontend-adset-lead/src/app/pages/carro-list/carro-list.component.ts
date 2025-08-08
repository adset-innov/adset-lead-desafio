import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CarroResponse, CarroService } from '../../services/carro.service';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  //templateUrl: './app.component.html',
  templateUrl: './carro-list.component.html',
  styleUrls: ['./carro-list.component.css'],
  imports: [ReactiveFormsModule, CommonModule, HttpClientModule, RouterModule],
})
export class CarrosComponent {
  filtroForm: FormGroup;
  totalPaginas: number = 0;
  pagina: number = 1;
  tamanhoPagina: number = 10;
  carros: CarroResponse[] = [];

  constructor(private fb: FormBuilder,
    private carroService: CarroService,
    private router: Router) {
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
      page: [1],
      pageSize: [10],
    });
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
