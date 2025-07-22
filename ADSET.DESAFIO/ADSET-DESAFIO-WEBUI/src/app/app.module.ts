import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { SharedModule } from './shared/shared.module';

import { ToolbarComponent } from './components/layout/toolbar/toolbar.component';
import { SummaryToolbarComponent } from './components/layout/summary-toolbar/summary-toolbar.component';
import { FiltersComponent } from './components/layout/filters/filters.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CarListComponent } from './components/layout/car-list/car-list.component';
import { ListControlsComponent } from './components/layout/list-controls/list-controls.component';
import { PaginationComponent } from './components/layout/pagination/pagination.component';

@NgModule({
  declarations: [
    AppComponent,
    ToolbarComponent,
    SummaryToolbarComponent,
    FiltersComponent,
    CarListComponent,
    ListControlsComponent,
    PaginationComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    SharedModule,
    BrowserAnimationsModule
  ],
  exports: [
    SummaryToolbarComponent
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }