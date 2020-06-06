import { Component, OnInit } from '@angular/core';
import { UserService } from "@IQuality/core/services/user.service";
import { ApplicationUser } from "@IQuality/core/models/application-user";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {
  public user: ApplicationUser;
  private open: boolean = false;
  success: boolean;
  successMessage: string;

  constructor(private _userService: UserService, private _route: ActivatedRoute, private _router: Router) {
  }

  ngOnInit(): void {
    this._route.params.subscribe((params) => {
      const applicationUserId: string = params.id;

      this._userService.getUser(applicationUserId).then(user => {
        this.user = user;
      })
    });
  }

  deactivateUser() {
    this._userService.deactivateUser(this.user.id).then((res) => {
      this.successMessage = "User has been deactivated and cannot login anymore. Contact a system administrator to reactivate."
      this.success = true;


    }).catch((err) => {
    });
  }

  async removeUser() {
    this._userService.deleteUser(this.user.id).then(async (res) => {
      this.successMessage = "User has been deleted, you will be redirected.";
      this.success = true;

      await this._router.navigate(['/admin', 'users']);
    }).catch((err) => {

    })
  }
}
