import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProjectFilterPipe } from './shared/project-filter.pipe';
import { ProjectListComponent } from './project-list/project-list.component';
import { ProjectEditComponent } from './project-edit/project-edit.component';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ProjectService } from './shared/project.service';

@NgModule({
  declarations: [
    ProjectListComponent,
    ProjectEditComponent,
    ProjectFilterPipe
  ],
  imports: [
    CommonModule,
    // BrowserModule,
    FormsModule,
    RouterModule.forChild([
      {
        path: 'list',
        component: ProjectListComponent
      },
      {
        path: ':id/edit',
        component: ProjectEditComponent,
      },
      {
        path: 'create',
        component: ProjectEditComponent
      }
    ])
  ],
  providers: [
    ProjectService
  ]
})
export class ProjectsModule { }
