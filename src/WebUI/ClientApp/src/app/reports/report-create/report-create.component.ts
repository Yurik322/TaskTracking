import { Component, OnInit } from '@angular/core';
import { ReportService } from '../shared/report.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Report } from '../shared/report';
import { NgForm } from '@angular/forms';
import { Issue } from '../../issues/shared/issue';
import { IssueService } from '../../issues/shared/issue.service';
import { Employee } from '../../employees/shared/employee';
import { EmployeeService } from '../../employees/shared/employee.service';

@Component({
  selector: 'app-report-create',
  templateUrl: './report-create.component.html',
  styleUrls: ['./report-create.component.css']
})
export class ReportCreateComponent implements OnInit {
  report: Report = {} as Report;
  issues: Issue[];
  employees: Employee[];
  errorMessage: Object;

  constructor(private reportService: ReportService,
              private issueService: IssueService,
              private employeeService: EmployeeService,
              private activatedRoute: ActivatedRoute,
              private router: Router) { }

  ngOnInit() {
    // Default - empty report
    this.report = {
      reportId: -1,
      reportDescription: '',
      assignmentDate: '',
      employeeId: 0,
      issueId: 0
    };
    this.getAllIssues();
    this.getAllEmployees();
  }

  public getAllIssues = () => {
    const apiAddress = 'api/issues';
    this.issueService.getIssues(apiAddress)
      .subscribe(result => {
          this.issues = result as Issue[];
        },
        (error) => {
          this.errorMessage = <any>error;
        });
  }

  public getAllEmployees = () => {
    const apiAddress = 'api/employees';
    this.employeeService.getEmployees(apiAddress)
      .subscribe(result => {
          this.employees = result as Employee[];
        },
        (error) => {
          this.errorMessage = <any>error;
        });
  }

  onSubmit(form: NgForm) {
    // Any file to upload?
    const apiUrl = 'api/reports';
    this.reportService.createReport(apiUrl, this.report)
      .subscribe(
        error => {
          this.errorMessage = error;
          console.log(error);
        }
      );
  }
}
