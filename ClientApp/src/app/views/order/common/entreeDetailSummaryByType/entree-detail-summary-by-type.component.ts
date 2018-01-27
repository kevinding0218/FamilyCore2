import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';


@Component({
  selector: 'app-entree-detail-summary-by-type',
  templateUrl: './entree-detail-summary-by-type.component.html',
  styles: [
    '.edit_ul_li { display: inline-block;padding-right: 10px; }',
    '.input-sm-space { padding-left: 10px; }',
    '.input-md-space { padding-left: 15px; }',
    '.col-form-label { font-weight: bold; }'
]
})
export class EntreeDetailSummaryByTypeComponent implements OnInit {
  entreeDetailList: any;

  constructor() { }

  ngOnInit() {
      this.entreeDetailList = [
          {entreeDetailName: 'VEGE', entreeDetailQty: 11},
          {entreeDetailName: 'VEGE', entreeDetailQty: 22},
          {entreeDetailName: 'VEGE', entreeDetailQty: 33},
          {entreeDetailName: 'VEGE', entreeDetailQty: 44}
      ]
  }

}
