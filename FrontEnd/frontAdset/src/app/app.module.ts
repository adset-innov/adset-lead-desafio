import { NgModule,LOCALE_ID  } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MatIconModule } from '@angular/material/icon';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CabecalhoComponent } from './componentes/cabecalho/cabecalho.component';
import { PainelEstatisticasComponent } from './componentes/painel-estatisticas/painel-estatisticas.component';
import { FiltroVeiculosComponent } from './componentes/filtro-veiculos/filtro-veiculos.component';
import { BarraAcoesComponent } from './componentes/barra-acoes/barra-acoes.component';
import { ListaVeiculosComponent } from './componentes/lista-veiculos/lista-veiculos.component';
import { CardVeiculoComponent } from './componentes/card-veiculo/card-veiculo.component';
import { PaginacaoComponent } from './componentes/paginacao/paginacao.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BotaoExportarComponent } from './componentes/botao-exportar/botao-exportar.component';
import { BotaoCadastrarComponent } from './componentes/botao-cadastrar/botao-cadastrar.component';
import { BotaoSalvarComponent } from './componentes/botao-salvar/botao-salvar.component';
import { MatTooltipModule } from '@angular/material/tooltip';
import { BotaoBuscarComponent } from './componentes/botao-buscar/botao-buscar.component';
import { BotaoDeletarComponent } from './componentes/botao-deletar/botao-deletar.component';
import { CardPortalComponent } from './componentes/card-portal/card-portal.component';
import { FormCadastrarComponent } from './componentes/form-cadastrar/form-cadastrar.component';
import { FormEditarComponent } from './componentes/form-editar/form-editar.component';
import { TelaCadastroComponent } from './componentes/tela-cadastro/tela-cadastro.component';
import { HomeComponent } from './componentes/home/home.component';
import { TelaEdicaoComponent } from './componentes/tela-edicao/tela-edicao.component';
import { PainelFotosComponent } from './componentes/painel-fotos/painel-fotos.component';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { registerLocaleData } from '@angular/common';
import localePt from '@angular/common/locales/pt';
import { BotaoAcaoComponent } from './componentes/botao-acao/botao-acao.component';
import { BotaoAcaoVerticalComponent } from './componentes/botao-acao-vertical/botao-acao-vertical.component';
import { BotaoLimparComponent } from './componentes/botao-limpar/botao-limpar.component';



registerLocaleData(localePt);

@NgModule({
  declarations: [
    AppComponent,
    CabecalhoComponent,
    PainelEstatisticasComponent,
    FiltroVeiculosComponent,
    BarraAcoesComponent,
    ListaVeiculosComponent,
    CardVeiculoComponent,
    PaginacaoComponent,
    BotaoExportarComponent,
    BotaoCadastrarComponent,
    BotaoSalvarComponent,
    BotaoBuscarComponent,
    BotaoDeletarComponent,
    CardPortalComponent,
    FormCadastrarComponent,
    FormEditarComponent,
    TelaCadastroComponent,
    HomeComponent,
    TelaEdicaoComponent,
    PainelFotosComponent,
    BotaoAcaoComponent,
    BotaoAcaoVerticalComponent,
    BotaoLimparComponent


  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatIconModule,
    MatTooltipModule,
    HttpClientModule,
    ReactiveFormsModule

  ],
 providers: [
  { provide: LOCALE_ID, useValue: 'pt-BR' }
],
  bootstrap: [AppComponent]
})
export class AppModule { }
