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
  // getProjects(): Observable<Project[]> {
  //   return this.http.get<Project[]>(this.baseUrl + 'api/projects');
  // }

  // TODO
  getProject(id: number): Observable<Project> {
    return this.http.get<Project>(this.baseUrl + 'api/projects/' + id);
  }
  // public getCompanyDetails = (id) => {
  //   const detailsUrl = `/company/details/${id}`;
  //   this.router.navigate([detailsUrl]);
  // }

  // public createProject = (route: string, body) => {
  //   return this.http.post(this.createCompleteRoute(route, this.envUrl.urlAddress), body, this.generateHeaders());
  // }
  createProject(project: Project): Observable<any> {
    return this.http.post(this.baseUrl + 'api/projects', project);
  }

  // public updateProject = (route: string, body) => {
  //   return this.http.put(this.createCompleteRoute(route, this.envUrl.urlAddress), body, this.generateHeaders());
  // }
  updateProject(project: Project): Observable<any> {
    return this.http.put(this.baseUrl + 'api/projects/' + project.id, project);
  }

  // public deleteProject = (route: string) => {
  //   return this.http.delete(this.createCompleteRoute(route, this.envUrl.urlAddress));
  // }
  // deleteProject(id: number): Observable<any> {
  //   return this.http.delete(this.baseUrl + 'api/projects/' + id);
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
