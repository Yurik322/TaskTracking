import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../shared/employee.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Employee } from '../shared/employee';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-employee-create',
  templateUrl: './employee-create.component.html',
  styleUrls: ['./employee-create.component.css']
})
export class EmployeeCreateComponent implements OnInit {
  employee: Employee = {} as Employee;
  errorMessage: Object;

  constructor(private employeeService: EmployeeService,
              private activatedRoute: ActivatedRoute,
              private router: Router) { }

  ngOnInit() {
    // Default - empty employee
    this.employee = {
      employeeId: -1,
      age: 18,
      position: '',
      // userId: '0'
    };
  }

  onSubmit(form: NgForm) {
    const apiUrl = 'api/employees';
    this.employeeService.createEmployee(apiUrl, this.employee)
      .subscribe(
        error => {
          this.errorMessage = error;
          console.log(error);
        }
      );
  }
}
