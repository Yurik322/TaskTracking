import { Component, OnInit } from '@angular/core';
import { Project } from '../shared/project';
import { Router, ActivatedRoute } from '@angular/router';
import { ProjectService } from '../shared/project.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-project-edit',
  templateUrl: './project-edit.component.html',
  styleUrls: ['./project-edit.component.css']
})
export class ProjectEditComponent implements OnInit {
  project: Project = {} as Project;
  id: number;
  errorMessage: string;

  constructor(private router: Router,
              private activatedRoute: ActivatedRoute,
              private projectService: ProjectService) { }

  ngOnInit() {
    this.id = +this.activatedRoute.snapshot.params['id'];

    if (this.id >= 0) {
      this.projectService.getProject(this.id).subscribe(result => {
        this.project = result;
      }, error => this.errorMessage = <any>error);
    }
  }

  // onSubmit(form: NgForm) {
  //   if (this.id >= 0) {
  //     this.projectService.updateProject(this.project).subscribe(data => {
  //       this.router.navigate(['/projects']);
  //     }, error => this.errorMessage = <any>error);
  //   }
  //   else {
  //     this.projectService.createProject(this.project).subscribe(data => {
  //       this.router.navigate(['/projects']);
  //     }, error => this.errorMessage = <any>error);
  //   }
  // }
}
