import { Component, OnInit } from '@angular/core';
import {UserService} from "@IQuality/core/services/user.service";
import {ApplicationUser} from "@IQuality/core/models/application-user";
import {TableHeaderItem, TableItem, TableModel} from "carbon-components-angular";

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {
  public users: Array<ApplicationUser>;
  constructor(private _userService: UserService) { }

  ngOnInit(): void {
    this._userService.getUsers().then(users => {
      this.users = users;
    })
  }

  //TODO: Get all users.
  //TODO: Select user.
  //TODO: Delete user.
}
