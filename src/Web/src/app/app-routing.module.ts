import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


const routes: Routes = [
  {
    path: '',
    //loadChildren: () => import('./areas/main/main.module').then(m => m.MainModule)
    loadChildren: () => import('./areas/authentication/authentication.module').then(m => m.AuthenticationModule)
  },
  {
    path: 'authenticate',
    loadChildren: () => import('./areas/authentication/authentication.module').then(m => m.AuthenticationModule)
  },
  {
    path: 'admin',
    loadChildren: () => import('./areas/admin/admin.module').then(m => m.AdminModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
