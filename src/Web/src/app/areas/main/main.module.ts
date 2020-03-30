import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MainRoutingModule } from './main-routing.module';
import { HomeComponent } from './home/home.component';
import {GridModule, TableModule} from 'carbon-components-angular';
import { ChatComponent } from './chat/chat.component';
import { MessageComponent } from './chat/message/message.component';


@NgModule({
  declarations: [HomeComponent, ChatComponent, MessageComponent],
  imports: [
    CommonModule,
    MainRoutingModule,
    TableModule,
    GridModule,
  ]
})
export class MainModule { }
