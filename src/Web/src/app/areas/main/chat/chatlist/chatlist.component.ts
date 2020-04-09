import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-chatlist',
  templateUrl: './chatlist.component.html',
  styleUrls: ['./chatlist.component.scss']
})
export class ChatlistComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  selected($event: { value: string; selected: boolean; name: string }) {
    console.log($event);
  }

  onChatCreate() {

  }
}
