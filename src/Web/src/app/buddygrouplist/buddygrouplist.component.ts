import { Component, OnInit } from '@angular/core';
import { ApiService } from '../core/services/api.service';

@Component({
  selector: 'app-buddygrouplist',
  templateUrl: './buddygrouplist.component.html',
  styleUrls: ['./buddygrouplist.component.scss']
})
export class BuddygrouplistComponent implements OnInit {

  buddygroups: any;
  constructor(private api: ApiService) { }

   async ngOnInit(): Promise<void> {
     this.buddygroups = await this.api.get<string>(`/buddygroup`);
     console.log(this.buddygroups);
  }

}
