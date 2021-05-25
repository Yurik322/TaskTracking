import { Component, OnInit } from '@angular/core';
import { Company } from '../shared/company.model';
import { Router, ActivatedRoute } from '@angular/router';
import { RepositoryService } from './../../shared/services/repository.service';
import { ErrorHandlerService } from './../../shared/services/error-handler.service';

@Component({
  selector: 'app-company-details',
  templateUrl: './company-details.component.html',
  styleUrls: ['./company-details.component.css']
})
export class CompanyDetailsComponent implements OnInit {
  public company: Company;
  public errorMessage = '';

  constructor(private repository: RepositoryService, private router: Router,
              private activeRoute: ActivatedRoute, private errorHandler: ErrorHandlerService) { }

  ngOnInit() {
    this.getCompanyDetails();
  }

  getCompanyDetails = () => {
    const id: string = this.activeRoute.snapshot.params['id'];
    const apiUrl = `api/company/${id}/employee`;
    // const apiUrl = `api/company/${id}/account`;

    this.repository.getData(apiUrl)
      .subscribe(res => {
          this.company = res as Company;
        },
        (error) => {
          this.errorHandler.handleError(error);
        });
  }

}
