import { Component, OnInit } from '@angular/core';
import { ApiService } from 'src/app/core/services/api.service';

@Component({
  selector: 'app-buddygroup',
  templateUrl: './buddygroup.component.html',
  styleUrls: ['./buddygroup.component.scss']
})
export class BuddygroupComponent implements OnInit {

  buddies = [];
  constructor(private api: ApiService) {
    this.buddies = [{
      id: 1,
      groupId: 0,
      name: 'Masud',
      phoneNumber: '0031613579246',
      imagePath: 'coole-foto-masud.jpg'
    },
    {
      id: 2,
      groupId: 0,
      name: 'Darjush',
      phoneNumber: '0031624680135',
      imagePath: 'coole-foto-darjush.jpg'
    }]
  }

  ngOnInit(): void {
    throw new Error("Method not implemented.");
  }
}




