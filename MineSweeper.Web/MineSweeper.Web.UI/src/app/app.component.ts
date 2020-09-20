import { Component } from '@angular/core';
import { TimeService } from '../services/time.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  constructor(private timeService: TimeService) {}

  startClick(): void {
    this.timeService.Reset();
    this.timeService.Start();
  }
}
