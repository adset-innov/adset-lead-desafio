import { Component, OnInit } from '@angular/core';
import { EdicaoService } from 'src/app/services/Triggers/edicao.service';

@Component({
  selector: 'app-barra-acoes',
  templateUrl: './barra-acoes.component.html',
  styleUrls: ['./barra-acoes.component.css']
})
export class BarraAcoesComponent implements OnInit {

  constructor(private _EdicaoService:EdicaoService) { }

  ngOnInit(): void {
  }
  onSalvarTudoClick(): void {
    this._EdicaoService.dispararSalvar();
  }
}
