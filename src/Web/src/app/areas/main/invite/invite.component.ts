import {Component, OnDestroy, OnInit} from '@angular/core';
import {Button} from "carbon-components-angular";
import {ActivatedRoute, Params} from '@angular/router';
import {Observable} from "rxjs";
import {AuthenticationService} from "@IQuality/core/services/authentication.service";

@Component({
  selector: 'app-invite',
  templateUrl: './invite.component.html',
  styleUrls: ['./invite.component.scss']
})
export class InviteComponent implements OnInit, OnDestroy {
  isSend: boolean ;
  id: string;
  sub: any;
  constructor(private route: ActivatedRoute, private _authService: AuthenticationService ) { }

  ngOnInit(): void {
      this.sub = this.route.params.subscribe( params => {
        console.log(params);
        this.id = params['id'];
        this.isSend = this.id == null;
      });
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  async createInvite(){
      const link = await this._authService.createInviteLink();
      console.log(link);
  }
}
