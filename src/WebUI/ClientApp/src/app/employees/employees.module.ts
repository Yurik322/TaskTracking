import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeeListComponent } from './employee-list/employee-list.component';
import { EmployeeEditComponent } from './employee-edit/employee-edit.component';
import { EmployeeCreateComponent } from './employee-create/employee-create.component';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { EmployeeService } from './shared/employee.service';

@NgModule({
  declarations: [
    EmployeeListComponent,
    EmployeeEditComponent,
    EmployeeCreateComponent
],
  imports: [
    CommonModule,
    // BrowserModule,
    FormsModule,
    RouterModule.forChild([
      {
        path: 'list',
        component: EmployeeListComponent
      },
      // TODO
      {
        path: ':id/issues',
        pathMatch: 'full',
        component: EmployeeListComponent,
      },
      {
        path: ':id/edit',
        component: EmployeeEditComponent,
      },
      {
        path: 'create',
        component: EmployeeCreateComponent
      }
    ])
  ],
  providers: [
    EmployeeService
  ]
})
export class EmployeesModule { }
