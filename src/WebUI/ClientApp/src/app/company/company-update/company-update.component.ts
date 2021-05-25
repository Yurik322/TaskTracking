import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ErrorHandlerService } from './../../shared/services/error-handler.service';
import { RepositoryService } from './../../shared/services/repository.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Company } from '../shared/company.model';

@Component({
  selector: 'app-company-update',
  templateUrl: './company-update.component.html',
  styleUrls: ['./company-update.component.css']
})
export class CompanyUpdateComponent implements OnInit {

  public errorMessage = '';
  public company: Company;
  public companyForm: FormGroup;

  constructor(private repository: RepositoryService, private errorHandler: ErrorHandlerService, private router: Router,
              private activeRoute: ActivatedRoute) { }

  ngOnInit() {
    this.companyForm = new FormGroup({
      name: new FormControl('', [Validators.required, Validators.maxLength(60)]),
    });

    this.getCompanyById();
  }

  private getCompanyById = () => {
    const companyId: string = this.activeRoute.snapshot.params['id'];

    const companyByIdUrl = `api/company/${companyId}`;

    this.repository.getData(companyByIdUrl).subscribe();
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

  public redirectToCompanyList = () => {
    this.router.navigate(['/company/list']);
  }

  public updateCompany = (companyFormValue) => {
    if (this.companyForm.valid) {
      this.executeCompanyUpdate(companyFormValue);
    }
  }

  private executeCompanyUpdate = (companyFormValue) => {

    console.log(this.company);
    //TODO company undefined!!!
    this.company.name = companyFormValue.name;

    const apiUrl = `api/company/${this.company.id}`;
    this.repository.update(apiUrl, this.company).subscribe();
      // .subscribe(res => {
      //     $('#successModal').modal();
      //   },
      //   (error => {
      //     this.errorHandler.handleError(error);
      //     this.errorMessage = this.errorHandler.errorMessage;
      //   })
      // )
  }
}
