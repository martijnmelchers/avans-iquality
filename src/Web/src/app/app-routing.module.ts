import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./areas/main/main.module').then(m => m.MainModule)
  },
<<<<<<< HEAD
=======
  {
    path: 'authenticate',
    loadChildren: () => import('./areas/authentication/authentication.module').then(m => m.AuthenticationModule)
  }
>>>>>>> feature/authentication+angular
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
