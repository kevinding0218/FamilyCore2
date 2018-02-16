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

import {
  AngularMaterialComponent
} from './angular-material-input';

import {MatInputModule} from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';

const TEST_PAGE_COMPONENTS = [
    SetupEntreeCalendarComponent,
    Angular5DatatableComponent,
    AngularMaterialComponent
]

@NgModule({
  imports: [ FormsModule, CommonModule, TestPageRoutingModule, AppNgxBootstrapModule, MatInputModule, MatFormFieldModule ],
  declarations: [
    ...TEST_PAGE_COMPONENTS
  ],
  providers: [ ]
})
export class TestPageModule { }

