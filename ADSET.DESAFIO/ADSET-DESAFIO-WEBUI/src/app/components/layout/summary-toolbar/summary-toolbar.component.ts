import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-summary-toolbar',
  templateUrl: './summary-toolbar.component.html',
  styleUrls: ['./summary-toolbar.component.scss']
})
export class SummaryToolbarComponent {
  @Input() total = 0;
  @Input() withPhotos = 0;
  @Input() withoutPhotos = 0;

  @Output() exportExcel = new EventEmitter<void>();
  @Output() exportCsv = new EventEmitter<void>();
  @Output() register = new EventEmitter<void>();
  @Output() save = new EventEmitter<void>();

  onExportExcel(): void { this.exportExcel.emit(); }
  onExportCsv(): void { this.exportCsv.emit(); }
  onRegister(): void { this.register.emit(); }
  onSave(): void { this.save.emit(); }
}