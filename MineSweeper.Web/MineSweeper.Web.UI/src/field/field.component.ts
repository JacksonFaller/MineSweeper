import { Component, OnInit, Input } from '@angular/core';
import { Cell } from '../models/cell';
import { MouseButton } from 'src/models/mouse-button';
import { TransportService } from 'src/services/transport.service';

@Component({
  selector: 'app-field',
  templateUrl: './field.component.html',
  styleUrls: ['./field.component.scss'],
})
export class FieldComponent implements OnInit {
  @Input() width: number;
  @Input() height: number;
  @Input() cellSize: number;

  cells: Cell[][] = [];

  constructor(private transport: TransportService) {}

  ngOnInit(): void {
    for (let y = 0; y < this.height; y++) {
      let temp = [];
      for (let x = 0; x < this.width; x++) {
        const num = Math.floor(Math.random() * 5 + 1);
        temp.push(new Cell(x, y, num));
      }
      this.cells.push(temp);
    }
  }

  cellClick(cell: Cell, event: MouseEvent): void {
    cell.Open = true;
    switch (event.button as MouseButton) {
      case MouseButton.Left: {
        this.transport.openCell(cell.X, cell.Y);
        cell.Open = true;
        break;
      }
      case MouseButton.Middle: {
        this.transport.openNeighbors(cell.X, cell.Y);
        break;
      }
      case MouseButton.Right: {
        if (cell.Flag) {
          this.transport.flagCell(cell.X, cell.Y);
        } else {
          this.transport.unflagCell(cell.X, cell.Y);
        }
        cell.Flag = !cell.Flag;
        break;
      }
    }
  }
}
