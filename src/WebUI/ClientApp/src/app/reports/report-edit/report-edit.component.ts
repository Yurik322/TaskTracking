import { Component, OnInit } from '@angular/core';
import { Report } from '../shared/report';
import { ReportService } from '../shared/report.service';
import { ActivatedRoute, Router, Params } from '@angular/router';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-report-edit',
  templateUrl: './report-edit.component.html',
  styleUrls: ['./report-edit.component.css']
})
export class ReportEditComponent implements OnInit {
  report: Report;
  reportId: number;
  errorMessage: string;

  constructor(private reportService: ReportService,
              private activatedRoute: ActivatedRoute,
              private router: Router) { }

  ngOnInit() {
    this.reportId = +this.activatedRoute.snapshot.params['id'];

    if (this.reportId >= 0) {
      const reportByIdUrl = `api/reports/${this.reportId}`;
      this.reportService.getData(reportByIdUrl).subscribe(result => {
        this.report = <Report>result;
      }, error => this.errorMessage = <any>error);
    }
  }

  onSubmit(form: NgForm) {
    const apiUrl = `api/reports/${this.report.reportId}`;
    this.reportService.updateReport(apiUrl, this.report)
      .subscribe(result => {
        this.router.navigate(['/reports/list']);
      }, error => this.errorMessage = error);
  }

}
