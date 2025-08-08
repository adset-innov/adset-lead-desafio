import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {
  FormArray,
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CarroService, CarroResponse } from '../../services/carro.service';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-carro-edicao',
  standalone: true,
  templateUrl: './carro-edicao.component.html',
  styleUrls: ['./carro-edicao.component.css'],
  imports: [CommonModule, ReactiveFormsModule, HttpClientModule, RouterModule],
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
      fotos: this.fb.array([]),
    });
  }

  get fotos() {
    return this.carroForm.get('fotos') as FormArray<FormGroup>;
  }

  createFotoFormGroup(url: string = ''): FormGroup {
    return this.fb.group({
      url: [url],
    });
  }

  adicionarFoto(): void {
    if (this.fotos.length < 15) {
      this.fotos.push(
        this.fb.group({
          url: [''],
        })
      );
    }
  }

  removerFoto(index: number): void {
    this.fotos.removeAt(index);
  }

  ngOnInit(): void {
    this.carroId = +this.route.snapshot.paramMap.get('id')!;
    this.carroService.getCarroPorId(this.carroId).subscribe({
      next: (carro: CarroResponse) => {
        this.carroForm.patchValue({
          id: carro.id,
          marca: carro.marca,
          modelo: carro.modelo,
          ano: carro.ano,
          placa: carro.placa,
          quilometragem: carro.quilometragem,
          cor: carro.cor,
          preco: carro.preco,
          listaOpcionais: carro.listaOpcionais,
        });

        this.fotos.clear();
        carro.fotos.forEach((foto) => {
          this.fotos.push(
            this.fb.group({
              id: [foto.id],
              url: [foto.url],
              carroId: [foto.carroId],
            })
          );
        });
      },
      error: (err) => {
        console.error('Erro ao carregar carro:', err);
      },
    });
  }

  salvar() {
    if (this.carroForm.invalid) return;

    this.carroService
      .atualizarCarro(this.carroId, this.carroForm.value)
      .subscribe({
        next: () => {
          alert('Carro atualizado com sucesso!');
          this.router.navigate(['/carros']);
        },
        error: (err) => {
          console.error('Erro ao atualizar carro:', err);
          alert('Erro ao atualizar carro.');
        },
      });
  }
}
