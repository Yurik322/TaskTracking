import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { EnvironmentUrlService } from '../../shared/services/environment-url.service';

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  constructor(private http: HttpClient, private envUrl: EnvironmentUrlService, @Inject('BASE_URL') private baseUrl: string) { }

  public getReports = (route: string) => {
    return this.http.get(this.createCompleteRoute(route, this.envUrl.urlAddress));
  }

  public getData = (route: string) => {
    return this.http.get(this.createCompleteRoute(route, this.envUrl.urlAddress));
  }

  public createReport = (route: string, body) => {
    return this.http.post(this.createCompleteRoute(route, this.envUrl.urlAddress), body, this.generateHeaders());
  }

  public updateReport = (route: string, body) => {
    return this.http.put(this.createCompleteRoute(route, this.envUrl.urlAddress), body, this.generateHeaders());
  }

  public deleteReport = (route: string) => {
    return this.http.delete(this.createCompleteRoute(route, this.envUrl.urlAddress));
  }

  // TODO
  // uploadFile(issueId: any, file: any): Observable<any> {
  //   let input = new FormData();
  //   input.append("file", file);
  //
  //   return this.http.put(this.baseUrl + 'api/attachments/Upload/' + issueId, input);
  // }

  public uploadFileFromService = (route: string, body) => {
    return this.http.put(this.createCompleteRoute(route, this.envUrl.urlAddress, ), body, this.generateHeaders());
  }
  // public uploadFileFromService = (route: string, body) => {
  //   return this.http.put(this.createCompleteRoute(route, this.envUrl.urlAddress), body, this.generateHeaders());
  // }

  private createCompleteRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  }

  private generateHeaders = () => {
    return {
      headers: new HttpHeaders({'Content-Type': 'application/json'})
    };
  }
}
