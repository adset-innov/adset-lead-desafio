import { Component, Input, OnInit, Output, EventEmitter, QueryList, ViewChildren, SimpleChanges } from '@angular/core';
import { VeiculoService } from '../../services/veiculo/veiculo-service.service';
import { VeiculoDto } from '../../interfaces/Veiculo/VeiculoDto';
import { OpcionalVeiculoDto } from '../../interfaces/Veiculo/OpcionalVeiculoDto';
import { Router } from '@angular/router';
import { SwalHandler } from '../../utils/SwalHandler';
import { PortalDto } from 'src/app/interfaces/Portal/PortalDto';
import { CardPortalComponent } from '../card-portal/card-portal.component';
import { environment } from 'src/environments/environment';
import { RemocaoService } from 'src/app/services/Triggers/remocao.service';

@Component({
  selector: 'app-card-veiculo',
  templateUrl: './card-veiculo.component.html',
  styleUrls: ['./card-veiculo.component.css']
})
export class CardVeiculoComponent implements OnInit {
  @Input() veiculo!: VeiculoDto;
  @Input() portais!: PortalDto[];
  @Output() veiculoDeletado = new EventEmitter<void>();
  @ViewChildren('cardportal') cardPortais!: QueryList<CardPortalComponent>;

  textoOpcionais!: string;
  labelFotos: string = "0 fotos";
  imageBaseUrl!: string;

  constructor(
    private _service: VeiculoService,
    private _RemocaoService: RemocaoService,
    private router: Router
  ) { }

  ngOnInit(): void {
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (this.veiculo) {

      this.textoOpcionais = this.formatarOpcionais(this.veiculo.opcionais);

      if (this.veiculo.fotos.length > 0) {
        this.imageBaseUrl = this.applyCacheBuster(environment.imagemRoute + this.veiculo.fotos[0].path);
        this.labelFotos = this.veiculo.fotos.length > 1 ? this.veiculo.fotos.length + "  fotos" : this.veiculo.fotos.length + " foto";
      } else {
        this.imageBaseUrl = "../../../assets/carros/imageBlank.jpg";
      }
    }
  }

  private applyCacheBuster(url: string): string {
    const separador = url.includes('?') ? '&' : '?';
    const cacheBusterQuery = `v=${new Date().getTime()}`;
    return `${url}${separador}${cacheBusterQuery}`;
  }

  private formatarOpcionais(opcionais: OpcionalVeiculoDto[] | null | undefined): string {
    if (opcionais && opcionais.length > 0) {
      return opcionais.map(op => op.descricao).join('\n');
    }
    return 'Nenhum opcional';
  }


  editarVeiculo(id: number): void {
    this.router.navigate(['/veiculo/editar', id]);
  }
  verFotosVeiculo(id: number): void {
    this.router.navigate(['/veiculo/fotos', id]);
  }
  deletarVeiculo(id: number): void {
    this._service.deletarVeiculo(id).subscribe({
      next: () => {
        SwalHandler.showSucesso('Sucesso', 'Veículo deletado com sucesso!');
        this._RemocaoService.emitirVeiculoDeletado();
        this.veiculoDeletado.emit();

      },
      error: () =>
        SwalHandler.showFalha('Falha', 'Ocorreu um erro ao deletar o veículo.')
    });
  }


  public coletarDadosDosPortais(): { veiculoId: number, portalId: number, pacoteId: number | null }[] {
    if (!this.cardPortais) {
      return [];
    }

    return this.cardPortais.map(card => card.obterDadosParaSalvar());
  }

  desmarcarTodosOsPortais(): void {
    if (this.cardPortais) {
      this.cardPortais.forEach(card => {
        card.desmarcarCheckboxes();
      });
    }
  }
}
