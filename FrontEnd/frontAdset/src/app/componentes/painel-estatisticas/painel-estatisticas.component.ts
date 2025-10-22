import { Component, OnInit, OnDestroy } from '@angular/core';
import { VeiculoService } from '../../services/veiculo/veiculo-service.service';
import { InformacoesVeiculosDto } from '../../interfaces/Veiculo/InformacoesVeiculosDto';
import { SwalHandler } from 'src/app/utils/SwalHandler';
import { RemocaoService } from 'src/app/services/Triggers/remocao.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-painel-estatisticas',
  templateUrl: './painel-estatisticas.component.html',
  styleUrls: ['./painel-estatisticas.component.css']
})
export class PainelEstatisticasComponent implements OnInit, OnDestroy {

  constructor(private _service: VeiculoService, private _remocaoService: RemocaoService) { }
  private sub!: Subscription;
  informacoesVeiculos: InformacoesVeiculosDto = {
    ids: [],
    total: 0,
    totalFotos: 0,
    totalSemFotos: 0
  };

  ngOnInit(): void {
    this.ObterInformacoesVeiculos();

    this.sub = this._remocaoService.veiculoDeletado$.subscribe(() => {
      this.ObterInformacoesVeiculos();
    });
  }

  ObterInformacoesVeiculos(): void {
    this._service.ObterInformacoesVeiculos().subscribe({
      next: (dados) => {
        this.informacoesVeiculos = dados;
      },
      error: (err) => {
        SwalHandler.showFalha('Falha', 'Não foi possivel carregar o total de veiculos e suas fotos ');
      }
    });
  }

    ngOnDestroy() {
    this.sub.unsubscribe();
  }
}

