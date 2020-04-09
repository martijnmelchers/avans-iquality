import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ApiService } from 'src/app/core/services/api.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-buddygroupadd',
  templateUrl: './buddygroupadd.component.html',
  styleUrls: ['./buddygroupadd.component.scss']
})
export class BuddygroupaddComponent implements OnInit {

  checkoutForm;

  constructor(private formBuilder: FormBuilder, private api: ApiService, private router: Router) {
    this.checkoutForm = this.formBuilder.group({
      name: '',
      phoneNumber: '',
      groupName: ''
    });
  }

  async onSubmit(buddyData) {
    await this.api.post<string>('/buddy', buddyData);
  }

  ngOnInit(): void {

  }

}