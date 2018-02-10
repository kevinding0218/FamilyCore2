import { MemberAuthGuard } from './services/member/member.auth.guard.service';
import { P404Component } from './views/pages/404.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
//Object Component
import { AppComponent } from './app.component';

// Import Containers
import {
  FullLayoutComponent,
  SimpleLayoutComponent
} from './containers';


const routes: Routes = [
  {
    path: '', redirectTo: 'pages/login', pathMatch: 'full'
  },
  {
    path: '',
    component: FullLayoutComponent,
    data: {
      title: 'Home'
    },
    children: [
      {
        path: 'dashboard',
        loadChildren: './views/dashboard/dashboard.module#DashboardModule'
      },
      {
        path: 'meal',
        loadChildren: './views/meal/meal.module#MealModule'
      },
      {
        path: 'order',
        loadChildren: './views/order/order.module#OrderModule'
      }
    ]
  },
  {
    path: 'pages',
    component: SimpleLayoutComponent,
    data: {
      title: 'Pages'
    },
    children: [
      {
        path: '',
        loadChildren: './views/pages/pages.module#PagesModule',
      }
    ]
  }
  // ,{path: '**', redirectTo: '/pages/404'}
];

@NgModule({
  declarations:[
    
  ],
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
