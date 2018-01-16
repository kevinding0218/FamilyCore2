import { GridEntreeDetail } from './../../../../viewModels/meal/entreeDetail';
import { Router } from '@angular/router';
import { Component, OnInit, ViewChild } from '@angular/core';
import { VegetableService } from '../../../../services/meal/vegetable.service';

@Component({
  selector: 'vegetable-list',
  templateUrl: './vegetable-list.component.html'
})
export class VegetableListComponent implements OnInit {
  isDevelopment: boolean = (JSON.parse(localStorage.getItem('currentUser')) == 'true' ? true : false);
  private readonly PAGE_SIZE = 2;
  queryResult: any = {};
  allVegetables: GridEntreeDetail[];
  query: any = {
    pageSize: this.PAGE_SIZE
  };
  columns = [
    { title: 'Id' },
    { title: 'Name', key: 'name', isSortable: true },
    { title: 'Added On', key: 'addedOn', isSortable: true },
    { title: 'Added By', key: 'addedBy', isSortable: true },
    { title: 'Entrees Included', key: 'entreesIncluded', isSortable: true },
    { title: 'Updated On', key: 'updatedOn', isSortable: true },
    { title: 'Staple Food', key: 'stapleFood', isSortable: true },
    { title: 'Note', key: 'note', isSortable: false },
    {}
  ];

  @ViewChild('mainTable') mainTable: any;
  @ViewChild('levelOneDetailTable') detailtable: any;
  @ViewChild('levelTwoDetailTable') detaildetailtable: any;
  ngx_rows = [];
  ngx_loadingIndicator: boolean = true;
  ngx_reorderable: boolean = true;
  ngx_timeout: any;
  temp = [];

  ngx_columns = [
    { prop: 'keyValuePairInfo.id', name: 'Id' },
    { prop: 'keyValuePairInfo.name', name: 'Name' },
    { prop: 'addedOn', name: 'Added On' },
    { prop: 'addedByUserName', name: 'Added By' },
    { prop: 'numberOfEntreeIncluded', name: 'Entrees Included' },
    { prop: 'lastUpdatedByOn', name: 'Updated On' },
    { prop: 'note', name: 'Note' }
  ];


  constructor(private _vegetableService: VegetableService, private router: Router) { }

  ngOnInit() {
    this.populateDataTable();
  }

  private populateDataTable() {
    this._vegetableService.getVegetables(this.query)
      .subscribe(result => {
        this.queryResult = result;
        this.allVegetables = result.items;
        this.ngx_rows = this.temp = result.totalItemList;
        setTimeout(() => { this.ngx_loadingIndicator = false; }, 1500);
      });
  }

  onqueryChange() {
    // Client Side, for small dataset
    /* var queryResult = this.allVegetables;

    if (this.query.vegetableId)
      queryResult = queryResult.query(v => v.keyValuePairInfo.id == this.query.vegetableId);

    this.vegetables = queryResult; */
    this.query.page = 1;
    this.populateDataTable();
  }

  resetquery() {
    this.query = {
      page: 1,
      pageSize: this.PAGE_SIZE
  };
    this.onqueryChange();
  }

  sortBy(columnName) {
    if (this.query.sortBy == columnName) {
      this.query.isSortAscending = !this.query.isSortAscending;
    } else {
      this.query.sortBy = columnName;
      this.query.isSortAscending = true;
    }

    this.populateDataTable();
  }

  onPageChange(_pageIndex) {
    this.query.page = _pageIndex;
    this.populateDataTable();
  }

  editMainTableItem(value) {
    console.log('editMainTableItem value: ' + value);
    this.router.navigate(['/meal/vegetableForm/' + value]);
  }

  updateFilter(event) {
    const val = event.target.value.toLowerCase();

    // filter our data
    const temp = this.temp.filter(function(d) {
      return d.keyValuePairInfo.name.toLowerCase().indexOf(val) !== -1 || !val;
    });

    // update the rows
    this.ngx_rows = temp;
    // Whenever the filter changes, always go back to the first page
    this.mainTable.offset = 0;
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
  
  onPageDetailDetailTable(event) {
    clearTimeout(this.ngx_timeout);
    this.ngx_timeout = setTimeout(() => {
      console.log('onPageDetailDetailTable!', event);
    }, 100);
  }

  toggleExpandRow(row, expanded) {
    console.log('toggleExpandRow Row: ', row);
    console.log('toggleExpandRow expanded: ', expanded);
    let vegeId = row.keyValuePairInfo.Id;
    this.mainTable.rowDetail.toggleExpandRow(row);
  }

  toggleExpandDetailRow(row, expanded) {
    console.log('toggleExpandDetailRow Row: ', row);
    console.log('toggleExpandDetailRow Row expanded: ', expanded);
    let entreeId = row.entreeId;
    this.detailtable.rowDetail.toggleExpandRow(row);
  }

  onDetailToggle(){
    console.log('Detail Toggled', event);
  }

  onDetailDetailToggle(){
    console.log('Detail Detail Toggled', event);
  }
}
