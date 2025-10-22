import { Component, OnInit,Output,EventEmitter } from '@angular/core';

@Component({
  selector: 'app-botao-salvar',
  templateUrl: './botao-salvar.component.html',
  styleUrls: ['./botao-salvar.component.css']
})
export class BotaoSalvarComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }
  @Output() salvar = new EventEmitter<void>(); 


  onBotaoClicado(): void {
   
    this.salvar.emit();
  }
}
