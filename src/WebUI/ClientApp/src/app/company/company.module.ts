import { SharedModule } from './../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';

import { CompanyListComponent } from './company-list/company-list.component';
// import { CompanyDetailsComponent } from './company-details/company-details.component';
import { CompanyCreateComponent } from './company-create/company-create.component';
// import { CompanyUpdateComponent } from './company-update/company-update.component';
// import { CompanyDeleteComponent } from './company-delete/company-delete.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    ReactiveFormsModule,
    RouterModule.forChild([
      { path: 'list', component: CompanyListComponent },
      // { path: 'details/:id', component: CompanyDetailsComponent },
      { path: 'create', component: CompanyCreateComponent },
      // { path: 'update/:id', component: CompanyUpdateComponent },
      // { path: 'delete/:id', component: CompanyDeleteComponent }
    ])
  ],
  declarations: [
    CompanyListComponent,
    // CompanyDetailsComponent,
    CompanyCreateComponent,
    // CompanyUpdateComponent,
    // CompanyDeleteComponent
  ]
})
export class CompanyModule { }
