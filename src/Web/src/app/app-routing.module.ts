import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {InviteComponent} from './areas/main/invite/invite.component'

const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./areas/main/main.module').then(m => m.MainModule)
  },
    {
      path: "invite/:id", component: InviteComponent
    },
    {
      path: "invite", component: InviteComponent
    },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
