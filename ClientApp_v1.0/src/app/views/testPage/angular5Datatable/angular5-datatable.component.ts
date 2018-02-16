import { Component, ViewEncapsulation, ViewChild } from '@angular/core';

@Component({
  selector: 'responsive-demo',
  templateUrl: './angular5-datatable.component.html',
  //encapsulation: ViewEncapsulation.None

})
export class Angular5DatatableComponent {

  @ViewChild('myTable') table: any;

  rows: any[] = [];
  expanded: any = {};
  timeout: any;
  ngx_loadingIndicator: boolean = true;

  constructor() {
    this.fetch((data) => {
      this.rows = data;
    });
  }

  onPage(event) {
    clearTimeout(this.timeout);
    this.timeout = setTimeout(() => {
      console.log('paged!', event);
    }, 100);
  }

  fetch(cb) {
    const req = new XMLHttpRequest();
    req.open('GET', `assets/data/100k.json`);

    req.onload = () => {
      cb(JSON.parse(req.response));
      //setTimeout(() => { this.ngx_loadingIndicator = false; }, 1500);
    };

    req.send();
  }

  toggleExpandRow(row) {
    console.log('Toggled Expand Row!', row);
    this.table.rowDetail.toggleExpandRow(row);
  }

  onDetailToggle(event) {
    console.log('Detail Toggled', event);
  }

}