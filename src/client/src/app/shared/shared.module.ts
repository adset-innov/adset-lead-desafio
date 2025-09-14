import { NgModule } from '@angular/core';
import { HeaderComponent } from './components/header/header.component';
import { DiagonalSeparatorSlashComponent } from './components/diagonal-separator-slash/diagonal-separator-slash.component';
import { VerticalSeparatorSlashComponent } from './components/vertical-separator-slash/vertical-separator-slash.component';

@NgModule({
  imports: [],
  exports: [
    HeaderComponent,
    DiagonalSeparatorSlashComponent,
    VerticalSeparatorSlashComponent,
  ],
  declarations: [
    HeaderComponent,
    DiagonalSeparatorSlashComponent,
    VerticalSeparatorSlashComponent,
  ],
})
export class SharedModule {}
