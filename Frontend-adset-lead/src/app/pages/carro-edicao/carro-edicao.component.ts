import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {
  FormArray,
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CarroService, CarroResponse } from '../../services/carro.service';
import { RouterModule } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatRadioModule } from '@angular/material/radio';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { NgxMaskDirective } from 'ngx-mask';

@Component({
  selector: 'app-carro-edicao',
  standalone: true,
  templateUrl: './carro-edicao.component.html',
  styleUrls: ['./carro-edicao.component.css'],
  imports: [
    CommonModule,
    ReactiveFormsModule,
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
      pacoteIcarros: [null],
      pacoteWebmotors: [null],
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
          pacoteIcarros: this.getPacoteByPortal(carro.portalPacotes, 1),
          pacoteWebmotors: this.getPacoteByPortal(carro.portalPacotes, 2),
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

    const formValue = this.carroForm.value;

    const payload = {
      id: formValue.id,
      marca: formValue.marca,
      modelo: formValue.modelo,
      ano: formValue.ano,
      placa: formValue.placa,
      quilometragem: formValue.quilometragem,
      cor: formValue.cor,
      preco: formValue.preco,
      listaOpcionais: formValue.listaOpcionais,
      fotos: formValue.fotos,
      portalPacotes: [
        { portal: 1, pacote: formValue.pacoteIcarros },
        { portal: 2, pacote: formValue.pacoteWebmotors },
      ],
    };

    if (payload.quilometragem == null) payload.quilometragem = 0;
    if (payload.placa) {
      payload.placa = payload.placa.replace(/-/g, '');
    }

    this.carroService.atualizarCarro(this.carroId, payload).subscribe({
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

  getPacoteByPortal(pacotes: any[], portal: number) {
    const p = pacotes.find((x) => x.portal === portal);
    return p ? p.pacote : null;
  }

  toUppercase(event: Event) {
    const input = event.target as HTMLInputElement;
    const value = input.value.toUpperCase();
    this.carroForm.get('placa')?.setValue(value, { emitEvent: false });
  }
}
