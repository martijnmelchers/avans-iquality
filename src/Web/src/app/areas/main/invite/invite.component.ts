import {Component, OnDestroy, OnInit} from '@angular/core';
import {Button} from "carbon-components-angular";
import {ActivatedRoute, Params} from '@angular/router';
import {Observable} from "rxjs";
import {AuthenticationService} from "@IQuality/core/services/authentication.service";
import {Invite} from "@IQuality/core/models/invite";

@Component({
  selector: 'app-invite',
  templateUrl: './invite.component.html',
  styleUrls: ['./invite.component.scss']
})
export class InviteComponent implements OnInit, OnDestroy {
  isSend: boolean ;
  id: string;
  sub: any;
  role: string;
  inviteToken: string;
  invite: Invite;
  inviteTypes: Array<string> = ["Buddy", "Patient", "Doctor", "Admin"];
  constructor(private route: ActivatedRoute, private _authService: AuthenticationService ) { }
  ngOnInit(): void {
      this.sub = this.route.params.subscribe( params => {
        console.log(params);
        this.id = params['id'];
        this.isSend = this.id == null;

        if(this.id){
          this.getInvite()
        }
      });

    this.role = this._authService.getRole;
    console.log(this.role)
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  async getInvite(){
    this.invite = await this._authService.getInviteLink(this.id);
  }

  async createInvite(){
      const link = await this._authService.createInviteLink();
      this.inviteToken = `http://localhost:4200/invite/${link.token}`;
  }

  acceptInvite(){

  }

  declineInvite(){

  }
}
