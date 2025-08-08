import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
  FormArray,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CarroService } from '../../services/carro.service';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-carro-cadastro',
  standalone: true,
  templateUrl: './carro-cadastro.component.html',
  styleUrls: ['./carro-cadastro.component.css'],
  imports: [ReactiveFormsModule, CommonModule, RouterModule],
})
export class CarroCadastroComponent {
  cadastroForm: FormGroup;
  sucesso: boolean = false;
  erro: string = '';

  constructor(private fb: FormBuilder, private carroService: CarroService) {
    this.cadastroForm = this.fb.group({
      marca: ['', Validators.required],
      modelo: ['', Validators.required],
      ano: [
        '',
        [
          Validators.required,
          Validators.min(1900),
          Validators.max(new Date().getFullYear()),
        ],
      ],
      placa: ['', Validators.required],
      quilometragem: [''],
      cor: ['', Validators.required],
      preco: ['', Validators.required],
      listaOpcionais: [''],
      fotos: this.fb.array<FormGroup>([]),
    });
  }

  get fotos() {
    return this.cadastroForm.get('fotos') as FormArray<FormGroup>;
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

  onSubmit(): void {
    if (this.cadastroForm.valid) {
      const carro = this.cadastroForm.value;

      this.carroService.cadastrarCarro(carro).subscribe({
        next: () => {
          this.sucesso = true;
          this.erro = '';
          this.cadastroForm.reset();
          this.fotos.clear();
          this.adicionarFoto();
        },
        error: (err) => {
          this.erro = 'Erro ao cadastrar carro.';
          this.sucesso = false;
          console.error(err);
        },
      });
    } else {
      this.cadastroForm.markAllAsTouched();
    }
  }
}
