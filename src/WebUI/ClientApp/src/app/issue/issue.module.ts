import { SharedModule } from './../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';

import { IssueListComponent } from './issue-list/issue-list.component';
import { IssueDetailsComponent } from './issue-details/issue-details.component';
import { IssueCreateComponent } from './issue-create/issue-create.component';
import { IssueUpdateComponent } from './issue-update/issue-update.component';
import { IssueDeleteComponent } from './issue-delete/issue-delete.component';
// import { IssueDeleteComponent } from './issue-delete/issue-delete.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    ReactiveFormsModule,
    RouterModule.forChild([
      { path: 'list', component: IssueListComponent },
      { path: 'details/:id', component: IssueDetailsComponent },
      { path: 'create', component: IssueCreateComponent },
      { path: 'update/:id', component: IssueUpdateComponent },
      // { path: 'delete/:id', component: IssueDeleteComponent }
    ])
  ],
  declarations: [
    IssueListComponent,
    IssueDetailsComponent,
    IssueCreateComponent,
    IssueUpdateComponent,
    IssueDeleteComponent,
    // IssueDeleteComponent
  ]
})
export class IssueModule { }
