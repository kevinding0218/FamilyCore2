import { ExcelService } from './../../utility/export/exportExcelService';
import { CurrentOrderService } from './../../services/order/current-order.service';
import { OrderRoutingModule } from './order-routing.module';
import { AppNgxBootstrapModule } from './../../ngxModule/app-ngx-bootstrap.module';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

// Import common components
import {
  EntreeDetailSummaryByTypeComponent,
  SingleEntreeComponent
} from './common';

const ORDER_COMMON_COMPONENTS = [
  EntreeDetailSummaryByTypeComponent,
  SingleEntreeComponent
]

import {
  CurrentWeeklyOrderComponent
} from './current-weekly-order';

const CURRENT_WEEKLY_COMPONENTS = [
  CurrentWeeklyOrderComponent
]

@NgModule({
  imports: [ FormsModule, CommonModule, OrderRoutingModule, AppNgxBootstrapModule ],
  declarations: [
    ...ORDER_COMMON_COMPONENTS,
    ...CURRENT_WEEKLY_COMPONENTS
  ],
  providers: [ CurrentOrderService, ExcelService ]
})
export class OrderModule { }

