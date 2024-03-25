import { Component, OnInit, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { FlexModule } from '@ngbracket/ngx-layout';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatToolbarModule } from '@angular/material/toolbar';
import { CarroService } from '../carro/carro.service';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { NgxMaskDirective } from 'ngx-mask';
import { MatDividerModule } from '@angular/material/divider';
import { MatListModule } from '@angular/material/list';
import {
  MatSnackBar,
} from '@angular/material/snack-bar';
@Component({
  selector: 'app-cadastrar',
  standalone: true,
  imports: [
    MatListModule,
    MatDividerModule,
    NgxMaskDirective,
    MatIconModule,
    MatCardModule,
    ReactiveFormsModule,
    RouterModule,
    FlexModule,
    MatButtonModule,
    MatInputModule,
    MatFormFieldModule,
    FormsModule,
    MatToolbarModule,
  ],
  templateUrl: './cadastrar.component.html',
  styleUrl: './cadastrar.component.scss',
})
export class CadastrarComponent implements OnInit {
  durationInSeconds = 5;
  public opcional = '';
  public form!: FormGroup;
  listOpcionais: any[] = [];
  listaImagens: any[] =[];
  public fileList = new Array<File>();
  private activatedRoute = inject(ActivatedRoute);
  id: string = '';

  constructor(
    private _snackBar: MatSnackBar,
    private _carroService: CarroService,
    private formBuilder: FormBuilder
  ) {
    this.id = this.activatedRoute.snapshot.params['id'];
  }

  ngOnInit() {
    this.buildForm();
    if (this.id !== undefined && this.id !== '') {
     this.obterVeiculo();
    }
  }

  obterVeiculo(){
    this._carroService.getById(this.id).subscribe((data) => {
      this.form.patchValue(data);
      this.listOpcionais = [];
      this.listaImagens = [];
      data?.opcionais.forEach((element: { id: any; descricao: any; }) => {
        this.listOpcionais.push({id:element.id, descricao: element.descricao})
      }); 
      data?.imagens.forEach((element: { id: any; nome: any; }) => {
        this.listaImagens.push({id: element.id, nome: element.nome})
      });       
    });
  }

  buildForm() {
    this.form = this.formBuilder.group({
      id: [],
      marca: ['', Validators.required],
      modelo: ['', Validators.required],
      ano: ['', Validators.required],
      placa: ['', Validators.required],
      km: [],
      cor: ['', Validators.required],
      preco: ['', Validators.required],
    });
  }

  salvar(form: FormGroup) {
    let value = form.value;
    if(this.listOpcionais.length > 0){
      value.opcionais = this.listOpcionais;
    }
    
    if (form.valid) {
      if (this.id === undefined || this.id === '') {
        this._carroService.createCarro(value).subscribe((data) => {
          console.log(data);
          this.uploadFiles(data?.id);
        });
      } else {
        this._carroService.updateCarro(this.id, value).subscribe((data) => {
          console.log(data);
          this.uploadFiles(data?.id);
        });
      }
    } else {
      this.openSnackBar('Ocorreu um erro ao salvar!!');
    }
  }

  uploadFiles(idCarro: string) {
    let formData = new FormData();
    this.fileList.forEach((element) => {
      formData.append('imagens', element);
    });

    this._carroService.uploadImagens(formData, idCarro).subscribe((data) => {
      this.openSnackBar('Registro salvo com sucesso');
      if (this.id !== undefined && this.id !== '') {
        this.obterVeiculo();
       }else{
      this.form.reset();
      this.fileList = [];
      this.listOpcionais = [];
      this.listaImagens = [];
       }
    });
  }

  fileChange = (event: any) => {
    const files: FileList = event.target.files;

    if (files.length > 15) {
      this.openSnackBar('Tamanho de arquivos ultrapassa 15');
      return;
    } else {
      let totalFiles = (files.length + this.fileList.length);
      if (totalFiles > 15) {
        this.openSnackBar('Tamanho de arquivos ultrapassa 15');
        return;
      } else {
        for (let index = 0; index < files.length; index++) {
          const element = files[index];
          this.fileList.push(element);
          this.listaImagens.push({id:'', nome: element.name})
        }
      }
    }
  };

  excluirImagem(item: any) {
    this.fileList = this.fileList.filter((x) => x.name !== item.nome);
    this.listaImagens = this.listaImagens.filter(x=> x.nome !== item.nome);
    if(item.id !== '')
    {
      this.excluirImagemDB(item.id)
    }else{
      this.openSnackBar('Imagem excluida com sucesso');
    }
  }

  excluirImagemDB(id: string) {
    this._carroService.deleteImagem(id).subscribe((data) => {
      this.openSnackBar('Imagem excluida com sucesso');
      this.obterVeiculo();
    });
  }

  openSnackBar(msg: string) {
    this._snackBar.open(msg, 'Fechar', {
      horizontalPosition: 'center',
      verticalPosition: 'bottom',
    });
  }

  excluirOpcional(item: any) {
    this.listOpcionais = this.listOpcionais.filter((x) => x.descricao !== item.descricao);
    if(item.id !==''){
      this.excluirOpcionalDB(item.id);
    }else{
      this.openSnackBar('Opcional excluido com sucesso');
    }
  }
  excluirOpcionalDB(id: string){
    this._carroService.deleteOpcional(id).subscribe((data) => {
      this.openSnackBar('Opcional excluido com sucesso');
      this.obterVeiculo();
    });
  }

  addOpcional() {
    if (this.opcional !== '') {
      var opc = {
        id: null,
        descricao: this.opcional,
      };
      this.listOpcionais.push(opc);
    } else {
      this.openSnackBar('Informe o nome do opcional');
    }
    this.opcional = '';
  }
}
