import { ErrorHandlerService } from '../../shared/services/error-handler.service';
import { Component, OnInit } from '@angular/core';
import { RepositoryService } from '../../shared/services/repository.service';
import { ActivatedRoute, Params } from '@angular/router';
import { Issue } from '../shared/issue.model';
import { Router } from '@angular/router';
import { SharedService } from '../../shared/services/shared.service';

@Component({
  selector: 'app-issue-list',
  templateUrl: './issue-list.component.html',
  styleUrls: ['./issue-list.component.css']
})
export class IssueListComponent implements OnInit {
  issues: Issue[];
  errorMessage: string;
  filter1 = -1;
  filter2 = -1;
  filter3 = -1;

  // tslint:disable-next-line:max-line-length
  constructor(private repository: RepositoryService, public sharedService: SharedService, private errorHandler: ErrorHandlerService, private router: Router) { }

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

  public redirectToDeletePage = (id) => {
    const deleteUrl = `/issue/delete/${id}`;
    this.router.navigate([deleteUrl]);
  }




  priorityToClass(priority: number) {
    switch (priority) {
      case 0:
        return 'fas fa-arrow-down priority-low';
      case 2:
        return 'fas fa-arrow-up priority-high';
      default:
        return 'fas fa-arrow-up priority-normal';
    }
  }

  priorityToText(priority: number) {
    switch (priority) {
      case 0:
        return 'Low';
      case 2:
        return 'High';
      default:
        return 'Normal';
    }
  }

  statusTypeToClass(statusType: number) {
    switch (statusType) {
      case 0:
        return 'badge badge-primary';
      case 1:
        return 'badge badge-warning';
      case 2:
        return 'badge badge-success';
      case 3:
        return 'badge badge-primary';
      case 4:
        return 'badge badge-danger';
      default:
        return 'badge badge-default';
    }
  }

  statusTypeToText(statusType: number) {
    switch (statusType) {
      case 0:
        return 'Open';
      case 1:
        return 'In Progress';
      case 2:
        return 'Resolved';
      case 3:
        return 'Reopened';
      case 4:
        return 'Closed';
      default:
        return 'Unknown';
    }
  }

  getIssueTypeText(val: number) {
    switch (val) {
      case 0:
        return 'Bug';
      case 1:
        return 'Task';
      case 2:
        return 'Change';
      case 3:
        return 'Enhancement';
      default:
        return 'Unknown';
    }
  }
}
