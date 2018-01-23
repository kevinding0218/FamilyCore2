import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ModalModule } from 'ngx-bootstrap/modal';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { StarRatingModule } from 'angular-star-rating';
// Import 3rd party components
import { TabsModule } from 'ngx-bootstrap/tabs';

@NgModule({
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(),
    TooltipModule.forRoot(),
    ModalModule.forRoot(),
    StarRatingModule.forRoot(),
    NgxDatatableModule,
    TabsModule.forRoot()
  ],
  exports: [BsDropdownModule, TooltipModule, ModalModule, NgxDatatableModule, StarRatingModule, TabsModule]
})
export class AppNgxBootstrapModule { }
