import { UserPasswordService } from './../../services/user/userpassword/userpassword.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ProfileComponent } from './profile/profile.component';
import { CallbackComponent } from './callback/callback.component';
import { NgModule } from '@angular/core';

import { P404Component } from './404.component';
import { P500Component } from './500.component';
import { LoginComponent } from './login.component';
import { RegisterComponent } from './register.component';

import { PagesRoutingModule } from './pages-routing.module';

@NgModule({
  imports: [ FormsModule, CommonModule, PagesRoutingModule ],
  declarations: [
    P404Component,
    P500Component,
    LoginComponent,
    RegisterComponent,
    CallbackComponent,
    ProfileComponent
  ],
  providers: [
    UserPasswordService
  ]
})
export class PagesModule { }
