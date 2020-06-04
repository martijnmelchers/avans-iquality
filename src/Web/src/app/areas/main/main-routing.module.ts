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
import {RoleGuard} from "@IQuality/core/guards/role.guard";
import {AuthGuard} from "@IQuality/core/guards/auth.guard";


const routes: Routes = [
  {
    path: 'chat',
    component: ChatComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: {roles: ['patient', 'admin']}
  },
  {
    path: 'chat/:chatId',
    component: ChatInstanceComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: {roles: ['patient', 'admin']}
  },
  {
    path: 'invite/:id',
    component: InviteComponent,
  },
  {
    path: 'create-invite',
    component: InviteComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: {roles: ['doctor', 'admin']}
  },
  {
    path: 'create-invite',
    component: InviteComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: {roles: ['doctor', 'admin']}
  },
  {
    path: 'create-invite/:chatId',
    component: InviteComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: {roles: ['doctor', 'admin']}
  },
  {
    path: 'doctor',
    component: DoctorComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: {roles: ['doctor', 'admin']}
  },
  {
    path: 'doctor/tips',
    component: TipsComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: {roles: ['doctor', 'admin']}
  },
  {
    path: 'doctor/tips/add',
    component: AddTipComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: {roles: ['doctor', 'admin']}
  },{
    path: 'doctor/tips/manage/:id',
    component: ManageComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: {roles: ['doctor', 'admin']}
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainRoutingModule { }
