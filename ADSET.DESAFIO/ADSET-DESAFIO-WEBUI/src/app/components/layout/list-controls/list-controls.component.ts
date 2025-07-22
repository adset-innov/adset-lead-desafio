import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output
} from '@angular/core';

@Component({
  selector: 'app-list-controls',
  templateUrl: './list-controls.component.html',
  styleUrls: ['./list-controls.component.scss']
})
export class ListControlsComponent implements OnInit {
  @Input() sortFields: { value: string; label: string }[] = [
    { value: 'brand', label: 'Marca / Modelo' },
    { value: 'year', label: 'Ano' },
    { value: 'price', label: 'Preço' },
    { value: 'photos', label: 'Fotos' }
  ];

  @Input() pageSizes: number[] = [10, 25, 50, 100];

  @Input() sortBy = 'brand';
  @Input() sortDir: 'asc' | 'desc' = 'asc';
  @Input() pageSize = 100;

  @Output() sortByChange = new EventEmitter<string>();
  @Output() sortDirChange = new EventEmitter<'asc' | 'desc'>();
  @Output() pageSizeChange = new EventEmitter<number>();

  ngOnInit(): void { }

  onSortByChange(value: string) {
    this.sortBy = value;
    this.sortByChange.emit(this.sortBy);
  }

  toggleSortDir() {
    this.sortDir = this.sortDir === 'asc' ? 'desc' : 'asc';
    this.sortDirChange.emit(this.sortDir);
  }

  onPageSizeChange(value: number) {
    this.pageSize = value;
    this.pageSizeChange.emit(this.pageSize);
  }
}
