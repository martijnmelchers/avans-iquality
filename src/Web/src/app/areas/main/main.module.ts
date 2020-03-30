import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MainRoutingModule } from './main-routing.module';
import { HomeComponent } from './home/home.component';
import { GridModule, TableModule, TilesModule } from "carbon-components-angular";
import {InviteComponent} from "@IQuality/areas/main/invite/invite.component";


@NgModule({
  declarations: [HomeComponent, InviteComponent],
  imports: [
    CommonModule,
    MainRoutingModule,
    TableModule,
    GridModule,
    TilesModule
  ]
})
export class MainModule { }
