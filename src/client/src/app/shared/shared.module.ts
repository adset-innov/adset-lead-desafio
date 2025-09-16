import { NgModule } from '@angular/core';
import { HeaderComponent } from './components/header/header.component';
import { DiagonalSeparatorSlashComponent } from './components/diagonal-separator-slash/diagonal-separator-slash.component';
import { VerticalSeparatorSlashComponent } from './components/vertical-separator-slash/vertical-separator-slash.component';
import { PaginatorComponent } from './components/paginator/paginator.component';
import { CommonModule } from '@angular/common';

@NgModule({
  imports: [CommonModule],
  exports: [
    HeaderComponent,
    PaginatorComponent,
    DiagonalSeparatorSlashComponent,
    VerticalSeparatorSlashComponent,
  ],
  declarations: [
    HeaderComponent,
    PaginatorComponent,
    DiagonalSeparatorSlashComponent,
    VerticalSeparatorSlashComponent,
  ],
})
export class SharedModule {}
