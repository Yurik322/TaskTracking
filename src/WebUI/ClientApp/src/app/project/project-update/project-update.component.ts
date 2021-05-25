import { Component, OnInit } from '@angular/core';
import { Project } from '../shared/project.module';
import { Router, ActivatedRoute } from '@angular/router';
import { FormControl, FormGroup, NgForm, Validators} from '@angular/forms';
import { RepositoryService } from '../../shared/services/repository.service';
import { ErrorHandlerService } from '../../shared/services/error-handler.service';

@Component({
  selector: 'app-project-update',
  templateUrl: './project-update.component.html',
  styleUrls: ['./project-update.component.css']
})
export class ProjectUpdateComponent implements OnInit {

  public project: Project = {} as Project;
  public projectId: number;
  public errorMessage = '';
  // public project: Project;
  public projectForm: FormGroup;

  constructor(private repository: RepositoryService, private errorHandler: ErrorHandlerService, private router: Router,
              private activeRoute: ActivatedRoute) { }

  ngOnInit() {
    this.projectForm = new FormGroup({
      name: new FormControl('', [Validators.required, Validators.maxLength(60)]),
    });

    this.getProjectById();
  }

  private getProjectById = () => {
    const projectId: string = this.activeRoute.snapshot.params['id'];

    const projectByIdUrl = `api/project/${projectId}`;

    this.repository.getData(projectByIdUrl).subscribe();
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

  public redirectToProjectList = () => {
    this.router.navigate(['/project/list']);
  }

  public updateProject = (projectFormValue) => {
    if (this.projectForm.valid) {
      this.executeProjectUpdate(projectFormValue);
    }
  }

  private executeProjectUpdate = (projectFormValue) => {

    console.log(this.project);
    //TODO project undefined!!!
    this.project.name = projectFormValue.name;

    const apiUrl = `api/project/${this.project.projectId}`;
    this.repository.update(apiUrl, this.project).subscribe();
    // .subscribe(res => {
    //     $('#successModal').modal();
    //   },
    //   (error => {
    //     this.errorHandler.handleError(error);
    //     this.errorMessage = this.errorHandler.errorMessage;
    //   })
    // )
  }
}
