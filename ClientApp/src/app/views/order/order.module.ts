import { CurrentOrderService } from './../../services/order/current-order.service';
import { CurrentWeeklyOrderComponent } from './current-weekly-order/current-weekly-order.component';
import { OrderRoutingModule } from './order-routing.module';
import { AppNgxBootstrapModule } from './../../ngxModule/app-ngx-bootstrap.module';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@NgModule({
  imports: [ FormsModule, CommonModule, OrderRoutingModule, AppNgxBootstrapModule ],
  declarations: [
    CurrentWeeklyOrderComponent
  ],
  providers: [ CurrentOrderService ]
})
export class OrderModule { }

