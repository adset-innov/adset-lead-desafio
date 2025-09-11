import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-vehicles',
  templateUrl: './vehicles.component.html',
  styleUrls: ['./vehicles.component.scss']
})
export class VehiclesComponent implements OnInit {
   filtrosVisiveis = true;

  constructor() { }

  ngOnInit(): void {
  }

  toggleFiltros() {
    this.filtrosVisiveis = !this.filtrosVisiveis;
  }

}
