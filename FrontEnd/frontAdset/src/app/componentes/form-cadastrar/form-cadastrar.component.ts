import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { OpcionalDto } from '../../interfaces/Opcional/OpcionalDto';
import { VeiculoCadastrarDto } from '../../interfaces/Veiculo/VeiculoCadastrarDto';
import { VeiculoService } from '../../services/veiculo/veiculo-service.service';
import { SwalHandler } from '../../utils/SwalHandler';
import { Router } from '@angular/router';

@Component({
  selector: 'app-form-cadastrar',
  templateUrl: './form-cadastrar.component.html',
  styleUrls: ['./form-cadastrar.component.css']
})
export class FormCadastrarComponent implements OnInit {

  veiculoForm!: FormGroup;
  opcionaisDisponiveis: OpcionalDto[] = [];
  opcionaisSelecionados: OpcionalDto[] = [];
  selectedFiles: File[] = [];

  constructor(
    private _service: VeiculoService,
    private router: Router,
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
    this.veiculoForm = this.fb.group({
      marca: ['', Validators.required],
      modelo: ['', Validators.required],
      ano: ['', [Validators.required, Validators.min(2000), Validators.max(2024)]],
      placa: ['', [
        Validators.required,
        Validators.pattern(/^[A-Z]{3}-?[0-9]{4}$|^[A-Z]{3}[0-9][A-Z][0-9]{2}$/)
      ]],
      km: [''],
      cor: ['', Validators.required],
      preco: ['', Validators.required]
    });

    this.listarOpcionais();
  }

  listarOpcionais(): void {
    this._service.ListarOpcionais().subscribe({
      next: (dados) => this.opcionaisDisponiveis = dados,
      error: () => SwalHandler.showFalha('Erro', 'Falha ao carregar opcionais.')
    });
  }

  adicionarOpcional(event: Event): void {
    const select = event.target as HTMLSelectElement;
    if (!select.value) return;

    const idSelecionado = Number(select.value);
    const opcional = this.opcionaisDisponiveis.find(o => o.id === idSelecionado);

    if (opcional && !this.opcionaisSelecionados.some(o => o.id === opcional.id)) {
      this.opcionaisSelecionados.push(opcional);
    }

    select.value = '';
  }


  removerOpcional(opcionalParaRemover: OpcionalDto): void {
    this.opcionaisSelecionados = this.opcionaisSelecionados.filter(o => o.id !== opcionalParaRemover.id);
  }


  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;

    if (!input.files) return;

    const files = Array.from(input.files);

    if (files.length > 15) {
      SwalHandler.showAtencao("Atenção", "Limite de 15 imagens para upload");
      input.value = '';
      return;
    }

    this.selectedFiles = files;
  }

  onSubmit(): void {

    if (this.veiculoForm.invalid) {
      this.veiculoForm.markAllAsTouched();
      return;
    }
    const opcionaisIds = this.opcionaisSelecionados.map(o => o.id.toString());

    const veiculoDto: VeiculoCadastrarDto = {
      marca: this.veiculoForm.value.marca,
      modelo: this.veiculoForm.value.modelo,
      ano: Number(this.veiculoForm.value.ano),
      placa: this.veiculoForm.value.placa,
      cor: this.veiculoForm.value.cor,
      preco: Number(this.veiculoForm.value.preco),
      km: this.veiculoForm.value.km ? Number(this.veiculoForm.value.km) : undefined,
      opcionais: opcionaisIds,
      fotos: this.selectedFiles,

    };

    this.cadastrarVeiculo(veiculoDto);
  }

  cadastrarVeiculo(veiculoDto: VeiculoCadastrarDto): void {
    this._service.CadastrarVeiculo(veiculoDto).subscribe({
      next: () => SwalHandler.showSucessoRedirecionamento(this.router, 'Sucesso', 'Veículo cadastrado com sucesso!', ''),
      error: (e) => {

        const titulo = e.status == 409
          ? (e.error.titulo || 'Falha ao cadastrar veículo')
          : 'Falha ao processar a requisição';

        const detalhes = e.status == 409
          ? (e.error.detalhes || 'Ocorreu um erro inesperado.')
          : 'Tente novamente mais tarde.';
        SwalHandler.showFalha(titulo, detalhes);
      }
    });
  }
}
