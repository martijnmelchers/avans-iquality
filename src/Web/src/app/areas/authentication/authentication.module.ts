import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthenticationRoutingModule } from './authentication-routing.module';
import { CoreModule } from "@IQuality/core/core.module";
import { LoginComponent } from './login/login.component';
import {
  ButtonModule,
  CheckboxModule,
  GridModule,
  InputModule,
  NotificationModule,
  TilesModule
} from "carbon-components-angular";
import { ReactiveFormsModule } from "@angular/forms";


@NgModule({
  declarations: [LoginComponent],
  imports: [
    CoreModule,
    CommonModule,
    AuthenticationRoutingModule,
    GridModule,
    TilesModule,
    InputModule,
    ButtonModule,
    CheckboxModule,
    ReactiveFormsModule,
    NotificationModule
  ]
})
export class AuthenticationModule { }
