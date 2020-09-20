import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class TransportService {
  constructor(private http: HttpClient) {}

  public newGame(): void {}

  public openCell(x: number, y: number): void {}
  public flagCell(x: number, y: number): void {}
  public unflagCell(x: number, y: number): void {}
  public openNeighbors(x: number, y: number): void {}
}
