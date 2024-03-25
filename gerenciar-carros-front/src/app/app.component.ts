import { Component, DestroyRef, OnInit, inject } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule, MatIconRegistry } from '@angular/material/icon'
import { DomSanitizer } from '@angular/platform-browser'
import { FlexLayoutModule, FlexModule, MediaObserver } from '@ngbracket/ngx-layout';
import { combineLatest, tap } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop'
import {MatSidenavModule} from '@angular/material/sidenav';
import { MatButtonModule } from '@angular/material/button'
import { MatTooltipModule } from '@angular/material/tooltip'
import { AsyncPipe } from '@angular/common';
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet, 
    RouterLink, 
    MatToolbarModule, 
    MatIconModule, 
    FlexLayoutModule, 
    FlexModule, 
    MatSidenavModule,
    MatTooltipModule,
    MatButtonModule,
    AsyncPipe],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  private destroyRef = inject(DestroyRef)
  opened!: boolean
  constructor(iconRegistry: MatIconRegistry, sanitizer: DomSanitizer,
    public media: MediaObserver) {
    iconRegistry.addSvgIcon(
      'lead-icon',
      sanitizer.bypassSecurityTrustResourceUrl('assets/img/icons/adset-lead.svg')
    )
  } 

  ngOnInit(): void {
    combineLatest([this.media.asObservable()])
      .pipe(
        tap(([mediaValue]) => {
            if (mediaValue[0].mqAlias === 'xs') {
              this.opened = false
            } else {
              this.opened = true
            }          
        }),
        takeUntilDestroyed(this.destroyRef)
      )
      .subscribe()
  }
}
