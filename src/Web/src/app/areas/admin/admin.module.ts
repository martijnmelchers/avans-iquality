import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminRoutingModule } from './admin-routing.module';
import { UsersComponent } from './users/users.component';
import {
  ButtonModule,
  GridModule,
  InputModule,
  ListModule, ModalModule, NotificationModule, NotificationService, PlaceholderService,
  StructuredListModule,
  TableModule, TagModule,
  TilesModule, ToggleModule,
} from 'carbon-components-angular';
import {MainRoutingModule} from "@IQuality/areas/main/main-routing.module";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {
  AddModule,
  CloseModule,
  ColumnModule, DeleteModule,
  DotMarkModule,
  SendAltModule,
  TrashCanModule
} from "@carbon/icons-angular";
import {ChartsModule} from "@carbon/charts-angular";
import {UserComponent} from "@IQuality/areas/admin/user/user.component";


@NgModule({
  declarations: [UsersComponent, UserComponent],
    imports: [
        AdminRoutingModule,
        CommonModule,
        MainRoutingModule,
        TableModule,
        GridModule,
        InputModule,
        ButtonModule,
        ReactiveFormsModule,
        TilesModule,
        ListModule,
        StructuredListModule,
        AddModule,
        FormsModule,
        SendAltModule,
        CloseModule,
        ColumnModule,
        TagModule,
        ToggleModule,
        DotMarkModule,
        TrashCanModule,
        DeleteModule,
        NotificationModule,
        ModalModule,
        ChartsModule,
    ]
})
export class AdminModule { }
