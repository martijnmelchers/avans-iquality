import {BrowserModule, HammerModule} from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgProgressModule } from "ngx-progressbar";
import {
  HeaderModule, ModalService,
  NotificationService,
  PanelModule,
  PlaceholderModule, PlaceholderService,
  SideNavModule,
  TagModule
} from "carbon-components-angular";
import { HTTP_INTERCEPTORS, HttpClientModule } from "@angular/common/http";
import { JwtHttpInterceptor } from "@IQuality/core/interceptor/jwt-http-interceptor";
import { TableModule } from "carbon-components-angular";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {FadeModule} from "@carbon/icons-angular";


@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    PlaceholderModule,
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
    ReactiveFormsModule,
    SideNavModule,
    FadeModule,
    PanelModule,
    PlaceholderModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtHttpInterceptor, multi: true },NotificationService
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
