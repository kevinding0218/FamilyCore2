import { EntreeService } from './../../../../../services/meal/entree.service';
import { EntreeDetailService } from '../../../../../services/meal/entree-detail.service';
import { Component, OnInit, Input, Output, EventEmitter, ViewChild } from '@angular/core';
import { Observable } from 'rxjs/Rx';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
    selector: 'entree-common-list',
    templateUrl: './entree-list-common.component.html'
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
        private toastr: ToastrService
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
        this._router.navigate(['/meal/entreeForm/' + this.splitById]);
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
}