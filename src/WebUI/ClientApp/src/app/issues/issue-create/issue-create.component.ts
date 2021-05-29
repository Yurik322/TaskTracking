import { Component, OnInit } from '@angular/core';
import { IssueService } from '../shared/issue.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Issue } from '../shared/issue';
import { Project } from '../../projects/shared/project';
import { NgForm } from '@angular/forms';
import { ProjectService } from '../../projects/shared/project.service';

@Component({
  selector: 'app-issue-create',
  templateUrl: './issue-create.component.html',
  styleUrls: ['./issue-create.component.css']
})
export class IssueCreateComponent implements OnInit {
  issue: Issue = {} as Issue;
  projects: Project[];
  errorMessage: string;
  fileToUpload: any;

  constructor(private issueService: IssueService,
              private projectService: ProjectService,
              private activatedRoute: ActivatedRoute,
              private router: Router) { }

  ngOnInit() {
    // Default - empty issue
    this.issue = {
      issueId: -1,
      title: '',
      description: '',
      taskType: 0,
      priority: 1,
      statusType: 0,
      projectId: 0
    };

    this.getAllProjects();
  }

  public getAllProjects = () => {
    const apiAddress = 'api/projects';
    this.projectService.getProjects(apiAddress)
      .subscribe(result => {
          this.projects = result as Project[];
        },
        (error) => {
          this.errorMessage = <any>error;
        });
  }



  onSubmit(form: NgForm) {
    // Any file to upload?
    const apiUrl = 'api/issues';

    this.issueService.createIssue(apiUrl, this.issue)

      .subscribe(result => {
          console.log('result =', this.issue);
        if (this.issue.issueId > -1) {
          // TODO
          this.uploadFile(this.issue.issueId);
        } else {
          this.errorMessage = 'Invalid Request. Check your values!';
        }
      },
      error => {
        this.errorMessage = error;
        console.log(error);
      }
    );
  }

  uploadFile(issueId: any) {
    if (this.fileToUpload != null) {
      // 1
      this.uploadMiddleFile(issueId, this.fileToUpload);
      // 2
      // uploadFile(issueId: any, file: any): Observable<any> {
      //   let input = new FormData();
      //   input.append("file", file);
      //
      //   return this.http.put(this.baseUrl + 'api/attachments/Upload/' + issueId, input);
      // }
    } else {
      this.router.navigate(['/issues/list']);
    }
  }

  public uploadMiddleFile(issueId: any, file: any) {
    const input = new FormData();
    input.append('file', file);

    // TODO concat INTPUT
    const apiUrl = `api/attachments/Upload/${this.issue.issueId}`;

    this.issueService.uploadFileFromService(apiUrl, input)
      .subscribe(
        data => {
          console.log('File uploaded!');
        },
        error => {
          console.error('File not uploaded!');
        },
        () => {
          this.router.navigate(['/issues/list']);
        }
      );
  }



  fileChange(event) {
    const fileList: FileList = event.target.files;
    if (fileList.length > 0) {
      this.fileToUpload = fileList[0];
    }
  }

}
