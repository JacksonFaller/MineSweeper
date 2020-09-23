import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IAppConfig } from 'src/assets/config/app-config.model';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AppConfig {
  static settings: IAppConfig;

  constructor(private http: HttpClient) {}
  load() {
    const jsonFile = `assets/config/config.${environment.name}.json`;
    this.http
      .get(jsonFile)
      .toPromise()
      .then((response: IAppConfig) => {
        AppConfig.settings = <IAppConfig>response;
      });
  }
}
