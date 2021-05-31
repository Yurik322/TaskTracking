import { Component, OnInit } from '@angular/core';
import { Project } from '../shared/project';
import { Router, ActivatedRoute } from '@angular/router';
import { ProjectService } from '../shared/project.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-project-edit',
  templateUrl: './project-edit.component.html',
  styleUrls: ['./project-edit.component.css']
})
export class ProjectEditComponent implements OnInit {
  project: Project = {} as Project;
  projectId: number;
  errorMessage: string;

  constructor(private router: Router,
              private activatedRoute: ActivatedRoute,
              private projectService: ProjectService) { }

  ngOnInit() {
    this.projectId = +this.activatedRoute.snapshot.params['id'];

    if (this.projectId >= 0) {
        const companyByIdUrl = `api/projects/${this.projectId}`;
        this.projectService.getData(companyByIdUrl).subscribe(result => {
        this.project = <Project>result;
      }, error => this.errorMessage = <any>error);
    }
  }

  onSubmit(form: NgForm) {
    if (this.projectId >= 0) {
      const apiUrl = `api/projects/${this.project.projectId}`;
      this.projectService.updateProject(apiUrl, this.project)
        .subscribe(data => {
        this.router.navigate(['/projects/list']);
      }, error => this.errorMessage = <any>error);
    } else {
        const apiUrl = 'api/projects';
        this.projectService.createProject(apiUrl, this.project)
        .subscribe(data => {
        this.router.navigate(['/projects/list']);
      }, error => this.errorMessage = <any>error);
    }
  }
}
