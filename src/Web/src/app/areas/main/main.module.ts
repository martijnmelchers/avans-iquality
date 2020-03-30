import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MainRoutingModule } from './main-routing.module';
import { HomeComponent } from './home/home.component';
import {ButtonModule, GridModule, InputModule, TableModule} from 'carbon-components-angular';
import { ChatComponent } from './chat/chat.component';
import { MessageComponent } from './chat/message/message.component';
import {Send32Module} from "@carbon/icons-angular/lib/send/32";
import {ReactiveFormsModule} from "@angular/forms";


@NgModule({
  declarations: [HomeComponent, ChatComponent, MessageComponent],
  imports: [
    CommonModule,
    MainRoutingModule,
    TableModule,
    GridModule,
    InputModule,
    ButtonModule,
    Send32Module,
    ReactiveFormsModule,
  ]
})
export class MainModule { }
