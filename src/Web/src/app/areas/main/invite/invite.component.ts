import {Component, OnDestroy, OnInit} from '@angular/core';
import {Button} from "carbon-components-angular";
import {ActivatedRoute, Params, Router} from '@angular/router';
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
  chatId: string;
  constructor(private route: ActivatedRoute, private _authService: AuthenticationService, private router: Router ) { }
  ngOnInit(): void {
      this.sub = this.route.params.subscribe( params => {
        console.log(params);
        this.id = params['id'];
        this.isSend = this.id == null;

        if(this.id){
          this.getInvite()
        }
      });

    this.route.params.subscribe((params) => {
      if(params.chatId){
        const chatId: string = params.chatId;
        this.chatId = chatId;
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
      const link = await this._authService.createInviteLink(this.chatId);
      this.inviteToken = `http://localhost:4200/invite/${link.token}`;
  }

  acceptInvite(){
    this.router.navigate(['/authenticate', 'register', this.id]);
  }

  declineInvite(){
    this.router.navigate(['/']);
  }
}
