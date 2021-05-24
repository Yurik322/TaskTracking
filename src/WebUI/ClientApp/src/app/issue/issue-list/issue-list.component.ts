import { ErrorHandlerService } from './../../shared/services/error-handler.service';
import { Component, OnInit } from '@angular/core';
import { RepositoryService } from './../../shared/services/repository.service';
import { Issue } from './../../_interfaces/issue.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-issue-list',
  templateUrl: './issue-list.component.html',
  styleUrls: ['./issue-list.component.css']
})
export class IssueListComponent implements OnInit {
  public issues: Issue[];
  public errorMessage = '';

  constructor(private repository: RepositoryService, private errorHandler: ErrorHandlerService, private router: Router) { }

  ngOnInit(): void {
    this.getAllIssues();
  }

  public getAllIssues = () => {
    const apiAddress = 'api/issue';
    this.repository.getData(apiAddress)
      .subscribe(res => {
          this.issues = res as Issue[];
        },
        (error) => {
          this.errorHandler.handleError(error);
        });
  }

  public getIssueDetails = (id) => {
    const detailsUrl = `/issue/details/${id}`;
    this.router.navigate([detailsUrl]);
  }

  public redirectToUpdatePage = (id) => {
    const updateUrl = `/issue/update/${id}`;
    this.router.navigate([updateUrl]);
  }

  // public redirectToDeletePage = (id) => {
  //   const deleteUrl = `/issue/delete/${id}`;
  //   this.router.navigate([deleteUrl]);
  // }
}
