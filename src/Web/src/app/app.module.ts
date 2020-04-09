import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgProgressModule } from "ngx-progressbar";
import { HeaderModule, TagModule } from "carbon-components-angular";
import { HTTP_INTERCEPTORS, HttpClientModule } from "@angular/common/http";
import { JwtHttpInterceptor } from "@IQuality/core/interceptor/jwt-http-interceptor";
import { BuddygroupComponent } from './buddy/buddygroup/buddygroup.component';
import { BuddygroupaddComponent } from './buddy/buddygroupadd/buddygroupadd.component';
import { TableModule } from "carbon-components-angular";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BuddyinviteComponent } from './buddyinvite/buddyinvite.component';
import { BuddyeditComponent } from './buddyedit/buddyedit.component';
import { BuddygrouplistComponent } from './buddygrouplist/buddygrouplist.component';
import { BuddygroupitemComponent } from './buddygroupitem/buddygroupitem.component';

@NgModule({
  declarations: [
    AppComponent,
    BuddygroupComponent,
    BuddygroupaddComponent,
    BuddyinviteComponent,
    BuddyeditComponent,
    BuddygrouplistComponent,
    BuddygroupitemComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    NgProgressModule,
    HttpClientModule,
    HeaderModule,
    TagModule,
    FormsModule,
    TagModule,
    TableModule,
    ReactiveFormsModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtHttpInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
