import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CarroService } from '../../services/carro.service';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-carro-cadastro',
  standalone: true,
  templateUrl: './carro-cadastro.component.html',
  styleUrls: ['./carro-cadastro.component.css'],
  imports: [ReactiveFormsModule, CommonModule, RouterModule]
})
export class CarroCadastroComponent {
  cadastroForm: FormGroup;
  sucesso: boolean = false;
  erro: string = '';

  constructor(private fb: FormBuilder, private carroService: CarroService) {
    this.cadastroForm = this.fb.group({
      marca: ['', Validators.required],
      modelo: ['', Validators.required],
      ano: ['', [Validators.required, Validators.min(1900), Validators.max(new Date().getFullYear())]],
      placa: ['', Validators.required],
      quilometragem: [''],
      cor: ['', Validators.required],
      preco: ['', Validators.required],
      listaOpcionais: [''],
      //hasPhotos: [false]
    });
  }

  onSubmit() {
    if (this.cadastroForm.invalid) {
      this.cadastroForm.markAllAsTouched();
      return;
    }

    const novoCarro = this.cadastroForm.value;
    
    this.carroService.cadastrarCarro(novoCarro).subscribe({
      next: () => {
        this.sucesso = true;
        this.erro = '';
        this.cadastroForm.reset();
      },
      error: err => {
        this.erro = 'Erro ao cadastrar carro. Tente novamente.';
        this.sucesso = false;
        console.error(err);
      }
    });
  }
}
