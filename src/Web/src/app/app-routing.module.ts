import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BuddygroupComponent } from '../app/buddy/buddygroup/buddygroup.component';
import { BuddygroupaddComponent } from '../app/buddy/buddygroupadd/buddygroupadd.component';
import { BuddyinviteComponent } from './buddyinvite/buddyinvite.component';
import { BuddyeditComponent } from './buddyedit/buddyedit.component';
import { BuddygrouplistComponent } from './buddygrouplist/buddygrouplist.component';


const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./areas/main/main.module').then(m => m.MainModule)
  },
  {
    path: 'buddy',component: BuddygroupComponent
  },
  {
    path: 'buddy/add',component: BuddygroupaddComponent
  },
  {
    path: 'buddy/invite',component: BuddyinviteComponent
  },
  {
    path: 'buddy/list',component: BuddygrouplistComponent
  },
  {
    path: 'buddy/:id',component: BuddyeditComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
