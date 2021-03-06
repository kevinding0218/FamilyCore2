import { HelperMethod } from './../../../../../utility/helper/helperMethod';
import { style } from '@angular/animations';
import { EntreeDetailService } from '../../../../../services/meal/entree-detail.service';
import { Component, OnInit, Input, Output, EventEmitter, ViewChild } from '@angular/core';
import { Observable } from 'rxjs/Rx';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
    selector: 'entree-detail-list',
    templateUrl: './entree-detail-list.component.html',
    styleUrls: [ './entree-detail-list.component.css']
})

export class EntreeDetailListComponent implements OnInit {
    @Input() entreeDetailType: string = '';
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
    selected = [];

    ngx_columns = [
        { prop: 'keyValuePairInfo.id', name: 'Id' },
        { prop: 'keyValuePairInfo.name', name: 'Name' },
        { prop: 'addedOn', name: 'Added On' },
        { prop: 'addedByUserName', name: 'Added By' },
        { prop: 'numberOfEntreeIncluded', name: 'Entrees Included' },
        { prop: 'lastUpdatedByOn', name: 'Updated On' },
        { prop: 'note', name: 'Note' }
    ];

    ngx_detail_columns = [
        { prop: 'entreeName', name: 'Entree' },
        { prop: 'stapleFood', name: 'Staple Food' },
        { prop: 'vegetableCount', name: 'Vegetable Count' },
        { prop: 'meatCount', name: 'Meat Count' },
        { prop: 'seafoodCount', name: 'Seafood Count' },
        { prop: 'ingredientCount', name: 'Ingredient Count' },
        { prop: 'sauceCount', name: 'Sauce Count' },
        { prop: 'style', name: 'Style' },
        { prop: 'catagory', name: 'Catagory' },
        { prop: 'note', name: 'Entree Note' }
    ];


    constructor(
        private _route: ActivatedRoute,
        private _router: Router,
        private _entreeDetailService: EntreeDetailService,
        private toastr: ToastrService
    ) {

    }

    ngOnInit() {
        this.setCreateNewRedirection();
        this.populateDataTable();
    }

    private populateDataTable() {
        this._entreeDetailService.getEntreeDetails(this.entreeDetailType)
            .subscribe(result => {
                this.ngx_rows = this.temp_grid = result;
                setTimeout(() => { this.ngx_loadingIndicator = false; }, 1500);
            },
            (err) => {
                HelperMethod.subscribeErrorHandler(err, this.toastr);
            }
        );
    }

    editMainTableItem(value) {
        console.log('editMainTableItem value: ' + value);
        this.editRowRedirection(value);
        this.editRowClick.emit(value);
    }

    editRowRedirection(value) {
        switch (this.entreeDetailType.toLowerCase()) {
            case 'meat':
                this._router.navigate(['/meal/meatForm/meat/' + value]);
                break;
            case 'vegetable':
                this._router.navigate(['/meal/vegetableForm/vegetable/' + value]);
                break;
            case 'seafood':
                this._router.navigate(['/meal/seafoodForm/seafood/' + value]);
                break;
            case 'ingredient':
                this._router.navigate(['/meal/ingredientForm/ingredient/' + value]);
                break;
            case 'stapleFood':
                this._router.navigate(['/meal/staplefoodForm/staplefood/' + value]);
                break;
            case 'sauce':
                this._router.navigate(['/meal/SauceForm/sauce/' + value]);
                break;
        }
    }

    updateFilter(event) {
        const val = event.target.value.toLowerCase();

        // filter our data
        const temp = this.temp_grid.filter(function (d) {
            return d.keyValuePairInfo.name.toLowerCase().indexOf(val) !== -1 || !val;
        });

        // update the rows
        this.ngx_rows = temp;
        // Whenever the filter changes, always go back to the first page
        this.mainTable.offset = 0;
    }

    newEntreeDetailRedirection() {
        //this._router.navigate(['/meal/meatForm/meat/new']);
        this.createNewClick.emit(this.entreeDetailType);
    }

    setCreateNewRedirection() {
        switch (this.entreeDetailType.toLowerCase()) {
            case 'meat':
                this.createNewRouterLink = ['/meal/meatForm/meat/new'];
                break;
            case 'vegetable':
                this.createNewRouterLink = ['/meal/vegetableForm/vegetable/new'];
                break;
            case 'seafood':
                this.createNewRouterLink = ['/meal/seafoodForm/seafood/new'];
                break;
            case 'ingredient':
                this.createNewRouterLink = ['/meal/ingredientForm/ingredient/new'];
                break;
            case 'stapleFood':
                this.createNewRouterLink = ['/meal/staplefoodForm/staplefood/new'];
                break;
            case 'sauce':
                this.createNewRouterLink = ['/meal/sauceForm/sauce/new'];
                break;
        }
    }

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
        let id = row.keyValuePairInfo.Id;
        this.mainTable.rowDetail.toggleExpandRow(row);
        this.toggleExpandRowClick.emit(id);
    }

    onDetailToggle(event) {
        console.log('Detail Toggled', event);
    }

    filterData(event) {
        console.log(event);
        let columnName = event.target.id;
        const filterValue = event.target.value.toLowerCase();
        console.log('columnName is ' + columnName + '; filterValue is ' + filterValue);
        console.log('this.temp_grid is ', this.temp_grid);
        let filteredData = this.temp_grid.filter(function (d) {
            console.log('d is ', d);
            console.log('d.note is ' + d.note);
            console.log('d[note] is ' + d['note']);
            if (columnName == 'keyValuePairInfo.id') {
                return d.keyValuePairInfo.id.indexOf(filterValue) !== -1 || !filterValue;
            } else if (columnName == 'keyValuePairInfo.name') {
                return d.keyValuePairInfo.name.indexOf(filterValue) !== -1 || !filterValue;
            } else {
                return d[columnName].indexOf(filterValue) !== -1 || !filterValue;
            }
        });
        this.ngx_rows = filteredData;
        this.mainTable.offset = 0;
    }
}