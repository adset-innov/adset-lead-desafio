import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button'
import { MatToolbarModule } from '@angular/material/toolbar';
import { Route, Router, RouterModule } from '@angular/router';
import { FlexModule } from '@ngbracket/ngx-layout';
import {MatTableModule} from '@angular/material/table';
import {Sort, MatSortModule} from '@angular/material/sort';
import {MatPaginatorModule, PageEvent} from '@angular/material/paginator';
import {MatCardModule} from '@angular/material/card';
import { CarroService } from '../carros/carro/carro.service';
import { MatIconModule } from '@angular/material/icon';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import { NgxMaskDirective,NgxMaskPipe } from 'ngx-mask';
import {MatSelectModule} from '@angular/material/select';
import {MatDialog, MatDialogModule} from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import {TooltipPosition, MatTooltipModule} from '@angular/material/tooltip';
import { DialogComponent } from '../common/dialog/dialog.component';
import {MatCheckboxModule} from '@angular/material/checkbox';
@Component({
  selector: 'app-home',
  standalone: true,
  imports: [MatCheckboxModule, MatTooltipModule, MatSelectModule, RouterModule, NgxMaskPipe, MatInputModule, NgxMaskDirective, MatFormFieldModule, FlexModule, MatIconModule,ReactiveFormsModule, MatButtonModule, RouterModule, MatToolbarModule, MatTableModule, MatCardModule, MatSortModule, MatPaginatorModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {
 totalVeiculos = 0;
 totalVeiculosComFoto = 0;
 totalVeiculosSemFoto = 0;
 anos: any[] = [];
 carros: any;
 public form!: FormGroup;
//  diamanteFeirao: boolean= false;
//  diamante: boolean= false;
//  platinum: boolean= false;
//  basico: boolean= false;

 tamanho: number = 0;
 tamanhoPagina: number = 0;

  constructor(private _carroService: CarroService,
    private router: Router,
    private _snackBar: MatSnackBar,
    private formBuilder: FormBuilder, public dialog: MatDialog) {
    this.buildForm();
  }

  ngOnInit(): void {
    this.atualizar();
  }

  buildForm() {
    this.form = this.formBuilder.group({
      marca:[''],
      modelo:[''],
      anoMin:[''],
      anoMax:[''],
      placa:[''],
      cor:[''],
      preco:[''],
      opcional:[]
    })
  }

  getTotalVeiculosComFoto()
  {
    this._carroService.getTotais('totalComFoto')
    .subscribe(data => {
      this.totalVeiculosComFoto = data;
    })
  }

  getTotalVeiculosSemFoto()
  {
    this._carroService.getTotais('totalSemFoto')
    .subscribe(data => {
      this.totalVeiculosSemFoto = data;
    })
  }

  getTotalVeiculos()
  {
    this._carroService.getTotais('total')
    .subscribe(data => {
      this.totalVeiculos = data;
    })
  }

  buscar(form: FormGroup, tamanho: number=1, itensPorPagina: number=10)
  {
    let paginacao = {
      tamanhoPagina: itensPorPagina,
      numeroPagina: tamanho,
      marca: '',
      modelo: '',
      anoMin: null,
      anoMax: null,
      placa: '',
      cor: '',
      preco: null,
      opcional: ''
    }

    if(form.value.marca !=='')
      paginacao.marca = form.value.marca;

    if(form.value.modelo !=='')
      paginacao.modelo = form.value.modelo;

    if(form.value.anoMin !=='' && form.value.anoMin !=='0')
      paginacao.anoMin = form.value.anoMin;

    if(form.value.anoMax !==''&& form.value.anoMax !== '0')
      paginacao.anoMax = form.value.anoMax;

    if(form.value.placa !=='')
       paginacao.placa = form.value.placa;

    if(form.value.cor !=='')
      paginacao.cor = form.value.cor;
    
    if(form.value.preco !=='')
      paginacao.preco = form.value.preco;
    
    if(form.value.opcional !=='')
      paginacao.opcional = form.value.opcional;
    this._carroService.getCarros(paginacao)
    .subscribe(data => {
      this.carros = data;   
      this.tamanhoPagina = data.tamanhoPagina;
      this.tamanho = data.quantidadeTotal;      
    })
  }

  atualizar()
  {
    this.form.reset();
    this.getTotalVeiculosComFoto();
    this.getTotalVeiculosSemFoto();
    this.getTotalVeiculos();
    this.getAnos();
    this.buscar(this.form)
  }

  getAnos(){
    this._carroService.getAnos()
    .subscribe(data => {
      this.anos = data;
    })
  }

  excluir(id: string)
  {
    this._carroService.deleteCarro(id)
    .subscribe(data => {
      this.openSnackBar('Registro excluido com sucesso');
      this.atualizar();
    })
  }

  editar(id: string)
  {
    this.router.navigate(['carros/cadastrar/' + id]);
  }

  convertImg(type: string, bytes: string[]){
    var base64Content = `data:${type};base64,${bytes}`;
    return base64Content;
  }

  handlePageEvent(e: PageEvent){
    let pagina = 0;
    if(e.pageIndex === 0)
      pagina = 1;
    else
      pagina = (e.pageIndex+1);

    this.buscar(this.form, pagina, e.pageSize);
  }

  openDialog(item: any, ehOpcional: boolean) {

    const dialogRef = this.dialog.open(DialogComponent,{
      data: {
        item: item,
        ehOpcional: ehOpcional
      },
      width: '400px'
    });
    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }

  openSnackBar(msg: string)
  {
    this._snackBar.open(msg, 'Fechar', {
      horizontalPosition: 'center',
      verticalPosition: 'bottom' ,
    });
  
  }

  vincularPacote(idCarro: string, tipoPacote: number){

    let pacote ={
      idCarro: idCarro,
      tipoPacote: tipoPacote
    }
    this._carroService.vincularPacote(pacote)
    .subscribe(data => {
    })
  }

  excluirPacote(idCarro: string, tipoPacote: number){
    this._carroService.deletePacote(idCarro, tipoPacote)
    .subscribe(data => {
    })
  }

  verificarPacote(event: any, tipoPacote: number, idCarro: string){
    if(event.checked){
      this.vincularPacote(idCarro, tipoPacote);
    }else{
      this.excluirPacote(idCarro, tipoPacote);
    }
  }

  checarPacote(item:any, tipoPacote: number): boolean {
    if(item.pacotes.filter((x: { tipoPacote: number; })=> x.tipoPacote === tipoPacote).length > 0){
      return true;
    }
    return false;
  }
}
