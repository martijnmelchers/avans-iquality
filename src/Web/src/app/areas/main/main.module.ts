import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {MainRoutingModule} from './main-routing.module';
import {HomeComponent} from './home/home.component';
import {
  ButtonModule,
  GridModule,
  InputModule,
  ListModule,
  StructuredListModule,
  TableModule, TagModule,
  TilesModule, ToggleModule,
} from 'carbon-components-angular';
import {ChatComponent} from './chat/chat.component';
import {MessageComponent} from './chat/message/message.component';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {InviteComponent} from "@IQuality/areas/main/invite/invite.component";
import {ChatListComponent} from './chat/chatlist/chat-list.component';
import {AddModule, CloseModule, ColumnModule, SendAltModule} from "@carbon/icons-angular";

@NgModule({
  declarations: [HomeComponent, ChatComponent, MessageComponent, InviteComponent, ChatListComponent],
  imports: [
    CommonModule,
    MainRoutingModule,
    TableModule,
    GridModule,
    InputModule,
    ButtonModule,
    ReactiveFormsModule,
    TilesModule,
    ListModule,
    StructuredListModule,
    AddModule,
    FormsModule,
    SendAltModule,
    CloseModule,
    ColumnModule,
    TagModule,
    ToggleModule,
  ]
})
export class MainModule {
}
