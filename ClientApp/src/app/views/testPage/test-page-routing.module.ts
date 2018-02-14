import { Angular5DatatableComponent } from './angular5Datatable/angular5-datatable.component';
import { NgModule } from '@angular/core';
import {
  Routes,
  RouterModule
} from '@angular/router';

import { SetupEntreeCalendarComponent } from './mwlCalendar';

const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Meal'
    },
    children: [
      {
        path: 'mwlCalendar',
        component: SetupEntreeCalendarComponent,
        data: {
          title: 'mwl Calendar'
        }
      },
      {
        path: 'angular5DataTable',
        component: Angular5DatatableComponent,
        data: {
          title: 'Angular 5 DataTable'
        }
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TestPageRoutingModule { }
