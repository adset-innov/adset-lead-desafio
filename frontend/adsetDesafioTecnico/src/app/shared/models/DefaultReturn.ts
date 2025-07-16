export class DefaultReturn<T = any> {
  constructor(
    public statusCode: number,
    public message: string[] = [],
    public data?: T
  ) {}
}