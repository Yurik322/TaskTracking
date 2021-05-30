import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReportListComponent } from './report-list/report-list.component';
import { ReportEditComponent } from './report-edit/report-edit.component';
import { ReportCreateComponent } from './report-create/report-create.component';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ReportService } from './shared/report.service';

@NgModule({
  declarations: [
    ReportListComponent,
    ReportEditComponent,
    ReportCreateComponent
  ],
  imports: [
    CommonModule,
    // BrowserModule,
    FormsModule,
    RouterModule.forChild([
      {
        path: 'list',
        component: ReportListComponent
      },
      // TODO
      {
        path: ':id/employees',
        pathMatch: 'full',
        component: ReportListComponent,
      },
      {
        path: ':id/edit',
        component: ReportEditComponent,
      },
      {
        path: 'create',
        component: ReportCreateComponent
      }
    ])
  ],
  providers: [
    ReportService
  ]
})
export class ReportsModule { }
