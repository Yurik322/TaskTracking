import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, NgForm, Validators} from '@angular/forms';
// import { IssueForCreation } from '../shared/issueForCreation.model';
import { ErrorHandlerService } from '../../shared/services/error-handler.service';
import { RepositoryService } from '../../shared/services/repository.service';
import { Router } from '@angular/router';
import { Issue } from '../shared/issue.model';
import { Project } from '../../project/shared/project.model';

@Component({
  selector: 'app-issue-create',
  templateUrl: './issue-create.component.html',
  styleUrls: ['./issue-create.component.css']
})
export class IssueCreateComponent implements OnInit {
  public errorMessage = '';
  // public issueForm: FormGroup;
  issue: Issue = {} as Issue;
  projects: Project[];
  fileToUpload: any;

  // tslint:disable-next-line:max-line-length
  constructor(private repository: RepositoryService, private errorHandler: ErrorHandlerService, private router: Router) { }

  ngOnInit() {
    // Default - empty issue
    this.issue = {
      // issueId: -1,
      title: '',
      description: '',
      issueType: 0,
      priority: 1,
      statusType: 0
    };

    this.getAllProjects();
  }

  // TODO
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

  // public hasError = (controlName: string, errorName: string) => {
  //   if (this.issueForm.controls[controlName].hasError(errorName)) {
  //     return true;
  //   }
  //
  //   return false;
  // }

  public redirectToIssueList() {
    this.router.navigate(['/issue/list']);
  }

  // TODO
  uploadFile(issueId: any) {
    if (this.fileToUpload != null) {
      this.repository.uploadFile(issueId, this.fileToUpload).subscribe(
        data => {
          console.log('File uploaded!');
        },
        error => {
          console.error('File not uploaded!');
        },
        () => {
          this.router.navigate(['/issues']);
        }
      );
    }
    else {
      this.router.navigate(['/issues']);
    }
  }

  fileChange(event) {
    let fileList: FileList = event.target.files;
    if (fileList.length > 0) {
      this.fileToUpload = fileList[0];
    }
  }

  onSubmit(form: NgForm) {
    const apiUrl = 'api/issue';
    // Any file to upload?

    this.repository.create(apiUrl, this.issue).subscribe(result => {
    // this.repository.createIssue(this.issue).subscribe(result => {
        if (result > -1) {
          this.uploadFile(result);
        }
        // if (result.issueId > -1) {
        //   this.uploadFile(result.issueId);
        // }
        else
          this.errorMessage = "Invalid Request. Check your values!";
      },
      error => {
        this.errorMessage = error;
        console.log(error);
      }
    );
  }
}
