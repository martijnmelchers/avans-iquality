import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgProgressModule } from "ngx-progressbar";
import { HeaderModule, TagModule } from "carbon-components-angular";
import { HTTP_INTERCEPTORS, HttpClientModule } from "@angular/common/http";
import { JwtHttpInterceptor } from "@IQuality/core/interceptor/jwt-http-interceptor";

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgProgressModule,
    HttpClientModule,
    HeaderModule,
    TagModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtHttpInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
