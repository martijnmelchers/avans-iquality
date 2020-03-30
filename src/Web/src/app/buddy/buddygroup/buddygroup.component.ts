import { Component, OnInit } from '@angular/core';
import { ApiService } from 'src/app/core/services/api.service';

@Component({
  selector: 'app-buddygroup',
  templateUrl: './buddygroup.component.html',
  styleUrls: ['./buddygroup.component.scss']
})
export class BuddygroupComponent implements OnInit {

  buddies:any;
  constructor(private api: ApiService) {

  }

  async ngOnInit(): Promise<void> {
    this.buddies = await this.api.get<string>('/api/buddygroup/index');
    console.log(this.buddies);
  }
}




