import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from "@IQuality/areas/authentication/login/login.component";
import {RegisterComponent} from "@IQuality/areas/authentication/register/register.component";


const routes: Routes = [
  {
    path: '',
    component: LoginComponent
  },
  {
    path: 'register/:inviteToken',
    component: RegisterComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthenticationRoutingModule { }
