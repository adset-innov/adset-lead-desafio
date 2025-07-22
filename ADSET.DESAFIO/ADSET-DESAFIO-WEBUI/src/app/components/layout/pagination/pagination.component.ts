import {
  Component,
  Input,
  Output,
  EventEmitter,
  OnChanges,
  SimpleChanges
} from '@angular/core';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.scss']
})
export class PaginationComponent implements OnChanges {
  @Input() totalPages = 1;
  @Input() currentPage = 1;

  @Output() pageChange = new EventEmitter<number>();

  pages: number[] = [];

  ngOnChanges(changes: SimpleChanges) {
    this.buildPages();
  }

  private buildPages() {
    this.pages = Array.from({ length: this.totalPages }, (_, i) => i + 1);
  }

  goTo(page: number) {
    if (page < 1 || page > this.totalPages || page === this.currentPage) return;
    this.pageChange.emit(page);
  }

  first() { this.goTo(1); }
  prev() { this.goTo(this.currentPage - 1); }
  next() { this.goTo(this.currentPage + 1); }
  last() { this.goTo(this.totalPages); }
}
