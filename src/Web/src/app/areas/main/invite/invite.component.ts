import {Component, OnDestroy, OnInit} from '@angular/core';
import {Button} from "carbon-components-angular";
import {ActivatedRoute, Params} from '@angular/router';
import {Observable} from "rxjs";

@Component({
  selector: 'app-invite',
  templateUrl: './invite.component.html',
  styleUrls: ['./invite.component.scss']
})
export class InviteComponent implements OnInit, OnDestroy {
  isSend: boolean ;
  id: string;
  sub: any;
  constructor(private route: ActivatedRoute) { }

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
}
