import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Project } from './project';
import { Observable } from 'rxjs';
import { EnvironmentUrlService } from '../../shared/services/environment-url.service';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  constructor(private http: HttpClient, private envUrl: EnvironmentUrlService, @Inject('BASE_URL') private baseUrl: string) { }

  public getProjects = (route: string) => {
    return this.http.get(this.createCompleteRoute(route, this.envUrl.urlAddress));
  }

  public getData = (route: string) => {
    return this.http.get(this.createCompleteRoute(route, this.envUrl.urlAddress));
  }

  public createProject = (route: string, body) => {
    return this.http.post(this.createCompleteRoute(route, this.envUrl.urlAddress), body, this.generateHeaders());
  }

  public updateProject = (route: string, body) => {
    return this.http.put(this.createCompleteRoute(route, this.envUrl.urlAddress), body, this.generateHeaders());
  }

  public deleteProject = (route: string) => {
    return this.http.delete(this.createCompleteRoute(route, this.envUrl.urlAddress));
  }

  private createCompleteRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  }

  private generateHeaders = () => {
    return {
      headers: new HttpHeaders({'Content-Type': 'application/json'})
    };
  }
}
