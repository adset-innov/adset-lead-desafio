import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CarroService, CarroResponse } from '../../services/carro.service';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-carro-edicao',
  standalone: true,
  templateUrl: './carro-edicao.component.html',
  styleUrls: ['./carro-edicao.component.css'],
  imports: [CommonModule, ReactiveFormsModule, HttpClientModule]
})
export class CarroEdicaoComponent implements OnInit {
  carroForm: FormGroup;
  carroId!: number;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder,
    private carroService: CarroService
  ) {
    this.carroForm = this.fb.group({
      id: [''],
      marca: [''],
      modelo: [''],
      ano: [''],
      placa: [''],
      quilometragem: [''],
      cor: [''],
      preco: [''],
      listaOpcionais: [''],
      // portalPacotes: [''],
      // fotos: [''],
    });
  }

  ngOnInit(): void {
    this.carroId = +this.route.snapshot.paramMap.get('id')!;
    this.carroService.getCarroPorId(this.carroId).subscribe({
      next: (carro: CarroResponse) => {
        this.carroForm.patchValue(carro);
      },
      error: err => {
        console.error('Erro ao carregar carro:', err);
      }
    });
  }

  salvar() {
    if (this.carroForm.invalid) return;

    this.carroService.atualizarCarro(this.carroId, this.carroForm.value).subscribe({
      next: () => {
        alert('Carro atualizado com sucesso!');
        this.router.navigate(['/carros']);
      },
      error: err => {
        console.error('Erro ao atualizar carro:', err);
        alert('Erro ao atualizar carro.');
      }
    });
  }
}
