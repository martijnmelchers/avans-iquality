import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from "./home/home.component";
import { ChatComponent } from "./chat/chat.component";
import {InviteComponent} from "@IQuality/areas/main/invite/invite.component";
import {DoctorComponent} from '@IQuality/areas/main/doctor/doctor.component';
import {ChatInstanceComponent} from "@IQuality/areas/main/chat/chat-instance/chat-instance.component";
import { TipsComponent } from './doctor/tips/tips.component';
import { AddTipComponent } from './doctor/tips/addtip/addtip.component';
import { ManageComponent } from './doctor/tips/manage/manage.component';

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
    path: 'invite',
    component: InviteComponent
  },
  {
    path: 'doctor',
    component: DoctorComponent
  },
  {
    path: 'doctor/tips',
    component: TipsComponent
  },{
    path: 'doctor/tips/add',
    component: AddTipComponent
  },{
    path: 'doctor/tips/manage/:id',
    component: ManageComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainRoutingModule { }
