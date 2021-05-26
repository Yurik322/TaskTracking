import {Component, Inject, OnInit} from '@angular/core';
import { ProjectService } from '../shared/project.service';
import { Router } from '@angular/router';
import { Project } from '../shared/project';
import { SharedService } from '../../shared/services/shared.service';

import {HttpClient, HttpHeaders} from '@angular/common/http';
import {EnvironmentUrlService} from '../../shared/services/environment-url.service';

@Component({
  selector: 'app-project-list',
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.css']
})
export class ProjectListComponent implements OnInit {
  errorMessage: string;
  projects: Project[];

  constructor(private projectService: ProjectService,
              private sharedService: SharedService,
              private router: Router) { }

  ngOnInit() {
    this.getAllProjects();
  }

  public getAllProjects = () => {
    const apiAddress = 'api/projects';
    this.projectService.getProjects(apiAddress)
      .subscribe(res => {
          this.projects = res as Project[];
        },
        (error) => {
          this.errorMessage = <any>error;
        });
  }

  updateProject(project: Project): void {
    this.sharedService.setCurrentProject(project);
  }

  deleteProject(project: Project): void {
    if (confirm('Are you sure you want to delete this project?')) {


      // this.projectService.deleteProject(project.id)

      const deleteUrl: string = `api/projects/${project.id}`;
      this.projectService.deleteProject(deleteUrl)

        .subscribe(data => {
        let index = this.projects.indexOf(project);
        if (index > -1) {
          this.projects.splice(index, 1);
        }

        // this.sharedService.update(this.projects);
      }, error => this.errorMessage = <any>error);
    }
  }
}
