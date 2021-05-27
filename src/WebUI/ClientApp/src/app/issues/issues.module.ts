import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IssueFilterPipe } from './shared/issue-filter.pipe';
import { IssueFilterByPriorityPipe } from './shared/issue-filter-by-priority.pipe';
import { IssueFilterByStatusPipe } from './shared/issue-filter-by-status.pipe';
import { IssueFilterByTypePipe } from './shared/issue-filter-by-type.pipe';
import { IssueListComponent } from './issue-list/issue-list.component';
import { IssueEditComponent } from './issue-edit/issue-edit.component';
import { IssueCreateComponent } from './issue-create/issue-create.component';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { IssueService } from './shared/issue.service';

@NgModule({
  declarations: [
    IssueListComponent,
    IssueEditComponent,
    IssueCreateComponent,
    IssueFilterPipe,
    IssueFilterByPriorityPipe,
    IssueFilterByStatusPipe,
    IssueFilterByTypePipe],
  imports: [
    CommonModule,
    // BrowserModule,
    FormsModule,
    RouterModule.forChild([
      {
        path: 'list',
        component: IssueListComponent
      },
      // TODO
      {
        path: ':id',
        pathMatch: 'full',
        component: IssueListComponent,
      },
      {
        path: ':id/edit',
        component: IssueEditComponent,
      },
      {
        path: 'create',
        component: IssueCreateComponent
      }
    ])
  ],
  providers: [
    IssueService
  ]
})
export class IssuesModule { }
