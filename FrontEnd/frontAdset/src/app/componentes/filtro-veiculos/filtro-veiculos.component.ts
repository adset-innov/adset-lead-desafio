import { Component, OnInit, ViewChildren, QueryList } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { VeiculoService } from '../../services/veiculo/veiculo-service.service';
import { SwalHandler } from 'src/app/utils/SwalHandler';
import { VeiculoFiltroDto } from 'src/app/interfaces/Veiculo/VeiculoFiltroDto';
import { AtualizarRelacaoVeiculoPacotePortalDto } from 'src/app/interfaces/Veiculo/AtualizarRelacaoVeiculoPacotePortalDto';
import { PortalDto } from 'src/app/interfaces/Portal/PortalDto';
import { EdicaoService } from 'src/app/services/Triggers/edicao.service';
import { PortalService } from 'src/app/services/portal/portal.service';
import { CardVeiculoComponent } from '../card-veiculo/card-veiculo.component';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-filtro-veiculos',
  templateUrl: './filtro-veiculos.component.html',
  styleUrls: ['./filtro-veiculos.component.css']
})
export class FiltroVeiculosComponent implements OnInit {

  private salvarSubscription!: Subscription;
  filtroForm!: FormGroup;
  constructor(
    private _veiculoService: VeiculoService,
    private _portalService: PortalService,
    private edicaoService: EdicaoService,
    private fb: FormBuilder) { }

  @ViewChildren('cardveiculo') cardVeiculos!: QueryList<CardVeiculoComponent>;


  paginacaoVeiculos: VeiculoFiltroDto = {
    itens: [],
    paginaAtual: 1,
    tamanhoPagina: 10,
    totalRegistros: 0,
    totalPaginas: 0
  };

  paginaAtual = 1;
  tamanhoPagina = 10;
  ordenacao: string = '';
  filtros: any = {};
  anos: number[] = [];
  faixasPreco: { label: string, valorMin: number }[] = [];
  cores: string[] = [];
  portais: PortalDto[] = [];


  ngOnInit(): void {
    this.gerarAnos();
    this.inicializarFormulario();
    this.gerarFaixasPreco();
    this.gerarCores();
    this.obterPortais();
    this.listarVeiculos();
    this.salvarSubscription = this.edicaoService.salvarTrigger$.subscribe(() => {
      this.processarSalvamentoEmMassa();
    });
  }

  ngOnDestroy(): void {
    this.salvarSubscription.unsubscribe();
  }

  private inicializarFormulario(): void {
    this.filtroForm = this.fb.group({
      placa: [''],
      marca: [''],
      modelo: [''],
      anoMin: [''],
      anoMax: [''],
      preco: [''],
      fotos: [''],
      opcional: [''],
      cor: ['']
    });
  }

  private processarSalvamentoEmMassa(): void {
    if (!this.cardVeiculos) {
      SwalHandler.showAtencao("Atenção", "Cadastre um veiculo primeiro");
      return;
    }

    const payloadFinal: AtualizarRelacaoVeiculoPacotePortalDto[] = this.cardVeiculos.toArray()
      .map(card => card.coletarDadosDosPortais())
      .reduce((acumulador, arrayAtual) => acumulador.concat(arrayAtual), []);
    this.editarVinculos(payloadFinal)
  }

  private gerarAnos(): void {
    const anoInicial = 2000;
    const anoFinal = 2024;
    this.anos = Array.from({ length: anoFinal - anoInicial + 1 }, (_, i) => anoInicial + i);
  }

  private gerarCores(): void {
    this._veiculoService.ObterCores().subscribe({
      next: (dados) => {
        this.cores = dados;
      },
      error: (err) => {
        SwalHandler.showFalha(
          'Erro',
          err?.message || 'Não foi possível carregar as cores'
        );
      }
    });
  }

  private editarVinculos(dto: AtualizarRelacaoVeiculoPacotePortalDto[]) {
    this._veiculoService.atualizarVinculos(dto).subscribe({
      next: () => {
        SwalHandler.showSucesso('Sucesso', 'Vínculos editados com sucesso!');
        this.listarVeiculos();
      },
      error: () => {
        SwalHandler.showFalha('Falha', 'Ocorreu um erro ao processar os vínculos.');
      }
    });
  }

  private obterPortais(): void {
    this._portalService.obterPortais().subscribe({
      next: (dados) => {
        this.portais = dados;
      },
      error: (err) => {
        SwalHandler.showFalha(
          'Erro',
          err?.message || 'Não foi possível carregar os portais'
        );
      }
    });
  }

  private gerarFaixasPreco(): void {
    this.faixasPreco = [
      { label: '10 mil a 50 mil', valorMin: 10000 },
      { label: '50 mil a 90 mil', valorMin: 50000 },
      { label: '+ 90 mil', valorMin: 90001 },

    ];
  }

  private obterPrecoMax(valorMin: string): string {
    switch (valorMin) {
      case "0":
      case "90001": return "0";
      case "10000": return "50000";
      case "50000": return "90000";
      default: return "0";
    }
  }

  public listarVeiculos(): void {
    this._veiculoService.listarVeiculos(this.paginaAtual, this.tamanhoPagina, this.filtros).subscribe({
      next: (dados) => {
        this.paginacaoVeiculos = dados;
      },
      error: (err) => {
        SwalHandler.showFalha(
          'Erro',
          err?.message || 'Não foi possível carregar os veiculos'
        );
      }
    });
  }

  public buscarClick(): void {
    const formValues = this.filtroForm.value;
    this.filtros = {
      placa: formValues.placa || null,
      marca: formValues.marca || null,
      modelo: formValues.modelo || null,
      anoMin: formValues.anoMin || null,
      anoMax: formValues.anoMax || null,
      precoMin: formValues.preco || null,
      precoMax: this.obterPrecoMax(formValues.preco),
      fotos: formValues.fotos || null,
      opcional: formValues.opcional || null,
      cor: formValues.cor || null,
    };

    Object.keys(this.filtros).forEach(
      (k) => (this.filtros[k] === null || this.filtros[k] === '' || this.filtros[k] === '0') && delete this.filtros[k]
    );

    this.paginaAtual = 1;
    this.listarVeiculos();
  }

  public ordenar(campo: string) {
    const asc = this.ordenacao === campo ? false : true;
    this.ordenacao = asc ? campo : campo + '_desc';
    this.paginacaoVeiculos.itens.sort((a, b) => {
      let valorA: any;
      let valorB: any;

      switch (campo) {
        case 'marcaModelo':
          valorA = a.marca + ' ' + a.modelo;
          valorB = b.marca + ' ' + b.modelo;
          break;
        case 'ano':
          valorA = a.ano;
          valorB = b.ano;
          break;
        case 'preco':
          valorA = a.preco;
          valorB = b.preco;
          break;
        case 'fotos':
          valorA = a.fotos && a.fotos.length > 0 ? 1 : 0;
          valorB = b.fotos && b.fotos.length > 0 ? 1 : 0;
          break;
        default:
          return 0;
      }
      if (valorA < valorB) return asc ? -1 : 1;
      if (valorA > valorB) return asc ? 1 : -1;
      return 0;
    });
  }

  public onPaginaMudou(pagina: number) {
    this.paginaAtual = pagina;
    this.listarVeiculos();
  }

  public onTamanhoPaginaChange(event: Event): void {
    const selectElement = event.target as HTMLSelectElement;
    this.tamanhoPagina = Number(selectElement.value);
    this.paginaAtual = 1;
    this.listarVeiculos();
  }


public limparFiltros(): void {
  Object.keys(this.filtroForm.controls).forEach(key => {
    this.filtroForm.controls[key].setValue('');
  });
  this.filtros = {};
  this.listarVeiculos();
}




}











