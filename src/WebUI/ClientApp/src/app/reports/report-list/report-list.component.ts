import { Component, OnInit } from '@angular/core';
import { ReportService } from '../shared/report.service';
import { ActivatedRoute, Params } from '@angular/router';
import { Report } from '../shared/report';
import { SharedService } from '../../shared/services/shared.service';

@Component({
  selector: 'app-report-list',
  templateUrl: './report-list.component.html',
  styleUrls: ['./report-list.component.css']
})
export class ReportListComponent implements OnInit {
  reports: Report[];
  errorMessage: string;

  constructor(private reportService: ReportService,
              public sharedService: SharedService,
              private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.activatedRoute.params.subscribe((params: Params) => {
      const issueId = params['id'];
      if (issueId > -1) {
        const reportsByProjectUrl = `api/issues/${issueId}/reports`;
        this.reportService.getData(reportsByProjectUrl)
          .subscribe(reports => this.reports = <Report[]>reports, error => this.errorMessage = <any>error);
      } else {
        this.getAllReports();
      }
    });
  }

  public getAllReports = () => {
    const apiAddress = 'api/reports';
    this.reportService.getReports(apiAddress)
      .subscribe(res => {
          this.reports = res as Report[];
        },
        (error) => {
          this.errorMessage = <any>error;
        });
  }


  deleteReport(report: Report): void {
    if (confirm('Are you sure you want to delete this report?')) {
      const deleteUrl = `api/reports/${report.reportId}`;
      this.reportService.deleteReport(deleteUrl)
        .subscribe(result => {
          const index = this.reports.indexOf(report);
          if (index > -1) {
            this.reports.splice(index, 1);
          }
        }, error => this.errorMessage = <any>error);
    }
  }
}
