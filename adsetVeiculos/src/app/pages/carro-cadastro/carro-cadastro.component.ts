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
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatRadioModule } from '@angular/material/radio';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { NgxMaskDirective } from 'ngx-mask';

@Component({
  selector: 'app-carro-cadastro',
  standalone: true,
  templateUrl: './carro-cadastro.component.html',
  styleUrls: ['./carro-cadastro.component.css'],
  imports: [
    ReactiveFormsModule,
    CommonModule,
    RouterModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatRadioModule,
    MatIconModule,
    MatCardModule,
    NgxMaskDirective,
  ],
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
      pacotes: this.fb.array([
        this.fb.group({
          tipoPortal: [1],
          tipoPacote: [null, Validators.required],
        }),
        this.fb.group({
          tipoPortal: [2],
          tipoPacote: [null, Validators.required],
        }),
      ]),
    });
  }

  get fotos() {
    return this.cadastroForm.get('fotos') as FormArray<FormGroup>;
  }

  get pacote(): FormArray {
    return this.cadastroForm.get('pacotes') as FormArray;
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

      if (carro.quilometragem == '') carro.quilometragem = 0;
      if (carro.placa) {
        carro.placa = carro.placa.replace(/-/g, '');
      }

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

  toUppercase(event: Event) {
    const input = event.target as HTMLInputElement;
    const value = input.value.toUpperCase();
    this.cadastroForm.get('placa')?.setValue(value, { emitEvent: false });
  }
}
