import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { IssueForCreation } from '../shared/issueForCreation.model';
import { ErrorHandlerService } from './../../shared/services/error-handler.service';
import { RepositoryService } from './../../shared/services/repository.service';
import { Router } from '@angular/router';
// import { DatePipe } from '@angular/common';
// import * as $ from 'jquery';

@Component({
  selector: 'app-issue-create',
  templateUrl: './issue-create.component.html',
  styleUrls: ['./issue-create.component.css']
})
export class IssueCreateComponent implements OnInit {
  public errorMessage = '';

  public issueForm: FormGroup;

  // tslint:disable-next-line:max-line-length
  constructor(private repository: RepositoryService, private errorHandler: ErrorHandlerService, private router: Router) { }

  ngOnInit() {
    this.issueForm = new FormGroup({
      title: new FormControl('', [Validators.required, Validators.maxLength(60)]),
      description: new FormControl('', [Validators.required, Validators.maxLength(100)]),
      taskType: new FormControl('', [Validators.required, Validators.maxLength(60)]),
      priority: new FormControl('', [Validators.required, Validators.maxLength(60)]),
      statusType: new FormControl('', [Validators.required, Validators.maxLength(60)]),
      createdAt: new FormControl('', [Validators.required, Validators.maxLength(60)]),
      updatedAt: new FormControl('', [Validators.required, Validators.maxLength(60)]),
    });
  }

  public validateControl = (controlName: string) => {
    if (this.issueForm.controls[controlName].invalid && this.issueForm.controls[controlName].touched) {
      return true;
    }

    return false;
  }

  public hasError = (controlName: string, errorName: string) => {
    if (this.issueForm.controls[controlName].hasError(errorName)) {
      return true;
    }

    return false;
  }

  public createIssue = (issueFormValue) => {
    if (this.issueForm.valid) {
      this.executeIssueCreation(issueFormValue);
    }
  }

  private executeIssueCreation = (issueFormValue) => {
    // const issue: IssueForCreation = {
    //   name: issueFormValue.name,
    //   fullAddress: issueFormValue.fullAddress
    // };
    //
    // const apiUrl = 'api/issue';
    // this.repository.create(apiUrl, issue).subscribe();
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

  public redirectToIssueList() {
    this.router.navigate(['/issue/list']);
  }

}
