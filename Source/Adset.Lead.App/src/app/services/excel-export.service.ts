import { Injectable } from '@angular/core';
import * as XLSX from 'xlsx';
import { saveAs } from 'file-saver';
import { Vehicle } from '../models/vehicle.model';

@Injectable({
  providedIn: 'root'
})
export class ExcelExportService {

  /**
   * Exporta lista de veículos para arquivo Excel
   */
  exportVehiclesToExcel(vehicles: Vehicle[], fileName: string = 'estoque-veiculos'): void {
    try {
      // Preparar dados para exportação
      const exportData = vehicles.map(vehicle => ({
        'Placa': vehicle.plate,
        'Marca': vehicle.brand,
        'Modelo': vehicle.model,
        'Ano': vehicle.year,
        'Cor': vehicle.color,
        'KM': vehicle.km || 0,
        'Preço': vehicle.price,
        'Portal': vehicle.portal || 'N/A',
        'Pacote': vehicle.package || 'N/A',
        'Qtd Fotos': vehicle.photosCount || 0,
        'Com Fotos': vehicle.hasPhotos ? 'Sim' : 'Não',
        'Qtd Opcionais': vehicle.featuresCount || 0,
        'Opcionais': this.getFeaturesText(vehicle.features || []),
        'Data Exportação': new Date().toLocaleDateString('pt-BR')
      }));

      // Criar workbook
      const worksheet = XLSX.utils.json_to_sheet(exportData);
      const workbook = XLSX.utils.book_new();
      
      // Adicionar worksheet ao workbook
      XLSX.utils.book_append_sheet(workbook, worksheet, 'Estoque');

      // Configurar larguras das colunas
      const columnWidths = [
        { wch: 10 }, // Placa
        { wch: 15 }, // Marca
        { wch: 20 }, // Modelo
        { wch: 8 },  // Ano
        { wch: 12 }, // Cor
        { wch: 10 }, // KM
        { wch: 15 }, // Preço
        { wch: 12 }, // Portal
        { wch: 12 }, // Pacote
        { wch: 10 }, // Qtd Fotos
        { wch: 10 }, // Com Fotos
        { wch: 12 }, // Qtd Opcionais
        { wch: 30 }, // Opcionais
        { wch: 15 }  // Data Exportação
      ];
      worksheet['!cols'] = columnWidths;

      // Gerar arquivo Excel
      const excelBuffer = XLSX.write(workbook, { 
        bookType: 'xlsx', 
        type: 'array' 
      });

      // Fazer download do arquivo
      const data = new Blob([excelBuffer], {
        type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
      });
      
      const finalFileName = `${fileName}_${new Date().toISOString().slice(0, 10)}.xlsx`;
      saveAs(data, finalFileName);

    } catch (error) {
      console.error('Erro ao exportar Excel:', error);
      throw new Error('Falha na exportação do arquivo Excel');
    }
  }

  /**
   * Converte array de features em texto legível
   */
  private getFeaturesText(features: number[]): string {
    if (!features || features.length === 0) {
      return 'Nenhum';
    }

    const featureNames: { [key: number]: string } = {
      1: 'Ar Condicionado',
      2: 'Alarme', 
      3: 'Airbag',
      4: 'Freio ABS',
      5: 'MP3 Player'
    };

    return features
      .map(featureId => featureNames[featureId] || `Feature ${featureId}`)
      .join(', ');
  }
}
