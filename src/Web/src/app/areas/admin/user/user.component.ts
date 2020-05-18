import { Component, OnInit } from '@angular/core';
import {UserService} from "@IQuality/core/services/user.service";
import {ApplicationUser} from "@IQuality/core/models/application-user";
import {TableHeaderItem, TableItem, TableModel} from "carbon-components-angular";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {
  public user: ApplicationUser;
  private open: boolean = false;
  constructor(private _userService: UserService, private _route: ActivatedRoute) { }

  ngOnInit(): void {
    this._route.params.subscribe((params) => {
      const applicationUserId: string = params.id;

      this._userService.getUser(applicationUserId).then(user => {
        this.user = user;
      })
    });
  }

  deactivateUser(){
    this._userService.deactivateUser(this.user.id).then((res) => {

    }).catch((err) => {

    });
  }

  removeUser(){
    this._userService.deleteUser(this.user.id).then((res) => {

    }).catch((err) => {

    })
  }
}
