import { MenuService } from './../../../../../services/menu/menu.service';
import { HelperMethod } from './../../../../../utility/helper/helperMethod';
import { SaveInitialOrder, EntreeOrderMapping } from './../../../../../viewModels/order/saveOrder';
import { EntreeService } from './../../../../../services/meal/entree.service';
import { EntreeDetailService } from '../../../../../services/meal/entree-detail.service';
import { Component, OnInit, Input, Output, EventEmitter, ViewChild } from '@angular/core';
import { Observable } from 'rxjs/Rx';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { CurrentOrderService } from '../../../../../services/order/current-order.service';
import * as moment from 'moment';

@Component({
    selector: 'entree-common-list',
    templateUrl: './entree-list-common.component.html',
    styleUrls: ['./entree-list-common.component.css']
})

export class EntreeListCommonComponent implements OnInit {
    @Input() splitBy: string = '';
    @Input() splitById: number = null;
    @Input() entreeListFormHeader: string = '';
    @Input() newEntreeButtonText: string = '';

    @Output('createNewClick') createNewClick = new EventEmitter();
    @Output('editRowClick') editRowClick = new EventEmitter();
    @Output('toggleExpandRow') toggleExpandRowClick = new EventEmitter();

    @ViewChild('mainTable') mainTable: any;
    @ViewChild('levelOneDetailTable') detailtable: any;
    createNewRouterLink: string[] = [];

    ngx_rows = [];
    ngx_loadingIndicator: boolean = true;
    ngx_reorderable: boolean = true;
    ngx_timeout: any;
    temp_grid = [];
    //selected row
    selected = [];

    ngx_columns = [
        { prop: 'entreeId', name: 'Id' },
        { prop: 'entreeName', name: 'Entree' },
        { prop: 'vegetableCount', name: 'Vegetable Count' },
        { prop: 'meatCount', name: 'Meat Count' },
        { prop: 'stapleFood', name: 'Staple Food' },
        { prop: 'style', name: 'Style' },
        { prop: 'catagory', name: 'Catagory' },
        { prop: 'note', name: 'Entree Note' },
        { prop: 'addedByUserName', name: 'Added By' }
    ];

    ngx_detail_columns = [
        { prop: 'vegetable', name: 'Vegetable' },
        { prop: 'meat', name: 'Meat' },
        { prop: 'seafood', name: 'Sea Food' },
        { prop: 'ingredient', name: 'Ingredient' },
        { prop: 'sauce', name: 'Sauce' },
        { prop: 'note', name: 'Entree Note' }
    ];


    constructor(
        private _route: ActivatedRoute,
        private _router: Router,
        private _entreeService: EntreeService,
        private toastr: ToastrService,
        private _currentOrderService: CurrentOrderService,
        private _menuService: MenuService
    ) {

    }

    ngOnInit() {
        this.populateDataTable();
    }

    private populateDataTable() {
        this._entreeService.getEntrees(this.splitBy, this.splitById)
            .subscribe(result => {
                this.ngx_rows = this.temp_grid = result;
                setTimeout(() => { this.ngx_loadingIndicator = false; }, 1500);
            });
    }

    editMainTableItem(value) {
        console.log('editMainTableItem value: ' + value);
        this._router.navigate(['/meal/entreeForm/update/' + this.splitBy + '/' + this.splitById + '/' + value]);
        this.editRowClick.emit(value);
    }

    updateFilter(event) {
        const val = event.target.value.toLowerCase();

        // filter our data
        const temp = this.temp_grid.filter(function (d) {
            return d.entreeName.toLowerCase().indexOf(val) !== -1 || !val;
        });

        // update the rows
        this.ngx_rows = temp;
        // Whenever the filter changes, always go back to the first page
        this.mainTable.offset = 0;
    }

    /*  newEntreeRedirection() {
         this._router.navigate(['/meal/entreeForm/new/' + this.splitBy + '/' + this.splitById]);
         this.createNewClick.emit(this.splitBy);
     } */

    onPageMainTable(event) {
        clearTimeout(this.ngx_timeout);
        this.ngx_timeout = setTimeout(() => {
            console.log('onPageMainTable!', event);
        }, 100);
    }

    onPageDetailTable(event) {
        clearTimeout(this.ngx_timeout);
        this.ngx_timeout = setTimeout(() => {
            console.log('onPageDetailTable!', event);
        }, 100);
    }

    toggleExpandRow(row, expanded) {
        console.log('toggleExpandRow Row: ', row);
        console.log('toggleExpandRow expanded: ', expanded);
        let id = row.entreeId;
        this.mainTable.rowDetail.toggleExpandRow(row);
        this.toggleExpandRowClick.emit(id);
    }

    onDetailToggle() {
        console.log('Detail Toggled', event);
    }

    // Select row
    onSelect({ selected }) {
        //console.log('Select Event', selected, this.selected);

        this.selected.splice(0, this.selected.length);
        this.selected.push(...selected);
    }

    onActivate(event) {
        //console.log('Activate Event', event);
    }

    selectFn(value) {
        console.log('selectFn value:', value);
    }

    onCheckboxChangeFn(event) {
        console.log('onCheckboxChangeFn', event);
    }

    add() {
        this.selected.push(this.ngx_rows[1], this.ngx_rows[3]);
    }

    update() {
        this.selected = [this.ngx_rows[1], this.ngx_rows[3]];
    }

    remove() {
        this.selected = [];
    }

    // Ordering
    addToOrder() {
        console.log('addToOrder:', this.selected);
        if (this.selected.length == 0) {
            this.toastr.warning('Please select at least one entree.', 'Invalid Operation');
        } else {
            let entreeOrderMappingList: EntreeOrderMapping[] = [];
            this.selected.forEach(function (element) {
                let newEntreeOrderMapping: EntreeOrderMapping = {
                    entreeId: element.entreeId,
                    count: 1,
                    note: element.note
                }
                entreeOrderMappingList.push(newEntreeOrderMapping);
            });

            this.findCurrentOrderIdIfExisted(entreeOrderMappingList);
        }

    }

    findCurrentOrderIdIfExisted(entreeOrderMappingList) {

        let currentDate = new Date();
        //console.log(currentDate);
        let currentDateStr = currentDate.toLocaleDateString();//toUTCDateTimeDigits();
        //console.log('currentDate is ' + currentDateStr);
        this._currentOrderService.getOrderIdByCurrentDate(currentDateStr)
            .subscribe(
            (data) => {
                if (data.id == 0) {
                    let startDateOfCurrentWeek = new Date().getWeekStartDate();
                    let endDateOfCurrentWeek = new Date().getWeekEndDate();
                    //let startDateOfCurrentWeek = moment().startOf('isoWeek').toDate();
                    //let endDateOfCurrentWeek = moment().endOf('isoWeek').toDate();
                    let _startWeek = moment().startOf('week').toDate();
                    let _endWeek = moment().endOf('week').toDate();
                    //console.log('_startWeek is ' + _startWeek + '\n_endWeek is ' + _endWeek);

                    let SaveInitialOrder: SaveInitialOrder = {
                        id: 0,
                        startDate: _startWeek,
                        endDate: _endWeek,
                        addedOn: new Date(),
                        addedById: 2,
                        lastUpdatedByOn: null,
                        lastUpdatedById: 0,
                        note: '',
                        entreeOrderMappingsWithCurrentOrder: entreeOrderMappingList
                    }
                    console.log('Ready to add', SaveInitialOrder);
                    this._currentOrderService.createOrder(SaveInitialOrder)
                        .subscribe(
                        (data) => {
                            this.toastr.success('Entree has been added to current weekly order!', 'Add To Order Successfully');
                            this._menuService.sendBadgeUpdateMessage('reloadMenu');
                        },
                        (err) => {
                            HelperMethod.subscribeErrorHandler(err, this.toastr);
                        });
                } else {
                    var existedOrderId = data.id;
                    console.log('existedOrderId: ', existedOrderId);
                    this._currentOrderService.getOrderByOrderId(existedOrderId, true, false)
                        .subscribe(
                        (data) => {
                            entreeOrderMappingList.forEach(newMapping => {
                                data.entreeOrderMappingsWithCurrentOrder.forEach(existedMapping => {
                                    if (existedMapping.entreeId == newMapping.entreeId){
                                        existedMapping.count += 1;
                                    }
                                    else
                                    {
                                        let item = data.entreeOrderMappingsWithCurrentOrder.find(e => e.entreeId === newMapping.entreeId);
                                        if (typeof(item) == 'undefined')
                                            data.entreeOrderMappingsWithCurrentOrder.push({ entreeId: newMapping.entreeId, count: 1, note: newMapping.note })
                                    }
                                })
                            });
                            this._currentOrderService.updateInitialOrder(data)
                                .subscribe(
                                (data) => {
                                    this.toastr.success('Entree has been added to current weekly order!', 'Add To Order Successfully');
                                    this._menuService.sendBadgeUpdateMessage('reloadMenu');
                                },
                                (err) => {
                                    HelperMethod.subscribeErrorHandler(err, this.toastr);
                                });
                            console.log('existed Order: ', data);
                        });
                }
            });
    }
}