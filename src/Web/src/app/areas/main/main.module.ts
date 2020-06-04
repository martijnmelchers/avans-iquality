import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {MainRoutingModule} from './main-routing.module';
import {HomeComponent} from './home/home.component';
import {
    ButtonModule, DialogModule,
    GridModule,
    InputModule,
    ListModule, ModalModule, NotificationModule, PlaceholderModule, SearchModule,
    StructuredListModule,
    TableModule, TagModule,
    TilesModule, ToggleModule,
} from 'carbon-components-angular';
import {ChatComponent} from './chat/chat.component';
import {MessageComponent} from './chat/message/message.component';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {InviteComponent} from "@IQuality/areas/main/invite/invite.component";
import {ChatListComponent} from './chat/chatlist/chat-list.component';
import {
  AddModule,
  CloseModule,
  ColumnModule, DeleteModule,
  DotMarkModule,
  SendAltModule,
  TrashCanModule
} from "@carbon/icons-angular";
import {DoctorComponent} from "@IQuality/areas/main/doctor/doctor.component";
import { ChatInstanceComponent } from './chat/chat-instance/chat-instance.component';
import { TipsComponent } from './doctor/tips/tips.component';
import { AddTipComponent } from './doctor/tips/addtip/addtip.component';
import { ManageComponent } from './doctor/tips/manage/manage.component';
import {BotMessageComponent} from "@IQuality/areas/main/chat/message/bot-message/bot-message.component";
import {ChartsModule} from "@carbon/charts-angular";
import {UserMessageComponent} from "@IQuality/areas/main/chat/message/user-message/user-message.component";

@NgModule({
  declarations: [
    HomeComponent,
    ChatComponent,
    MessageComponent,
    InviteComponent,
    ChatListComponent,
    DoctorComponent,
    ChatInstanceComponent,
    BotMessageComponent,
    UserMessageComponent,
	 TipsComponent, 
	 AddTipComponent, 
	 ManageComponent,
  ],
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
        DotMarkModule,
        TrashCanModule,
        DeleteModule,
        NotificationModule,
        ModalModule,
        ChartsModule,
        PlaceholderModule,
        SearchModule,
        DialogModule,
    ],
  providers: []
})
export class MainModule {
}
