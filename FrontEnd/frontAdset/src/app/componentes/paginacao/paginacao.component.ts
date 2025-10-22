import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-paginacao',
  templateUrl: './paginacao.component.html',
  styleUrls: ['./paginacao.component.css']
})
export class PaginacaoComponent {
  @Input() paginaAtual = 1;
  @Input() totalPaginas = 1;
  @Output() paginaMudou = new EventEmitter<number>();

  irParaPagina(pagina: number) {
    if (pagina < 1 || pagina > this.totalPaginas) return;
    this.paginaMudou.emit(pagina);
  }
  constructor() { }

 
}
