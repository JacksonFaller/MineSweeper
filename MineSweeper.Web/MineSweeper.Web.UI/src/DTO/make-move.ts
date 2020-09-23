import { Cell } from 'src/models/cell';
import { MoveResultType } from '../Enums/move-result-type';
import { MoveType } from '../Enums/move-type';

export class MakeMoveRequest {
  constructor(public gameKey: string, public playerMove: Move) {}
}

export class MakeMoveResponse {
  public moveResult: MoveResult;
}

export class Move {
  constructor(public x: number, public y: number, public type: MoveType) {}
}

export class MoveResult {
  public resultType: MoveResultType;
  public openedCells: Array<Cell>;
}
