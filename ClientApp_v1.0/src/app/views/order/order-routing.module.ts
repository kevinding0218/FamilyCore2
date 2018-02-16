import { CurrentWeeklyOrderComponent } from './current-weekly-order/current-weekly-order.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
    {
        path: '',
        component: CurrentWeeklyOrderComponent,
        data: {
            title: 'Current Weekly Order'
        }
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class OrderRoutingModule { }
