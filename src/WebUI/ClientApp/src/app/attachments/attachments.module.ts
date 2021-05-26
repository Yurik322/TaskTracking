import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AttachmentFilterPipe } from './shared/attachment-filter.pipe';
import { AttachmentListComponent } from './attachment-list/attachment-list.component';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AttachmentService } from './shared/attachment.service';



@NgModule({
  declarations: [
    AttachmentListComponent,
    AttachmentFilterPipe
  ],
  imports: [
    CommonModule,
    BrowserModule,
    FormsModule,
    RouterModule.forRoot([
      {
        path: 'attachments',
        component: AttachmentListComponent
      },
      {
        path: 'issues/:id/attachments',
        component: AttachmentListComponent,
      },
    ])
  ],
  providers: [
    AttachmentService
  ]
})
export class AttachmentsModule { }
