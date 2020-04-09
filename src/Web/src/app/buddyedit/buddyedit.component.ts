import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from '../core/services/api.service';

@Component({
  selector: 'app-buddyedit',
  templateUrl: './buddyedit.component.html',
  styleUrls: ['./buddyedit.component.scss']
})
export class BuddyeditComponent implements OnInit {
   buddy:any;

  constructor(private route: ActivatedRoute,private api: ApiService) { }

  async ngOnInit(): Promise<void> {
    let id = this.route.snapshot.paramMap.get('id');
    this.buddy = await this.api.get<string>(`buddy/${id}`);
    console.log(this.buddy);
  }

}
