import { BrowserModule } from '@angular/platform-browser';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent, GAME_SERVICE } from './app.component';
import { FieldComponent } from '../field/field.component';
import { TimerComponent } from '../timer/timer.component';
import { AppConfig } from 'src/services/app-config.service';
import { TransportService } from 'src/services/transport.service';

export function loadAppConfig(appConfig: AppConfig) {
  appConfig.load();
}

@NgModule({
  declarations: [AppComponent, FieldComponent, TimerComponent],
  imports: [BrowserModule, AppRoutingModule, HttpClientModule],
  providers: [
    AppConfig,
    {
      provide: APP_INITIALIZER,
      useFactory: (appConfigService: AppConfig) => () => appConfigService.load(),
      deps: [AppConfig],
      multi: true,
    },
    { provide: GAME_SERVICE, useClass: TransportService },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
