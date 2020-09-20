export class Cell {
  public Open: boolean;
  public Flag: boolean;
  public Mine: boolean;

  constructor(public X: number, public Y: number, public Number?: number) {}
}
