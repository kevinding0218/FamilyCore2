import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { NavMenuHeaderComponent } from '../navmenu/nav-menu-header/nav-menu-header.component';
import { NavMenuTopLinkComponent } from '../navmenu/nav-menu-top-link/nav-menu-top-link.component';
import { NavMenuSideBarComponent } from '../navmenu/nav-menu-side-bar/nav-menu-side-bar.component';
import { NavMenuComponent } from '../navmenu/navmenu.component';


@NgModule({
  declarations: [
    NavMenuHeaderComponent,
    NavMenuTopLinkComponent,
    NavMenuSideBarComponent,
    NavMenuComponent
  ],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports:[
    NavMenuComponent
  ]
})
export class AppNavMenuModule { }
