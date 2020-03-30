import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BuddygroupComponent } from '../app/buddy/buddygroup/buddygroup.component';


const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./areas/main/main.module').then(m => m.MainModule)
  },
  {
    path: 'buddy/group',component: BuddygroupComponent

  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
