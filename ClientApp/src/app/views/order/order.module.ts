import { ExcelService } from './../../utility/export/exportExcelService';
import { EntreeDetailSummaryByTypeComponent } from './common/entreeDetailSummaryByType/entree-detail-summary-by-type.component';
import { CurrentOrderService } from './../../services/order/current-order.service';
import { CurrentWeeklyOrderComponent } from './current-weekly-order/current-weekly-order.component';
import { OrderRoutingModule } from './order-routing.module';
import { AppNgxBootstrapModule } from './../../ngxModule/app-ngx-bootstrap.module';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SingleEntreeComponent } from './common/single-entree/single-entree.component';

@NgModule({
  imports: [ FormsModule, CommonModule, OrderRoutingModule, AppNgxBootstrapModule ],
  declarations: [
    CurrentWeeklyOrderComponent,
    EntreeDetailSummaryByTypeComponent,
    SingleEntreeComponent
  ],
  providers: [ CurrentOrderService, ExcelService ]
})
export class OrderModule { }

