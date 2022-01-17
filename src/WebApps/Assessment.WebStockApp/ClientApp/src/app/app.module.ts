import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { SalesComponent } from './sales/sales.component';
import { LogComponent } from './log/log.component';
import { AuthenticationService } from '../services/authentication.service';
import { AlertifyService } from '../services/alertify.service';
import { HttpErrorInterceptor } from '../shared/error.interceptor';
import { CommonModule, HashLocationStrategy, LocationStrategy } from '@angular/common';
import { IntegrationService } from '../services/integration.service';
import { invoicedPipe } from '../models/pipes/invoiced.pipe';
import { LogService } from '../services/log.service';

@NgModule({
  declarations: [
    invoicedPipe,
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    SalesComponent,
    LogComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    CommonModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'sales', component: SalesComponent },
      { path: 'logs', component: LogComponent },
    ])
  ],
  exports: [
    invoicedPipe
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpErrorInterceptor,
      multi: true
    },
    AuthenticationService,
    AlertifyService,
    IntegrationService,
    LogService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
