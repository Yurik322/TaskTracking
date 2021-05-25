import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ProjectForCreation } from '../shared/projectForCreation.model';
import { ErrorHandlerService } from './../../shared/services/error-handler.service';
import { RepositoryService } from './../../shared/services/repository.service';
import { Router } from '@angular/router';
import {Project} from '../shared/project.module';
import {getLocaleDateTimeFormat} from '@angular/common';

@Component({
  selector: 'app-project-create',
  templateUrl: './project-create.component.html',
  styleUrls: ['./project-create.component.css']
})
export class ProjectCreateComponent implements OnInit {

  public project: Project = {} as Project;
  public errorMessage = '';
  // public project: Project;
  public projectForm: FormGroup;

  // tslint:disable-next-line:max-line-length
  constructor(private repository: RepositoryService, private errorHandler: ErrorHandlerService, private router: Router) { }

  ngOnInit() {
    this.projectForm = new FormGroup({
      name: new FormControl('', [Validators.required, Validators.maxLength(60)]),
      url: new FormControl('', [Validators.required, Validators.maxLength(60)]),
      description: new FormControl('', [Validators.required, Validators.maxLength(60)]),
    });
  }

  public validateControl = (controlName: string) => {
    if (this.projectForm.controls[controlName].invalid && this.projectForm.controls[controlName].touched) {
      return true;
    }

    return false;
  }

  public hasError = (controlName: string, errorName: string) => {
    if (this.projectForm.controls[controlName].hasError(errorName)) {
      return true;
    }

    return false;
  }

  public createProject = (projectFormValue) => {
    if (this.projectForm.valid) {
      this.executeProjectCreation(projectFormValue);
    }
  }

  private executeProjectCreation = (projectFormValue) => {
    const project: ProjectForCreation = {
      name: projectFormValue.name,
      url: projectFormValue.url,
      description: projectFormValue.description,
      createdAt: projectFormValue.dateTime
    };

    const apiUrl = 'api/project';
    this.repository.create(apiUrl, project).subscribe();
    // TODO
    // .subscribe(
    //   res => {
    //     $('#successModal').modal();
    //   },
    //   (error => {
    //     this.errorHandler.handleError(error);
    //     this.errorMessage = this.errorHandler.errorMessage;
    //   })
    // );
  }

  public redirectToProjectList() {
    this.router.navigate(['/project/list']);
  }

}
