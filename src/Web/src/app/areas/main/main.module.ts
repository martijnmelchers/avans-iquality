import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MainRoutingModule } from './main-routing.module';
import { HomeComponent } from './home/home.component';
import {
  ButtonModule,
  GridModule,
  TilesModule,
  InputModule,
  TableModule,
  ListModule,
  StructuredListModule
} from 'carbon-components-angular';
import { ChatComponent } from './chat/chat.component';
import { MessageComponent } from './chat/message/message.component';
import {Send32Module} from "@carbon/icons-angular/lib/send/32";
import {ReactiveFormsModule} from "@angular/forms";
import {InviteComponent} from "@IQuality/areas/main/invite/invite.component";
import { ChatlistComponent } from './chat/chatlist/chatlist.component';

@NgModule({
  declarations: [HomeComponent, ChatComponent, MessageComponent, InviteComponent, ChatlistComponent],
  imports: [
    CommonModule,
    MainRoutingModule,
    TableModule,
    GridModule,
    InputModule,
    ButtonModule,
    Send32Module,
    ReactiveFormsModule,
    TilesModule,
    ListModule,
    StructuredListModule
  ]
})
export class MainModule { }
