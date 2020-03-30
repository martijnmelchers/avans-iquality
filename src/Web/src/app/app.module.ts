import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgProgressModule } from "ngx-progressbar";
import { HeaderModule, TagModule } from "carbon-components-angular";
import { Fade20Module } from "@carbon/icons-angular/lib/fade/20";
import { BuddygroupComponent } from './buddy/buddygroup/buddygroup.component';
import { BuddygroupaddComponent } from './buddy/buddygroupadd/buddygroupadd.component';

@NgModule({
  declarations: [
    AppComponent,
    BuddygroupComponent,
    BuddygroupaddComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgProgressModule,
    HeaderModule,
    Fade20Module,
    TagModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
