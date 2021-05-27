import { Component, OnInit } from '@angular/core';
import { AttachmentService } from '../shared/attachment.service';
import { ActivatedRoute, Router, Params } from '@angular/router';
import { Attachment } from '../shared/attachment';

@Component({
  selector: 'app-attachment-list',
  templateUrl: './attachment-list.component.html',
  styleUrls: ['./attachment-list.component.css']
})
export class AttachmentListComponent implements OnInit {
  attachments: Attachment[];
  errorMessage: string;

  constructor(private attachmentService: AttachmentService,
              private activatedRoute: ActivatedRoute,
              private router: Router) { }

  ngOnInit() {
    this.activatedRoute.params.subscribe((params: Params) => {
      const issueId = params['id'];
      if (!issueId) {
        this.getAllAttachments();
      } else {
        const attachmentsByProjectUrl = `api/issues/${issueId}/attachments`;
        this.attachmentService.getData(attachmentsByProjectUrl)
          .subscribe(attachments => this.attachments = <Attachment[]>attachments, error => this.errorMessage = <any>error);
      }
    });
  }

  public getAllAttachments = () => {
    const apiAddress = 'api/attachments';
    this.attachmentService.getAttachments(apiAddress)
        .subscribe(res => {
              this.attachments = res as Attachment[];
            },
            (error) => {
              this.errorMessage = <any>error;
            });
  }

  deleteAttachment(attachment: Attachment) {
    if (confirm('Are you sure you want to delete this attachment?')) {
      const deleteUrl = `api/attachments/${attachment.attachmentId}`;
      this.attachmentService.deleteAttachment(deleteUrl).subscribe(result => {
          const index = this.attachments.indexOf(attachment);
          if (index > -1) {
            this.attachments.splice(index, 1);
          }
        },
        error => {
          this.errorMessage = error;
        }
      );
    }
  }
}
