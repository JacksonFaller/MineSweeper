import { Component, OnInit, Input, Output, EventEmitter, OnDestroy } from '@angular/core';
import { Cell } from '../models/cell';
import { FieldClick } from 'src/models/field-click';
import { FieldService } from 'src/services/field.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-field',
  templateUrl: './field.component.html',
  styleUrls: ['./field.component.scss'],
})
export class FieldComponent implements OnInit, OnDestroy {
  @Input() width: number;
  @Input() height: number;
  @Input() cellSize: number;
  @Input() gameKey: string;

  @Output()
  fieldClick = new EventEmitter<FieldClick>();

  cells: Cell[][];

  subscriptions: Array<Subscription> = [];

  constructor(field: FieldService) {
    this.subscriptions.push(
      field.updateCellsSubscribe({
        next: (cells) => this.openCells(cells),
      }),
    );

    this.subscriptions.push(
      field.refreshFieldSubscribe({
        next: () => this.initField(),
      }),
    );
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((sub) => sub.unsubscribe());
  }

  ngOnInit(): void {
    this.initField();
  }

  initField(): void {
    this.cells = [];
    for (let y = 0; y < this.height; y++) {
      let temp = [];
      for (let x = 0; x < this.width; x++) {
        temp.push(new Cell(x, y));
      }
      this.cells.push(temp);
    }
  }

  cellClick(cell: Cell, event: MouseEvent): void {
    this.fieldClick.emit(new FieldClick(event.button, cell));
  }

  openCells(cells: Cell[]) {
    cells.forEach((val) => {
      Object.assign(this.cells[val.y][val.x], {
        number: val.number,
        mine: val.mine,
        open: true,
      });
    });
  }
}
