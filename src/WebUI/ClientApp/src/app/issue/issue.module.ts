// import { SharedModule } from './../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IssueFilterPipe } from './shared/issue-filter.pipe';
import { IssueFilterByPriorityPipe } from './shared/issue-filter-by-priority.pipe';
import { IssueFilterByStatusPipe } from './shared/issue-filter-by-status.pipe';
import { IssueFilterByTypePipe } from './shared/issue-filter-by-type.pipe';
// import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';

import { IssueListComponent } from './issue-list/issue-list.component';
import { IssueDetailsComponent } from './issue-details/issue-details.component';
import { IssueCreateComponent } from './issue-create/issue-create.component';
import { IssueUpdateComponent } from './issue-update/issue-update.component';
import { IssueDeleteComponent } from './issue-delete/issue-delete.component';
import {SharedModule} from '../shared/shared.module';
// import { IssueDeleteComponent } from './issue-delete/issue-delete.component';

@NgModule({
  imports: [
    CommonModule,
    // BrowserModule,
    FormsModule,
    // SharedModule,
    ReactiveFormsModule,
    RouterModule.forChild([
      {path: 'list', component: IssueListComponent},
      {path: 'details/:id', component: IssueDetailsComponent},
      {path: 'create', component: IssueCreateComponent},
      {path: 'update/:id', component: IssueUpdateComponent},
      // { path: 'delete/:id', component: IssueDeleteComponent }
    ]),
    SharedModule
  ],
  declarations: [
    IssueListComponent,
    IssueDetailsComponent,
    IssueCreateComponent,
    IssueUpdateComponent,
    IssueDeleteComponent,
    // IssueDeleteComponent,
    IssueFilterPipe,
    IssueFilterByPriorityPipe,
    IssueFilterByStatusPipe,
    IssueFilterByTypePipe
  ]
})
export class IssueModule { }
