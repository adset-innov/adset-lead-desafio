import { Component, OnInit } from '@angular/core';
import { VeiculoService } from 'src/app/services/veiculo/veiculo-service.service';
import * as XLSX from 'xlsx';
import * as FileSaver from 'file-saver';
import { VeiculoFiltroDto } from 'src/app/interfaces/Veiculo/VeiculoFiltroDto';
import { SwalHandler } from 'src/app/utils/SwalHandler';


@Component({
  selector: 'app-botao-exportar',
  templateUrl: './botao-exportar.component.html',
  styleUrls: ['./botao-exportar.component.css']
})
export class BotaoExportarComponent implements OnInit {

  constructor(private _veiculoService: VeiculoService) { }

  ngOnInit(): void {
  }
  public exportarExcel(): void {
    this._veiculoService.listarVeiculos(1, 50).subscribe({
      next: (veiculos: VeiculoFiltroDto) => {
        const veiculosTratados = veiculos.itens.map(({ fotos, ...v }) => ({
          ...v,
          opcionais: (v.opcionais ?? [])
            .map(o => o.descricao)
            .join(', ') || '—',
          pacotesPortal: (v.pacotesPortal ?? [])
            .map(p => p.nomePacote + '('+ p.pacoteId + ')')
            .join(', ') || '—',
       
        }));

        if (!veiculosTratados.length) {
          SwalHandler.showAtencao("Atenção", "Não há veículos para exportar");
          return; 
        }
        const worksheet = XLSX.utils.json_to_sheet(veiculosTratados);
        const colWidths = Object.keys(veiculosTratados[0]).map(k => ({
          wch: Math.max(k.length + 2, 15)
        }));
        worksheet['!cols'] = colWidths;

        const workbook = XLSX.utils.book_new();
        XLSX.utils.book_append_sheet(workbook, worksheet, 'Veiculos');
        const excelBuffer = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
        const blob = new Blob([excelBuffer], { type: 'application/octet-stream' });
        FileSaver.saveAs(blob, 'veiculos.xlsx');
      },
      error: (err) => {
        SwalHandler.showFalha("Falha", "Ocorreu um pequeno problema ao exportar estoque");
      }
    });
  }
}
