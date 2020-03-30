import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from "./home/home.component";
import {InviteComponent} from "@IQuality/areas/main/invite/invite.component";


const routes: Routes = [
  {
    path: '',
    component: HomeComponent
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
