import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

    public modalRef: BsModalRef; // {1}
    constructor(private modalService: BsModalService) { } // {2}

    public openModal(template: TemplateRef<any>) {
      this.modalRef = this.modalService.show(template); // {3}
    }

    ngOnInit() {
    }
}
