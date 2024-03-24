import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-page-not-found',
  standalone: true,
  imports: [RouterLink],
  template: `
   <p>
      Esta página não existe. volte para O Inicio
      <a mat-button routerLink="/home">Inicio</a>.
    </p>
  `,
  styles: ``
})
export class PageNotFoundComponent {

}
