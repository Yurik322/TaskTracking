import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CompanyForCreation } from '../../_interfaces/companyForCreation.model';
import { ErrorHandlerService } from './../../shared/services/error-handler.service';
import { RepositoryService } from './../../shared/services/repository.service';
import { Router } from '@angular/router';
// import { DatePipe } from '@angular/common';
// import * as $ from 'jquery';

@Component({
  selector: 'app-company-create',
  templateUrl: './company-create.component.html',
  styleUrls: ['./company-create.component.css']
})
export class CompanyCreateComponent implements OnInit {
  public errorMessage = '';

  public companyForm: FormGroup;

  // tslint:disable-next-line:max-line-length
  constructor(private repository: RepositoryService, private errorHandler: ErrorHandlerService, private router: Router) { }

  ngOnInit() {
    this.companyForm = new FormGroup({
      name: new FormControl('', [Validators.required, Validators.maxLength(60)]),
      fullAddress: new FormControl('', [Validators.required, Validators.maxLength(100)])
    });
  }

  public validateControl = (controlName: string) => {
    if (this.companyForm.controls[controlName].invalid && this.companyForm.controls[controlName].touched) {
      return true;
    }

    return false;
  }

  public hasError = (controlName: string, errorName: string) => {
    if (this.companyForm.controls[controlName].hasError(errorName)) {
      return true;
    }

    return false;
  }

  public createCompany = (companyFormValue) => {
    if (this.companyForm.valid) {
      this.executeCompanyCreation(companyFormValue);
    }
  }

  private executeCompanyCreation = (companyFormValue) => {
    const company: CompanyForCreation = {
      name: companyFormValue.name,
      fullAddress: companyFormValue.fullAddress
    };

    const apiUrl = 'api/company';
    this.repository.create(apiUrl, company);
      // .subscribe(
      //   res => {
      //     $('#successModal').modal();
      //   },
      //   (error => {
      //     this.errorHandler.handleError(error);
      //     this.errorMessage = this.errorHandler.errorMessage;
      //   })
      // );
  }

  public redirectToCompanyList() {
    this.router.navigate(['/company/list']);
  }

}
