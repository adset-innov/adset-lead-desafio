import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})


export class AppComponent {
  title = 'ADSET-DESAFIO-WEBUI';
  currentSortBy = "";
  currentSortDir: 'asc' | 'desc' = 'asc';
  currentPageSize = 10;
  currentPage = 1;
  totalPagesFromApi = 10;

  handleExportExcel() { }
  handleExportCsv() { }
  handleRegister() { }
  handleSave() { }
  onSortByChange() { }
  onSortDirChange() { }
  onPageSizeChange() { }
}
