import { Component, OnInit } from '@angular/core';
import { VeiculoService } from '../../services/veiculo/veiculo-service.service';

@Component({
  selector: 'app-lista-veiculos',
  templateUrl: './lista-veiculos.component.html',
  styleUrls: ['./lista-veiculos.component.css']
})
export class ListaVeiculosComponent implements OnInit {

  constructor(   private _service: VeiculoService,) { }

  ngOnInit(): void {
    this.listarCardsVeiculos();
  }

  listarCardsVeiculos(){

  }
}
