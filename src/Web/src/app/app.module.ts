import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgProgressModule } from "ngx-progressbar";
import { HeaderModule, TagModule } from "carbon-components-angular";
import { Fade20Module } from "@carbon/icons-angular/lib/fade/20";
import { BuddygroupComponent } from './buddy/buddygroup/buddygroup.component';
import { BuddygroupaddComponent } from './buddy/buddygroupadd/buddygroupadd.component';
import { HttpClientModule } from '@angular/common/http';
import { TableModule } from "carbon-components-angular";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BuddyinviteComponent } from './buddyinvite/buddyinvite.component';
import { BuddyeditComponent } from './buddyedit/buddyedit.component';

@NgModule({
  declarations: [
    AppComponent,
    BuddygroupComponent,
    BuddygroupaddComponent,
    BuddyinviteComponent,
    BuddyeditComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    NgProgressModule,
    HeaderModule,
    FormsModule,
    Fade20Module,
    TagModule,
    TableModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
