import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Project } from '../shared/project.module';
import { RepositoryService } from '../../shared/services/repository.service';
import { ErrorHandlerService } from '../../shared/services/error-handler.service';

@Component({
  selector: 'app-project-list',
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.css']
})
export class ProjectListComponent implements OnInit {

  errorMessage: string;
  projects: Project[];

  constructor(private repository: RepositoryService, private errorHandler: ErrorHandlerService, private router: Router) { }

  ngOnInit(): void {
    this.getAllProjects();
  }

  public getAllProjects = () => {
    const apiAddress = 'api/project';
    this.repository.getData(apiAddress)
      .subscribe(res => {
          this.projects = res as Project[];
        },
        (error) => {
          this.errorHandler.handleError(error);
        });
  }

  public getProjectDetails = (id) => {
    const detailsUrl = `/project/details/${id}`;
    this.router.navigate([detailsUrl]);
  }

  public redirectToUpdatePage = (id) => {
    const updateUrl = `/project/update/${id}`;
    this.router.navigate([updateUrl]);
  }

  public redirectToDeletePage = (id) => {
    const deleteUrl = `/project/delete/${id}`;
    this.router.navigate([deleteUrl]);
  }

}
