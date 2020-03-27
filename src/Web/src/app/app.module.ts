import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {NgProgressModule} from "ngx-progressbar";
import {HeaderModule} from "carbon-components-angular";
import {Fade20Module} from "@carbon/icons-angular/lib/fade/20";

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgProgressModule,
    HeaderModule,
    Fade20Module
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
