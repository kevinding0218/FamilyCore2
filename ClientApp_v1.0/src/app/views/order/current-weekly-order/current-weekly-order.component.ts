import { MenuService } from './../../../services/menu/menu.service';
import { colors } from './../../../ngxModule/ng2-calendar/ng2-calendar-color';
import { element } from 'protractor';
import { ExcelService } from './../../../utility/export/exportExcelService';
import { ToastrService } from 'ngx-toastr';
import { HelperMethod } from './../../../utility/helper/helperMethod';
import { CurrentOrderService } from './../../../services/order/current-order.service';
import { OrderProcessingSingleEntree, OrderProcessInfo, OrderEntreeDetailInfo, EntreeOrderMapping, EntreeOrderMappingSchedule } from './../../../viewModels/order/saveOrder';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Observable } from 'rxjs/Rx';
import { Router } from '@angular/router';
import {
  startOfDay,
  endOfDay,
  subDays,
  addDays,
  endOfMonth,
  isSameDay,
  isSameMonth,
  addHours
} from 'date-fns';
import { Subject } from 'rxjs/Subject';
import {
  CalendarEvent,
  CalendarEventAction,
  CalendarEventTimesChangedEvent
} from 'angular-calendar';
import { NgProgress } from 'ngx-progressbar';


@Component({
  templateUrl: 'current-weekly-order.component.html',
  styleUrls: ['./current-weekly-order.component.css']
})
export class CurrentWeeklyOrderComponent implements OnInit {
  processStep: number = 1;
  processPercent: string;
  processValue: number;

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
    { title: 'Scheduled On' },
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
    private toastr: ToastrService,
    private _menuService: MenuService,
    public _ngProgress: NgProgress
  ) { }

  ngOnInit() {
    /** request started */
    this._ngProgress.start();
    this._currentOrderService.getCurrentWeekOrderPrepare()
      .subscribe(result => {
        this.currentWeekOrderInitialInfo = result;
        /** request completed */
        this._ngProgress.done();
      });
    this.processStep = 1;
    this.processPercent = '35%';
    this.processValue = 35;
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
        this.processStep = 2;
        this.processPercent = '70%';
        this.processValue = 70;
        this.initialCurrentEvents();
        this._menuService.sendBadgeUpdateMessage('reloadMenu');
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
    this.processStep = 1;
  }

  processOrderSummary() {
    this.processStep = 3;
    this.processPercent = '100%';
    this.processValue = 100;
    this.getCurrentWeekEntreeDetailList();
  }

  // EXPORT
  exportCurrentWeekEntreeDetailList(type) {
    if (type == 'entree') {
      let exportData = this.currentWeekOrderInitialInfo.entreeInfoList;

      exportData.forEach(function (element) {
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

      exportData.forEach(function (element) {
        delete element['stapleFood'];
      })

      this._excelService.exportAsExcelFile(exportData,
        'Entree Detail List Between ' + this.currentWeekOrderInitialInfo.startDate.toStringFormat() +
        ' To ' + this.currentWeekOrderInitialInfo.endDate.toStringFormat());
    }
  }


  // Drag Drop Calendar
  view: string = 'week';

  viewDate: Date = new Date();

  actions: CalendarEventAction[] = [
    {
      label: '<i class="fa fa-fw fa-pencil"></i>',
      onClick: ({ event }: { event: CalendarEvent }): void => {
        this.handleEvent('Edited', event);
      }
    },
    {
      label: '<i class="fa fa-fw fa-times"></i>',
      onClick: ({ event }: { event: CalendarEvent }): void => {
        this.externalEvents = this.externalEvents.filter(iEvent => iEvent !== event);
        this.handleEvent('Deleted', event);
      }
    }
  ];

  refresh: Subject<any> = new Subject();

  externalEvents: CalendarEvent[] = [];
  initialEvents: CalendarEvent[] = [];

  activeDayIsOpen: boolean = true;

  initialCurrentEvents() {
    var currentWeeklyEntreeList = this.currentWeekOrderInitialInfo.entreeInfoList;
    currentWeeklyEntreeList.forEach((element) => {
      console.log('element in initialCurrentEvents', element);
      if (element.scheduledDate == null) {
        let newCalendarEvent: CalendarEvent = {
          title: element.entreeName,
          color: this.assignColor(element),
          start: new Date(),
          allDay: true,
          draggable: true,
          meta: { orderId: this.currentWeekOrderInitialInfo.id, entreeId: element.entreeId }
        };
        this.externalEvents.push(newCalendarEvent);
      } else {
        let existedCalendarEvent: CalendarEvent = {
          title: element.entreeName,
          color: this.assignColor(element),
          start: new Date(element.scheduledDate),
          allDay: true,
          draggable: true,
          meta: { orderId: this.currentWeekOrderInitialInfo.id, entreeId: element.entreeId }
        };
        this.initialEvents.push(existedCalendarEvent);
      }
    });
    this.refresh.next();
    
    console.log('ngOnInit externalEvents', this.externalEvents);
  }

  assignColor(entreeInfo: OrderProcessingSingleEntree) {
    let color: any;
    switch (entreeInfo.style) {
      case "日本": {
        color = colors.red;
        break;
      }
      case "韩国": {
        color = colors.blue;
        break;
      }
      case "西餐": {
        color = colors.orange;
        break;
      }
      case "地中海": {
        color = colors.orange;
        break;
      }
      default: {
        color = colors.yellow;
        break;
      }
    }

    return color;
  }

  handleEvent(action: string, event: CalendarEvent): void {
    console.log('action is ' + action + '\nCalendarEvent is: ', event);
  }

  changeEventLog: string[] = [];

  eventDropped({
    event,
    newStart,
    newEnd
  }: CalendarEventTimesChangedEvent): void {
    console.log('eventDropped newStart', newStart);
    console.log('eventDropped newEnd', newEnd);
    console.log('eventDropped event', event);
    this.changeEventLog.push(event.title + ' has been placed on ' + newStart.toLocaleDateString());
    event.draggable = false;
    let existedMapping = this.initialEvents.find(e => e.title === event.title);
    if (existedMapping != null || typeof(existedMapping) != 'undefined') {
      if (existedMapping.start.toLocaleDateString() != newStart.toLocaleDateString()) {
        this.updateScheduledDateInDb(event, newStart, newEnd);
      } else {
        console.log('Event is not changed!');
      }
    } else {
      this.updateScheduledDateInDb(event, newStart, newEnd);
    }
    
  }
  
  updateScheduledDateInDb(event, newStart, newEnd) {
    var updateMapping: EntreeOrderMappingSchedule = {
      orderId: event.meta.orderId,
      entreeId: event.meta.entreeId,
      scheduleDate: newStart
    }
    console.log('updateMapping is ', updateMapping);
    this._currentOrderService.updateEntreeOrderSchedule(updateMapping)
      .subscribe(
      (data) => {
        event.draggable = true;
      },
      (err) => {
        HelperMethod.subscribeErrorHandler(err, this.toastr);
      }
      );
    const externalIndex: number = this.externalEvents.indexOf(event);
    if (externalIndex > -1) {
      this.externalEvents.splice(externalIndex, 1);
      this.initialEvents.push(event);
    }
    event.start = newStart;
    if (newEnd) {
      event.end = newEnd;
    }
    this.viewDate = newStart;
    this.activeDayIsOpen = true;
  }
}
