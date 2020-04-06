import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from "./home/home.component";
import { ChatComponent } from "./chat/chat.component";
import {InviteComponent} from "@IQuality/areas/main/invite/invite.component";

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
    path: 'invite',
    component: InviteComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainRoutingModule { }
