import { CalendarHeaderComponent } from './ng2-calendar/calendar-header.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ModalModule } from 'ngx-bootstrap/modal';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { StarRatingModule } from 'angular-star-rating';
// Import 3rd party components
import { TabsModule } from 'ngx-bootstrap/tabs';
import { CalendarModule } from 'angular-calendar';
import { Daterangepicker } from 'ng2-daterangepicker';

@NgModule({
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(),
    TooltipModule.forRoot(),
    ModalModule.forRoot(),
    StarRatingModule.forRoot(),
    NgxDatatableModule,
    TabsModule.forRoot(),
    CalendarModule.forRoot(),
    Daterangepicker
  ],
  declarations: [CalendarHeaderComponent],
  exports: [BsDropdownModule, TooltipModule, ModalModule, NgxDatatableModule, StarRatingModule, TabsModule, CalendarModule, Daterangepicker, CalendarHeaderComponent]
})
export class AppNgxBootstrapModule { }
