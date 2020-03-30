import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MainRoutingModule } from './main-routing.module';
import { HomeComponent } from './home/home.component';
import { TableModule } from "carbon-components-angular";


@NgModule({
  declarations: [HomeComponent],
  imports: [
    CommonModule,
    MainRoutingModule,
    TableModule
  ]
})
export class MainModule { }
