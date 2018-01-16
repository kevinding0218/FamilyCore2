import { Router } from '@angular/router';
import { Component, OnInit, ViewChild } from '@angular/core';
import { StaplefoodService } from '../../../../services/meal/staplefood.service';

@Component({
  selector: 'app-staplefood-list',
  templateUrl: './staplefood-list.component.html'
})
export class StaplefoodListComponent implements OnInit {
  isDevelopment: boolean = (JSON.parse(localStorage.getItem('currentUser')) == 'true' ? true : false);
  @ViewChild('mainTable') mainTable: any;
  @ViewChild('levelOneDetailTable') detailtable: any;
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

  constructor(private _staplefoodService: StaplefoodService, private router: Router) { }

  ngOnInit() {
    this.populateDataTable();
  }

  private populateDataTable() {
    this._staplefoodService.getStaplefoods()
      .subscribe(result => {
        this.ngx_rows = this.temp = result;
        setTimeout(() => { this.ngx_loadingIndicator = false; }, 1500);
      });
  }

  editMainTableItem(value) {
    console.log('editMainTableItem value: ' + value);
    this.router.navigate(['/meal/staplefoodForm/' + value]);
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

  toggleExpandRow(row, expanded) {
    console.log('toggleExpandRow Row: ', row);
    console.log('toggleExpandRow expanded: ', expanded);
    let vegeId = row.keyValuePairInfo.Id;
    this.mainTable.rowDetail.toggleExpandRow(row);
  }

  onDetailToggle(){
    console.log('Detail Toggled', event);
  }

}
