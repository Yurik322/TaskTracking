import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../shared/employee.service';
import { ActivatedRoute, Params } from '@angular/router';
import { Employee } from '../shared/employee';
import { SharedService } from '../../shared/services/shared.service';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {
  employees: Employee[];
  errorMessage: string;

  constructor(private employeeService: EmployeeService,
              public sharedService: SharedService,
              private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.activatedRoute.params.subscribe((params: Params) => {
      const issueId = params['id'];
      if (issueId > -1) {
        // TODO
        const employeesByIssueUrl = `api/issues/${issueId}/employees`;
        this.employeeService.getData(employeesByIssueUrl)
          .subscribe(employees => this.employees = <Employee[]>employees, error => this.errorMessage = <any>error);
      } else {
        this.getAllEmployees();
      }
    });
  }

  public getAllEmployees = () => {
    const apiAddress = 'api/employees';
    this.employeeService.getEmployees(apiAddress)
      .subscribe(res => {
          this.employees = res as Employee[];
        },
        (error) => {
          this.errorMessage = <any>error;
        });
  }


  deleteEmployee(employee: Employee): void {
    if (confirm('Are you sure you want to delete this employee?')) {
      const deleteUrl = `api/employees/${employee.employeeId}`;
      this.employeeService.deleteEmployee(deleteUrl)
        .subscribe(result => {
          const index = this.employees.indexOf(employee);
          if (index > -1) {
            this.employees.splice(index, 1);
          }
        }, error => this.errorMessage = <any>error);
    }
  }
}
