import { element } from 'protractor';
import { ExcelService } from './../../../utility/export/exportExcelService';
import { ToastrService } from 'ngx-toastr';
import { HelperMethod } from './../../../utility/helper/helperMethod';
import { CurrentOrderService } from './../../../services/order/current-order.service';
import { OrderProcessingSingleEntree, OrderProcessInfo, OrderEntreeDetailInfo } from './../../../viewModels/order/saveOrder';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Observable } from 'rxjs/Rx';
import { Router } from '@angular/router';

@Component({
  templateUrl: 'current-weekly-order.component.html'
})
export class CurrentWeeklyOrderComponent implements OnInit {
  isProcessed: boolean = false;
  currentWeekOrderInitialInfo: OrderProcessInfo = {
    id: 0,
    startDate: null,
    endDate: null,
    addedOn: null,
    addedById: 0,
    lastUpdatedByOn: null,
    lastUpdatedById: 0,
    note: '',
    entreeInfoList: []
  };


  // Current Week Entree List Table
  entree_list_columns = [
    { title: 'Name' },
    { title: 'Entree Style', },
    { title: 'Entree Catagory' },
    { title: 'Count' },
    { title: 'Note' }
  ];

  // Current Week Entree Detail List Table
  entree_detail_list_columns = [
    { title: 'Name' },
    { title: 'Quantity' },
    { title: 'Type' },
  ]
  currentWeekEntreeDetails: OrderEntreeDetailInfo[] = [];


  constructor(
    private _currentOrderService: CurrentOrderService,
    private _excelService: ExcelService,
    private toastr: ToastrService
  ) { }

  ngOnInit() {
    this._currentOrderService.getCurrentWeekOrderPrepare()
      .subscribe(result => {
        this.currentWeekOrderInitialInfo = result;
      });
  }

  OnRemoveEntreeClick(entree) {
    console.log('CurrentWeeklyOrderComponent received:', entree);
    var index = this.currentWeekOrderInitialInfo.entreeInfoList.indexOf(entree, 0);
    if (index > -1) {
      this.currentWeekOrderInitialInfo.entreeInfoList.splice(index, 1);
    }
  }

  processInitialOrder() {
    this._currentOrderService.updateProcessingOrder(this.currentWeekOrderInitialInfo)
      .subscribe(
      (data) => {
        this.toastr.success('Entree has been added to current weekly order!', 'Add To Order Successfully');
        this.isProcessed = true;
        this.getCurrentWeekEntreeDetailList();
      },
      (err) => {
        HelperMethod.subscribeErrorHandler(err, this.toastr);
      });
    console.log('processInitialOrder: ', this.currentWeekOrderInitialInfo);
  }

  getCurrentWeekEntreeDetailList() {
    this._currentOrderService.getCurrentWeekOrderEntreeDetails()
      .subscribe(
      (data) => {
        this.currentWeekEntreeDetails = data;
      },
      (err) => {
        HelperMethod.subscribeErrorHandler(err, this.toastr);
      }
      );
  }

  goBackToInitialOrder() {
    this.isProcessed = false;
  }

  // EXPORT
  exportCurrentWeekEntreeDetailList(type) {
    if (type == 'entree') {
      let exportData = this.currentWeekOrderInitialInfo.entreeInfoList;

      exportData.forEach(function(element) {
        delete element['orderId'];
        delete element['entreeId'];
        delete element['entreeImgUrl'];
      })

      this._excelService.exportAsExcelFile(exportData,
        'Entree List Between ' + this.currentWeekOrderInitialInfo.startDate.toStringFormat() +
        ' To ' + this.currentWeekOrderInitialInfo.endDate.toStringFormat());
    }
    else {
      let exportData = this.currentWeekEntreeDetails;
      
      exportData.forEach(function(element) {
        delete element['stapleFood'];
      })

      this._excelService.exportAsExcelFile(exportData,
        'Entree Detail List Between ' + this.currentWeekOrderInitialInfo.startDate.toStringFormat() +
        ' To ' + this.currentWeekOrderInitialInfo.endDate.toStringFormat());
    }
  }


}
