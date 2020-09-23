import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { StartGameRequest } from 'src/DTO/start-game';
import { MakeMoveRequest, Move } from 'src/DTO/make-move';
import { MoveType } from 'src/Enums/move-type';
import { AppConfig } from './app-config.service';
import { Observable } from 'rxjs';
import { IGameServiceInterface } from 'src/interfaces/igame-service';

@Injectable({
  providedIn: 'root',
})
export class TransportService implements IGameServiceInterface {
  constructor(private http: HttpClient) {}

  public newGame(width: number, height: number, minesCount: number): Observable<Object> {
    const request = new StartGameRequest(width, height, minesCount);
    return this.post('Game/Start/', request);
  }

  public makeMove(x: number, y: number, moveType: MoveType, gameKey: string): Observable<Object> {
    const request = new MakeMoveRequest(gameKey, new Move(x, y, moveType));
    return this.post('Game/MakeMove/', request);
  }

  public post(url: string, request: any): Observable<Object> {
    return this.http.post(`${AppConfig.settings.apiBaseUrl}/${url}`, request);
  }
}
