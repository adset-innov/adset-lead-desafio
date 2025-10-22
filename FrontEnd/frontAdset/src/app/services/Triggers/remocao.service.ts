import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RemocaoService {

  constructor() { }
  private veiculoDeletadoSource = new Subject<number>(); 
  veiculoDeletado$ = this.veiculoDeletadoSource.asObservable();

  emitirVeiculoDeletado() {
    this.veiculoDeletadoSource.next();
  }
}
