import { Injectable } from '@angular/core';
import { PartialObserver, Subject, Subscription } from 'rxjs';
import { Cell } from 'src/models/cell';

@Injectable({
  providedIn: 'root',
})
export class FieldService {
  private refreshField$ = new Subject<any>();
  private updateCells$ = new Subject<Cell[]>();

  refreshFieldSubscribe(observer: PartialObserver<any>): Subscription {
    return this.refreshField$.subscribe(observer);
  }

  updateCellsSubscribe(observer: PartialObserver<Cell[]>): Subscription {
    return this.updateCells$.subscribe(observer);
  }

  constructor() {}

  refreshFieldSend(): void {
    this.refreshField$.next();
  }

  updateCells(cells: Cell[]): void {
    this.updateCells$.next(cells);
  }
}
