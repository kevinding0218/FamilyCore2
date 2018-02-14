import { DataTableModule } from './../../components/data-table/index';
import { TestPageRoutingModule } from './test-page-routing.module';
import { AppNgxBootstrapModule } from './../../ngxModule/app-ngx-bootstrap.module';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

// Import common components
import {
    SetupEntreeCalendarComponent
} from './mwlCalendar';

import {
  Angular5DatatableComponent
} from './angular5Datatable';

const TEST_PAGE_COMPONENTS = [
    SetupEntreeCalendarComponent,
    Angular5DatatableComponent
]

@NgModule({
  imports: [ FormsModule, CommonModule, TestPageRoutingModule, AppNgxBootstrapModule, DataTableModule ],
  declarations: [
    ...TEST_PAGE_COMPONENTS
  ],
  providers: [ ]
})
export class TestPageModule { }

