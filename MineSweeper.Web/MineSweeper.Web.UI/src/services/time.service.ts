import { Injectable, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class TimeService implements OnDestroy {
  public Time: Subject<number> = new Subject<number>();

  private timeout: NodeJS.Timeout;
  private time = 0;

  constructor() {}

  ngOnDestroy(): void {
    this.Stop();
  }

  public Reset(): void {
    this.time = 0;
    this.Time.next(0);
  }

  public Stop(): void {
    clearInterval(this.timeout);
  }

  public Start(): void {
    if (this.timeout) {
      return;
    }
    this.timeout = setInterval(() => {
      this.time++;
      this.Time.next(this.time);
    }, 1000);
  }
}
