import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {NgProgressModule} from "ngx-progressbar";
import {HeaderModule, TagModule} from "carbon-components-angular";
import {Fade20Module} from "@carbon/icons-angular/lib/fade/20";
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import {JwtHelperService} from "@auth0/angular-jwt";
import {JwtHttpInterceptor} from "@IQuality/core/interceptor/jwt-http-interceptor";

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgProgressModule,
    HttpClientModule,
    HeaderModule,
    Fade20Module,
    TagModule,
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: JwtHttpInterceptor,
    multi: true,
  }],
  bootstrap: [AppComponent]
})
export class AppModule {
}
