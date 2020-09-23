export class Cell {
  public flag: boolean;

  constructor(
    public x: number,
    public y: number,
    public number?: number,
    public mine?: boolean,
    public open = false,
  ) {}
}
