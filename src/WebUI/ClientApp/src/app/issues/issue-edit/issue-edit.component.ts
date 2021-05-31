import { Component, OnInit } from '@angular/core';
import { Issue } from '../shared/issue';
import { IssueService } from '../shared/issue.service';
import { ActivatedRoute, Router, Params } from '@angular/router';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-issue-edit',
  templateUrl: './issue-edit.component.html',
  styleUrls: ['./issue-edit.component.css']
})
export class IssueEditComponent implements OnInit {
  issue: Issue;
  issueId: number;
  errorMessage: string;

  constructor(private issueService: IssueService,
              private activatedRoute: ActivatedRoute,
              private router: Router) { }

  ngOnInit() {
    this.issueId = +this.activatedRoute.snapshot.params['id'];
    if (this.issueId >= 0) {
      const issueByIdUrl = `api/issues/${this.issueId}`;
      this.issueService.getData(issueByIdUrl).subscribe(result => {
        this.issue = <Issue>result;
      }, error => this.errorMessage = <any>error);
    }
  }

  onSubmit(form: NgForm) {
    const apiUrl = `api/issues/${this.issue.issueId}`;
    this.issueService.updateIssue(apiUrl, this.issue)
      .subscribe(result => {
      this.router.navigate(['/issues/list']);
    }, error => this.errorMessage = error);
  }

}
