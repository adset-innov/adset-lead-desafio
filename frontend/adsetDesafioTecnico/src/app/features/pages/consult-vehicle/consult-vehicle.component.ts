import { VehicleResultDTO } from './../../../shared/DTOs/VehicleResultDTO';
import { Component, OnInit } from '@angular/core';
import {
  CarsService,
  ResumoVeiculos,
} from 'src/app/api/carServices/cars.service';
import { CarsFilterDTO } from 'src/app/shared/DTOs/CarsFilterDTO';
import { DefaultReturn } from 'src/app/shared/models/DefaultReturn';
import { PostVehicleComponent } from './post-vehicle/post-vehicle.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-consult-vehicle',
  templateUrl: './consult-vehicle.component.html',
  styleUrls: ['./consult-vehicle.component.css'],
})
export class ConsultVehicleComponent implements OnInit {
  defaultImg = 'assets/defaultimage.png';
  filters: any = {
    placa: '',
    marca: '',
    modelo: '',
    anoMin: '',
    anoMax: '',
    preco: '',
    fotos: '',
    opcionais: '',
    cor: '',
  };
  total = 0;
  comFotos = 0;
  semFotos = 0;

  veiculos: any[] = [];
  loading = false;

  anos: number[] = [];
  precoOptions = [
    { label: 'Até R$ 20.000', value: '20000' },
    { label: 'Até R$ 50.000', value: '50000' },
    { label: 'Até R$ 100.000', value: '100000' },
    { label: 'Até R$ 150.000', value: '150000' },
  ];
  fotoOptions = [
    { label: 'Com Fotos', value: 'com' },
    { label: 'Sem Fotos', value: 'sem' },
  ];
  corOptions = [
    { label: 'Branco', value: 0 },
    { label: 'Preto', value: 1 },
    { label: 'Prata', value: 2 },
    { label: 'Cinza', value: 3 },
    { label: 'Vermelho', value: 4 },
    { label: 'Azul', value: 5 },
    { label: 'Verde', value: 6 },
    { label: 'Amarelo', value: 7 },
    { label: 'Marrom', value: 8 },
    { label: 'Laranja', value: 9 },
    { label: 'Outra', value: 99 },
  ];

  constructor(private carsService: CarsService, private dialog: MatDialog) {}

  ngOnInit() {
    const currentYear = new Date().getFullYear();
    for (let i = currentYear; i >= 1995; i--) {
      this.anos.push(i);
    }

    this.carsService
      .getResumoVehicle()
      .subscribe((res: DefaultReturn<ResumoVeiculos>) => {
        this.total = res.data?.total ?? 0;
        this.comFotos = res.data?.comFotos ?? 0;
        this.semFotos = res.data?.semFotos ?? 0;
      });
  }
  onSearch() {
    const dto: CarsFilterDTO = {
      anoMin: this.filters.anoMin || undefined,
      anoMax: this.filters.anoMax || undefined,
      preco: this.filters.preco ? Number(this.filters.preco) : undefined,
      somenteComFotos: this.filters.fotos === 'com' ? true : false,
      cor: this.filters.cor !== undefined ? this.filters.cor : undefined,
      opcionais:
        this.filters.opcionais && this.filters.opcionais.length > 0
          ? this.filters.opcionais
          : undefined,
    };

    this.loading = true;
    this.carsService.getFilteredVehicles(dto).subscribe({
      next: (res) => {
        this.veiculos = res.data || [];
        this.loading = false;
      },
      error: () => {
        this.veiculos = [];
        this.loading = false;
      },
    });
  }

  clearFilters() {
    this.filters = {
      placa: '',
      marca: '',
      modelo: '',
      anoMin: '',
      anoMax: '',
      preco: '',
      fotos: '',
      opcionais: '',
      cor: '',
    };
  }

  getCorLabel(corValue: number): string {
  const cor = this.corOptions.find(c => c.value === corValue);
  return cor ? cor.label : 'Desconhecida';
}

  copydata(veiculo: VehicleResultDTO) {
    const text = `
    Marca: ${veiculo.marca}
    Modelo: ${veiculo.modelo}
    Ano: ${veiculo.ano}
    Preço: ${veiculo.preco}
    Cor: ${veiculo.cor}
    Placa: ${veiculo.placa}
    Km: ${veiculo.km}
  `.trim();
    navigator.clipboard.writeText(text);
  }
  deleteVehicle(e: any) {
     this.carsService.DeleteVehicle(e.id).subscribe({
    next: (res: DefaultReturn<ResumoVeiculos>) => {
      // Atualiza a lista local de veículos, removendo o que foi deletado
      this.veiculos = this.veiculos.filter(v => v.id !== e.id);
    },
    error: (err) => {
      console.error('Erro ao deletar veículo:', err);
      // Aqui você pode mostrar um toast ou alerta se quiser
    }
  });
  }

  PostVehicle(e:any) {
    const dialogRef = this.dialog.open(PostVehicleComponent, {
      width: '600px',
      disableClose: true,
      data: { carData: e },
    });

    dialogRef.componentInstance.onSubmit.subscribe((data) => {
      dialogRef.close();

       this.carsService
      .PutVehicle(data)
      .subscribe((res: DefaultReturn<ResumoVeiculos>) => {
        this.total = res.data?.total ?? 0;
        this.comFotos = res.data?.comFotos ?? 0;
        this.semFotos = res.data?.semFotos ?? 0;
      });
    });

    dialogRef.componentInstance.onCancel.subscribe(() => {
      dialogRef.close();
    });
  }
}
