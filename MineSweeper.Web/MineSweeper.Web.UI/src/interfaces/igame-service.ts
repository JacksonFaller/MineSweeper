import { Observable } from 'rxjs';
import { MoveType } from 'src/Enums/move-type';

export interface IGameServiceInterface {
  newGame(width: number, height: number, minesCount: number): Observable<Object>;
  makeMove(x: number, y: number, moveType: MoveType, gameKey: string): Observable<Object>;
}
