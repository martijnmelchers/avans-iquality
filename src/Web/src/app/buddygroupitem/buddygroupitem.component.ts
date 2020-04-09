import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from '../core/services/api.service';

@Component({
  selector: 'app-buddygroupitem',
  templateUrl: './buddygroupitem.component.html',
  styleUrls: ['./buddygroupitem.component.scss']
})
export class BuddygroupitemComponent implements OnInit {

  groupName;
  buddies:any;

  constructor(private route: ActivatedRoute, private api: ApiService) { }

 async ngOnInit(): Promise<void> {
    this.groupName = this.route.snapshot.paramMap.get('id');
    this.buddies = await this.api.get<string>(`/buddygroup/${this.groupName}`);

  }

}
