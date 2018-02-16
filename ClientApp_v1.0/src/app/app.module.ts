import { SpinnerComponent } from './views/pages/spinner/spinner.component';
import { AuthenticateXHRBackend } from './services/member/authenticate-xhr.backend';
import { AuthService } from './services/auth/auth.service';
import { HttpClientModule } from '@angular/common/http';
import { AppNgxBootstrapModule } from './ngxModule/app-ngx-bootstrap.module';
import { ProgressService, BrowserXhrWithProgress } from './services/progress/progress.service';
import { ToastrModule } from 'ngx-toastr';
import { HttpModule, BrowserXhr, XHRBackend } from '@angular/http';
import { ErrorHandler } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { LocationStrategy, HashLocationStrategy } from '@angular/common';
import { AppErrorHandler } from './eventHandler/app.error-handler';
//Routing
import { AppRoutingModule } from './app-routing.module';
//App Component
import { AppComponent } from './app.component';

// Import containers
import {
  FullLayoutComponent,
  SimpleLayoutComponent
} from './containers';

const APP_CONTAINERS = [
  FullLayoutComponent,
  SimpleLayoutComponent
]

// Import components
import {
  AppAsideComponent,
  AppBreadcrumbsComponent,
  AppFooterComponent,
  AppHeaderComponent,
  AppSidebarComponent,
  AppSidebarFooterComponent,
  AppSidebarFormComponent,
  AppSidebarHeaderComponent,
  AppSidebarMinimizerComponent,
  APP_SIDEBAR_NAV
} from './components';

const APP_COMPONENTS = [
  AppAsideComponent,
  AppBreadcrumbsComponent,
  AppFooterComponent,
  AppHeaderComponent,
  AppSidebarComponent,
  AppSidebarFooterComponent,
  AppSidebarFormComponent,
  AppSidebarHeaderComponent,
  AppSidebarMinimizerComponent,
  APP_SIDEBAR_NAV
]

// Import directives
import {
  AsideToggleDirective,
  NAV_DROPDOWN_DIRECTIVES,
  ReplaceDirective,
  SIDEBAR_TOGGLE_DIRECTIVES
} from './directives';
import { ErrorLogService } from './services/event/error.log.service';

const APP_DIRECTIVES = [
  AsideToggleDirective,
  NAV_DROPDOWN_DIRECTIVES,
  ReplaceDirective,
  SIDEBAR_TOGGLE_DIRECTIVES
]

// Import services
import {
  MenuService, UserService
} from './services';

const APP_SERVICES = [
  MenuService
]

@NgModule({
  declarations: [
    AppComponent,
    ...APP_CONTAINERS,
    ...APP_COMPONENTS,
    ...APP_DIRECTIVES
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpModule,
    HttpClientModule,
    AppRoutingModule,
    AppNgxBootstrapModule,
    ToastrModule.forRoot({
      timeOut: 3000,
      positionClass: 'toast-top-right',
      closeButton: true,
      progressAnimation: 'increasing'
    })
  ],
  providers: [
    ...APP_SERVICES,
    {
      provide: LocationStrategy,
      useClass: HashLocationStrategy
    },
    {
      provide: ErrorHandler, 
      useClass: AppErrorHandler
    },
    AuthService,
    ErrorLogService
    // ,{ 
    //   provide: XHRBackend, 
    //   useClass: AuthenticateXHRBackend
    // }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
