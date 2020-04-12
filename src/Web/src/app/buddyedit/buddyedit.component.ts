import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from '../core/services/api.service';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-buddyedit',
  templateUrl: './buddyedit.component.html',
  styleUrls: ['./buddyedit.component.scss']
})
export class BuddyeditComponent implements OnInit {
   buddy: any;
   checkoutForm;
   groupNames;

  constructor(private route: ActivatedRoute,private api: ApiService,private formBuilder: FormBuilder, private router: Router) {
    this.checkoutForm = this.formBuilder.group({
      name: '',
      phoneNumber: '',
      groupName: ''
    });
  }

  async onSubmit(buddyData) {
    if (buddyData.name == '') {
      buddyData.name = this.buddy.name;
    }
    if (buddyData.groupName == '') {
      buddyData.groupName = this.buddy.groupName;
    }
    if (buddyData.phoneNumber == '') {
      buddyData.phoneNumber = this.buddy.phoneNumber;
    }
    buddyData.id = this.buddy.id;
    await this.api.put<string>(`/buddy/${buddyData.id}`, buddyData);
    this.router.navigate(['../../buddy/']);
  }

  async ngOnInit(): Promise<void> {
    let id = this.route.snapshot.paramMap.get('id');
    this.buddy = await this.api.get<string>(`/buddy/${id}`);

    this.groupNames = await this.api.get<string>('/buddygroup');
    if (this.groupNames.length != 0) {
      this.checkoutForm.value.groupName = this.buddy.groupName;
    }
  }

}
