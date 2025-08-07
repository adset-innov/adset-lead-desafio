import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CarroResponse, CarroService } from './services/carro.service';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  imports: [ReactiveFormsModule, CommonModule, HttpClientModule],
})
export class AppComponent {
  filtroForm: FormGroup;
  totalPaginas: number = 0;
  pagina: number = 1;
  tamanhoPagina: number = 10
  carros: CarroResponse[] = [];

  constructor(private fb: FormBuilder, private carroService: CarroService) {
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
      pageSize: [10]
    });
  }

    buscarCarros() {
    const filtro = this.filtroForm.value;

    this.carroService.getCarros(filtro).subscribe({
      next: (response: any) => {

        console.log(response)

        this.carros = response.content;
        this.totalPaginas = response.totalPages;
        this.pagina = response.page;
        this.tamanhoPagina = response.pageSize

        console.log(this.carros)
        console.log(this.totalPaginas)
        console.log(this.pagina)
        console.log(this.tamanhoPagina)
      },
      error: err => {
        console.error('Erro ao buscar carros:', err);
      }
    });
  }
}
