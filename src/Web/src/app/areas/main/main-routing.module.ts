import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from "./home/home.component";
import { ChatComponent } from "./chat/chat.component";
import {InviteComponent} from "@IQuality/areas/main/invite/invite.component";
import {DoctorComponent} from '@IQuality/areas/main/doctor/doctor.component';
import {ChatInstanceComponent} from "@IQuality/areas/main/chat/chat-instance/chat-instance.component";

const routes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'chat',
    component: ChatComponent
  },
  {
    path: 'chat/:chatId',
    component: ChatInstanceComponent
  },
  {
    path: 'invite/:id',
    component: InviteComponent
  },
  {
    path: 'create-invite',
    component: InviteComponent
  },
  {
    path: 'create-invite',
    component: InviteComponent
  },
  {
    path: 'create-invite/:chatId',
    component: InviteComponent
  },
  {
    path: 'doctor',
    component: DoctorComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainRoutingModule { }
