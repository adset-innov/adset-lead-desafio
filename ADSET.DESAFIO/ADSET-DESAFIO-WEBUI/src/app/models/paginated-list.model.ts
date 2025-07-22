export interface PaginatedList<T> {
  pageIndex: number;
  totalPages: number;
  items: T[];
}