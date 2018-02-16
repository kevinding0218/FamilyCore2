import { OrderProcessingSingleEntree } from './../../../../viewModels/order/saveOrder';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';


@Component({
  selector: 'app-single-entree',
  templateUrl: './single-entree.component.html',
  styleUrls: [ './single-entree.component.css' ]
})
export class SingleEntreeComponent implements OnInit {
  @Input() entree: OrderProcessingSingleEntree;
  @Output('removeEntree') removeEntreeEmitter = new EventEmitter();

  apiFtp: string = localStorage.getItem('WebApiFtp').toString();
  constructor() { }

  ngOnInit() {
  }

  removeCurrentEntree() {
    console.log('SingleEntreeComponent send:',this.entree);
    this.removeEntreeEmitter.emit(this.entree);
  }

}
