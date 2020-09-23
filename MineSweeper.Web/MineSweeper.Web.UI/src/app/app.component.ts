import { Component, Inject, InjectionToken } from '@angular/core';
import { MakeMoveResponse } from 'src/DTO/make-move';
import { StartGameResponse } from 'src/DTO/start-game';
import { MoveResultType } from 'src/Enums/move-result-type';
import { MoveType } from 'src/Enums/move-type';
import { IGameServiceInterface } from 'src/interfaces/igame-service';
import { FieldClick } from 'src/models/field-click';
import { MouseButton } from 'src/models/mouse-button';
import { FieldService } from 'src/services/field.service';
import { TimeService } from '../services/time.service';

export let GAME_SERVICE = new InjectionToken<IGameServiceInterface>('game-service');

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  gameKey: string;
  width = 10;
  height = 10;
  cellSize = 20;
  minesCount = 10;
  active = true;
  unflaggedMinesCount = 0;

  constructor(
    private timeService: TimeService,
    private field: FieldService,
    @Inject(GAME_SERVICE) private gameService: IGameServiceInterface,
  ) {}

  startClick(): void {
    if (!this.active) {
      this.timeService.Reset();
      this.active = true;
      this.field.refreshFieldSend();
    }
  }

  startGame(callback?: Function) {
    this.gameService
      .newGame(this.width, this.height, this.minesCount)
      .subscribe((data: StartGameResponse) => {
        this.gameKey = data.gameKey;
        this.timeService.Start();
        this.unflaggedMinesCount = this.minesCount;
        if (callback != null) {
          callback();
        }
      });
  }

  fieldClick(click: FieldClick) {
    const cell = click.cell;
    if (!this.active || (cell.open && click.mouseButton != MouseButton.Middle)) {
      return;
    }

    switch (click.mouseButton) {
      case MouseButton.Left: {
        if (this.gameKey == null) {
          this.startGame(() => this.fieldClick(click));
          return;
        }

        this.gameService
          .makeMove(cell.x, cell.y, MoveType.Click, this.gameKey)
          .subscribe((response: MakeMoveResponse) => {
            this.processMakeMoveResponse(response);
          });
        break;
      }
      case MouseButton.Middle: {
        this.gameService
          .makeMove(cell.x, cell.y, MoveType.OpenNeighbors, this.gameKey)
          .subscribe((response: MakeMoveResponse) => {
            this.processMakeMoveResponse(response);
          });
        break;
      }
      case MouseButton.Right: {
        this.gameService
          .makeMove(cell.x, cell.y, cell.flag ? MoveType.Unflag : MoveType.Flag, this.gameKey)
          .subscribe((response: MakeMoveResponse) => {
            cell.flag = !cell.flag;
            if (cell.flag) {
              this.unflaggedMinesCount--;
            } else {
              this.unflaggedMinesCount++;
            }
          });
        break;
      }
    }
  }

  gameOver(): void {
    this.timeService.Stop();
    this.active = false;
    this.gameKey = null;
  }

  processMakeMoveResponse(response: MakeMoveResponse): void {
    this.field.updateCells(response.moveResult.openedCells);

    if (
      response.moveResult.resultType === MoveResultType.GameOver ||
      response.moveResult.resultType === MoveResultType.Finished
    ) {
      this.gameOver();
      alert('Game over!');
    }

    if (response.moveResult.resultType === MoveResultType.Victory) {
      this.gameOver();
      alert('Victory!');
    }
  }
}
