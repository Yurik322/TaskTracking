import { Component, OnInit } from '@angular/core';
import { Employee } from '../shared/employee';
import { EmployeeService } from '../shared/employee.service';
import { ActivatedRoute, Router, Params } from '@angular/router';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-employee-edit',
  templateUrl: './employee-edit.component.html',
  styleUrls: ['./employee-edit.component.css']
})
export class EmployeeEditComponent implements OnInit {
  employee: Employee;
  employeeId: number;
  errorMessage: string;

  constructor(private employeeService: EmployeeService,
              private activatedRoute: ActivatedRoute,
              private router: Router) { }

  ngOnInit() {
    this.employeeId = +this.activatedRoute.snapshot.params['id'];

    if (this.employeeId >= 0) {
      const employeeByIdUrl = `api/employees/${this.employeeId}`;
      this.employeeService.getData(employeeByIdUrl).subscribe(result => {
        this.employee = <Employee>result;
      }, error => this.errorMessage = <any>error);
    }
  }

  onSubmit(form: NgForm) {
    const apiUrl = `api/employees/${this.employee.employeeId}`;
    this.employeeService.updateEmployee(apiUrl, this.employee)
      .subscribe(result => {
        this.router.navigate(['/employees/list']);
      }, error => this.errorMessage = error);
  }

}
