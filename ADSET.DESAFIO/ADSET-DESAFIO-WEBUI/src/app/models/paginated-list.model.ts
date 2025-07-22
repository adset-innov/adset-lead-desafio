export interface PaginatedList<T> {
  pageIndex: number;
  totalPages: number;
  totalItems: number;
  withPhotos: number;
  withoutPhotos: number;
  items: T[];
}