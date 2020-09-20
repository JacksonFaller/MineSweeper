import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { TimeService } from '../services/time.service';

@Component({
  selector: 'app-timer',
  templateUrl: './timer.component.html',
  styleUrls: ['./timer.component.scss'],
})
export class TimerComponent implements OnInit, OnDestroy {
  constructor(private timeService: TimeService) {
    this.subscription = timeService.Time.subscribe((newTime: number) => {
      this.time = newTime;
    });
  }

  time = 0;
  subscription: Subscription;

  ngOnInit(): void {}

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
