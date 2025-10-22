import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EdicaoService {

  private salvarTriggerSource = new Subject<void>();
  public salvarTrigger$ = this.salvarTriggerSource.asObservable();
  constructor() { }
  public dispararSalvar(): void {
    this.salvarTriggerSource.next();
  }
}
