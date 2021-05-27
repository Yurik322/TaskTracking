import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Issue } from './issue';
import { EnvironmentUrlService } from '../../shared/services/environment-url.service';

@Injectable({
  providedIn: 'root'
})
export class IssueService {

  constructor(private http: HttpClient, private envUrl: EnvironmentUrlService, @Inject('BASE_URL') private baseUrl: string) { }

  public getIssues = (route: string) => {
    return this.http.get(this.createCompleteRoute(route, this.envUrl.urlAddress));
  }

  public getData = (route: string) => {
    return this.http.get(this.createCompleteRoute(route, this.envUrl.urlAddress));
  }

  public createIssue = (route: string, body) => {
    return this.http.post(this.createCompleteRoute(route, this.envUrl.urlAddress), body, this.generateHeaders());
  }

  public updateIssue = (route: string, body) => {
    return this.http.put(this.createCompleteRoute(route, this.envUrl.urlAddress), body, this.generateHeaders());
  }

  public deleteIssue = (route: string) => {
    return this.http.delete(this.createCompleteRoute(route, this.envUrl.urlAddress));
  }

  // TODO
  // uploadFile(issueId: any, fileToUpload: any) {
  //
  // }
  // uploadFile(issueId: any, file: any): Observable<any> {
  //   let input = new FormData();
  //   input.append("file", file);
  //
  //   return this.http.put(this.baseUrl + 'api/attachments/Upload/' + issueId, input);
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
