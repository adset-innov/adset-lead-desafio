import { Component, OnInit } from '@angular/core';
import { FotoDto } from 'src/app/interfaces/Foto/FotoDto';
import { VeiculoService } from 'src/app/services/veiculo/veiculo-service.service';
import { environment } from 'src/environments/environment';
import { SwalHandler } from 'src/app/utils/SwalHandler';
import { Router, ActivatedRoute } from '@angular/router';
import { finalize } from 'rxjs/operators';
import { FotoService } from 'src/app/services/foto/foto.service';

@Component({
  selector: 'app-painel-fotos',
  templateUrl: './painel-fotos.component.html',
  styleUrls: ['./painel-fotos.component.css']
})
export class PainelFotosComponent implements OnInit {
  fotos: FotoDto[] = [];

  public carrosselAberto = false;
  public fotoAtualIndex = 0;
  public isLoadingFoto = false;
  fotosSelecionadas: File[] = [];
  constructor(private _service: VeiculoService, private router: Router, private route: ActivatedRoute, private _fotoService: FotoService) { }

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));

    this.carregarFotosVeiculo(id);

  }


  private carregarFotosVeiculo(id: number): void {
    this._service.ObterFotosVeiculo(id).subscribe({
      next: (fotos: FotoDto[]) => {
        this.fotos = fotos ?? [];
        this.gerarUrls();
      },
      error: (err) => {
        SwalHandler.showFalha("Falha", "Algo deu errado ao consultar as fotos do veiculo");
      }
    });
  }

  private applyCacheBuster(url: string): string {
    const separador = url.includes('?') ? '&' : '?';
    const cacheBusterQuery = `v=${new Date().getTime()}`;
    return `${url}${separador}${cacheBusterQuery}`;
  }

  private gerarUrls() {
    this.fotos.forEach(foto => {
      const baseUrl = environment.imagemRoute + foto.path;
      foto.path = this.applyCacheBuster(baseUrl);
    });
  }

  trackByFotoId(index: number, foto: FotoDto): number {
    return foto.id;
  }

  public abrirCarrossel(index: number): void {
    this.fotoAtualIndex = index;
    this.carrosselAberto = true;
  }


  public fecharCarrossel(): void {
    this.carrosselAberto = false;
  }


  public fotoAnterior(): void {
    this.fotoAtualIndex = (this.fotoAtualIndex > 0)
      ? this.fotoAtualIndex - 1
      : this.fotos.length - 1;
  }


  public proximaFoto(): void {
    this.fotoAtualIndex = (this.fotoAtualIndex < this.fotos.length - 1)
      ? this.fotoAtualIndex + 1
      : 0;
  }


  public get fotoAtual(): FotoDto {
    return this.fotos[this.fotoAtualIndex];
  }



  public onExcluirClick(): void {
    const idParaExcluir = this.fotoAtual.id;
       this._fotoService.DeletarFoto(idParaExcluir).subscribe({
      next: () => {
        SwalHandler.showSucesso("Feito","Foto Deletada com sucesso");
      },
      error: (err) => {
        SwalHandler.showFalha("Falha", "Algo deu errado ao deletar a foto");
      }
    });

    this.fotos.splice(this.fotoAtualIndex, 1);

    if (this.fotos.length === 0) {
      this.fecharCarrossel();
    } else {

      this.fotoAtualIndex = Math.min(this.fotoAtualIndex, this.fotos.length - 1);
    }
  }




  public onSubstituirChange(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      const file = input.files[0];
      const idParaSubstituir = this.fotoAtual.id;
      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.fotoAtual.path = e.target.result;
      };
      reader.readAsDataURL(file);

      this.isLoadingFoto = true;
      this._fotoService.EditarFoto(idParaSubstituir, file).pipe(
        finalize(() => {
          this.isLoadingFoto = false;
          input.value = '';
        })
      ).subscribe({
        next: (fotoDto: FotoDto) => {

          const realUrl = environment.imagemRoute + fotoDto.path;
          const separador = realUrl.includes('?') ? '&' : '?';
          const cacheBuster = `${separador}v=${new Date().getTime()}`;
          const finalUrl = this.applyCacheBuster(realUrl);

          this.fotoAtual.path = finalUrl;
          this.fotoAtual.arquivo = fotoDto.arquivo;

          const fotoNoArray = this.fotos.find(f => f.id === idParaSubstituir);
          if (fotoNoArray) {
            fotoNoArray.path = finalUrl;
            fotoNoArray.arquivo = fotoDto.arquivo;
          }

          SwalHandler.showSucesso("Feito", "Foto editada com sucesso");
        },

        error: (err) => {
          SwalHandler.showFalha("Falhou", "Ocorreu um problema ao efetuar a edição da foto");

        }
      });
    }
  }



abrirSeletorFotos(): void {
  const input = document.getElementById('inputFotos') as HTMLInputElement;
  input.click();
}

onSelecionarFotos(event: Event): void {
  const input = event.target as HTMLInputElement;
  if (!input.files) return;

  const arquivos = Array.from(input.files);

  if (arquivos.length > 15) {
    SwalHandler.showAtencao('Limite excedido', 'Você pode enviar no máximo 15 fotos.');
    input.value = ''; 
    return;
  }

  this.fotosSelecionadas = arquivos;

  const id = Number(this.route.snapshot.paramMap.get('id'));

  this._fotoService.CadastrarFotos(id, this.fotosSelecionadas).subscribe({
    next: () => SwalHandler.showSucessoRedirecionamento(this.router,'Sucesso', 'Fotos cadastradas com sucesso!',""),
    error: () => SwalHandler.showFalha('Erro', 'Não foi possível enviar as fotos.')
  });

  input.value = ''; 
}

abrirSeletorMaisFotos(): void {
  const input = document.getElementById('inputMaisFotos') as HTMLInputElement;
  input.click();
}

onSelecionarMaisFotos(event: Event): void {
  const input = event.target as HTMLInputElement;
  if (!input.files) return;

  const arquivos = Array.from(input.files);
  const restantes = 15 - this.fotos.length;

  if (arquivos.length > restantes) {
    SwalHandler.showAtencao('Limite excedido', `Você pode enviar no máximo ${restantes} fotos.`);
    input.value = '';
    return;
  }

  const id = Number(this.route.snapshot.paramMap.get('id'));

  this._fotoService.CadastrarFotos(id, arquivos).subscribe({
    next: () => {
      SwalHandler.showSucesso('Sucesso', 'Fotos adicionadas com sucesso!');
      this.carregarFotosVeiculo(id); 
    },
    error: () => SwalHandler.showFalha('Erro', 'Não foi possível enviar as fotos.')
  });

  input.value = '';
}
}
