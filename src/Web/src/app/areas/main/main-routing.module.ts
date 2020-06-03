import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from "./home/home.component";
import { ChatComponent } from "./chat/chat.component";
import {InviteComponent} from "@IQuality/areas/main/invite/invite.component";
import {DoctorComponent} from '@IQuality/areas/main/doctor/doctor.component';
import {ChatInstanceComponent} from "@IQuality/areas/main/chat/chat-instance/chat-instance.component";
import {RoleGuard} from "@IQuality/core/guards/role.guard";
import {AuthGuard} from "@IQuality/core/guards/auth.guard";

const routes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'chat',
    component: ChatComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: {roles: ['Patient', 'Admin']}
  },
  {
    path: 'chat/:chatId',
    component: ChatInstanceComponent
  },
  {
    path: 'invite/:id',
    component: InviteComponent,
  },
  {
    path: 'create-invite',
    component: InviteComponent,
    canActivate: [RoleGuard],
    data: {roles: ['Doctor', 'Admin']}
  },
  {
    path: 'create-invite',
    component: InviteComponent,
    canActivate: [RoleGuard],
    data: {roles: ['Doctor', 'Admin']}
  },
  {
    path: 'create-invite/:chatId',
    component: InviteComponent,
    canActivate: [RoleGuard],
    data: {roles: ['Doctor', 'Admin']}
  },
  {
    path: 'doctor',
    component: DoctorComponent,
    canActivate: [RoleGuard],
    data: {roles: ['Doctor', 'Admin']}
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainRoutingModule { }
