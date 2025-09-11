
import { Component } from '@angular/core';

type PortalFlag = 'iCarros' | 'WebMotors';

interface Vehicle {
  plate: string;
  make: string;
  model: string;
  year: number;
  km: number;
  color: string;
  price: number;
  photo: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  // filtros simulados
  filters = {
    plate: '', make: '', model: '',
    yearMin: '', yearMax: '', price: '',
    photos: 'Todos', optionals: '', color: 'Todos'
  };

  portals: PortalFlag[] = ['iCarros', 'WebMotors'];
  selectedPortal: PortalFlag = 'iCarros';

  page = 4;
  pageSize = 100;
  total = 110;

  vehicles: Vehicle[] = [
    {
      plate: 'AAA-0102',
      make: 'Volkswagen',
      model: 'Golf Variant',
      year: 2016,
      km: 25000,
      color: 'Branco',
      price: 103900,
      photo: 'https://images.unsplash.com/photo-1619767886558-efdc259cde1e?q=80&w=1200&auto=format&fit=crop'
    }
  ];

  get withPhotosCount() { return 80; }
  get withoutPhotosCount() { return 30; }

  search() { /* aqui entraria sua chamada de API */ }
  save()   { /* salvar filtros/estoque */ }
  export() { /* exportar CSV/Excel */ }

  setPortal(p: PortalFlag) { this.selectedPortal = p; }

  brl(value: number) {
    return new Intl.NumberFormat('pt-BR', { style:'currency', currency:'BRL' }).format(value);
  }
}
