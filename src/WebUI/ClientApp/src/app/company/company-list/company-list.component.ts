import { ErrorHandlerService } from './../../shared/services/error-handler.service';
import { Component, OnInit } from '@angular/core';
import { RepositoryService } from './../../shared/services/repository.service';
import { Company } from './../../_interfaces/company.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-company-list',
  templateUrl: './company-list.component.html',
  styleUrls: ['./company-list.component.css']
})
export class CompanyListComponent implements OnInit {
  public companys: Company[];
  public errorMessage = '';

  constructor(private repository: RepositoryService, private errorHandler: ErrorHandlerService, private router: Router) { }

  ngOnInit(): void {
    this.getAllCompanys();
  }

  public getAllCompanys = () => {
    const apiAddress = 'api/company';
    this.repository.getData(apiAddress)
      .subscribe(res => {
          this.companys = res as Company[];
        },
        (error) => {
          this.errorHandler.handleError(error);
          this.errorMessage = this.errorHandler.errorMessage;
        });
  }

  // public getCompanyDetails = (id) => {
  //   const detailsUrl = `/company/details/${id}`;
  //   this.router.navigate([detailsUrl]);
  // }
  //
  // public redirectToUpdatePage = (id) => {
  //   const updateUrl = `/company/update/${id}`;
  //   this.router.navigate([updateUrl]);
  // }
  //
  // public redirectToDeletePage = (id) => {
  //   const deleteUrl = `/company/delete/${id}`;
  //   this.router.navigate([deleteUrl]);
  // }
}
