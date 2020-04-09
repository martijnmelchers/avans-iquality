import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-buddygrouplist',
  templateUrl: './buddygrouplist.component.html',
  styleUrls: ['./buddygrouplist.component.scss']
})
export class BuddygrouplistComponent implements OnInit {

  buddiegroups = [];
  constructor() { }

   asnyc ngOnInit(): Promise<void> {
  }

}
