import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {InviteComponent} from './areas/main/invite/invite.component'

const routes: Routes = [
  {path: "invite", component: InviteComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
