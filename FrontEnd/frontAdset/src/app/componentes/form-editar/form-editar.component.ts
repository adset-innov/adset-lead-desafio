import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { OpcionalVeiculoDto } from '../../interfaces/Veiculo/OpcionalVeiculoDto';
import { VeiculoAtualizarDto } from '../../interfaces/Veiculo/VeiculoAtualizarDto';
import { VeiculoDto } from '../../interfaces/Veiculo/VeiculoDto';
import { VeiculoService } from '../../services/veiculo/veiculo-service.service';
import { SwalHandler } from '../../utils/SwalHandler';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-form-editar',
  templateUrl: './form-editar.component.html',
  styleUrls: ['./form-editar.component.css']
})
export class FormEditarComponent implements OnInit {

  veiculoForm!: FormGroup;
  opcionaisDisponiveis: OpcionalVeiculoDto[] = [];
  opcionaisSelecionados: OpcionalVeiculoDto[] = [];


  constructor(
    private _service: VeiculoService,
    private router: Router,
    private fb: FormBuilder,
    private route: ActivatedRoute
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

    const id = Number(this.route.snapshot.paramMap.get('id'));
    if (id) {
      this.carregarVeiculo(id);
    }
  }

  listarOpcionais(): void {
    this._service.ListarOpcionais().subscribe({
      next: (dados) => this.opcionaisDisponiveis = dados,
      error: () => SwalHandler.showFalha('Erro', 'Falha ao carregar opcionais.')
    });
  }

  carregarVeiculo(id: number): void {
    this._service.ObterPorId(id).subscribe({
      next: (v: VeiculoDto) => {
        this.veiculoForm.patchValue({
          marca: v.marca,
          modelo: v.modelo,
          ano: v.ano,
          placa: v.placa,
          km: v.km,
          cor: v.cor,
          preco: v.preco
        });


        this.opcionaisSelecionados = [...(v.opcionais ?? [])];


      },
      error: () => SwalHandler.showFalha('Erro', 'Não foi possível carregar o veículo.')
    });
  }
  adicionarOpcional(event: Event): void {
    const select = event.target as HTMLSelectElement;
    const idSelecionado = Number(select.value);
    const opcional = this.opcionaisDisponiveis.find(o => o.id === idSelecionado);

    if (opcional && !this.opcionaisSelecionados.some(o => o.id === opcional.id)) {
      this.opcionaisSelecionados.push({ ...opcional });
    }

    select.value = '';
  }

  removerOpcional(opcionalParaRemover: OpcionalVeiculoDto): void {
    this.opcionaisSelecionados = this.opcionaisSelecionados.filter(o => o.id !== opcionalParaRemover.id);
  }


  onSubmit(): void {

    if (this.veiculoForm.invalid) {
      this.veiculoForm.markAllAsTouched();
      return;
    }
    const opcionaisIds = this.opcionaisSelecionados.map(o => o.id.toString());

    const veiculoDto: VeiculoAtualizarDto = {
      id: Number(this.route.snapshot.paramMap.get('id')),
      marca: this.veiculoForm.value.marca,
      modelo: this.veiculoForm.value.modelo,
      ano: Number(this.veiculoForm.value.ano),
      placa: this.veiculoForm.value.placa,
      cor: this.veiculoForm.value.cor,
      preco: Number(this.veiculoForm.value.preco),
      km: this.veiculoForm.value.km ? Number(this.veiculoForm.value.km) : undefined,
      opcionais: opcionaisIds

    };

    this.editarVeiculo(veiculoDto);
  }

  editarVeiculo(veiculoDto: VeiculoAtualizarDto): void {
    this._service.editarVeiculo(Number(veiculoDto.id), veiculoDto).subscribe({
      next: () =>
        SwalHandler.showSucessoRedirecionamento(
          this.router,
          'Sucesso',
          'Veículo editado com sucesso!',
          ''
        ),
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
