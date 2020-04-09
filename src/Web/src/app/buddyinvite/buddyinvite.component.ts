import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ApiService } from '../core/services/api.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-buddyinvite',
  templateUrl: './buddyinvite.component.html',
  styleUrls: ['./buddyinvite.component.scss']
})
export class BuddyinviteComponent implements OnInit {
  inviteForm;

  constructor(private formBuilder: FormBuilder, private api: ApiService, private router: Router) {
    this.inviteForm = this.formBuilder.group({
      name: '',
      phoneNumber: '',
      group: ''
    });

  }

  ngOnInit(): void {
  }


  async submit(buddyData){

  }

}
