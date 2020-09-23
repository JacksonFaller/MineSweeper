import { GameBase } from './game-base';

export class StartGameRequest {
  constructor(public width: number, public height: number, public minesCount: number) {}
}

export class StartGameResponse extends GameBase {}
