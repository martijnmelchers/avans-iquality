import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { ApiService } from '@IQuality/core/services/api.service';

@Component({
  selector: 'app-buddygroupadd',
  templateUrl: './buddygroupadd.component.html',
  styleUrls: ['./buddygroupadd.component.scss']
})
export class BuddygroupaddComponent implements OnInit {

  checkoutForm;
  groupNames;

  constructor(private formBuilder: FormBuilder, private api: ApiService, private router: Router) {
    this.checkoutForm = this.formBuilder.group({
      name: '',
      phoneNumber: '',
      groupName: '',
      selectedValue: '',
      newGroupName: ''
    });
  }

  async onSubmit(buddyData) {
    
    let groupNameOutput;
    this.groupNames.forEach(element => {
      element = element.toLowerCase();
    });
    if(buddyData.newGroupName != '' && !this.groupNames.includes(buddyData.newGroupName.toLowerCase())) {
      groupNameOutput = buddyData.newGroupName;
    }else if(buddyData.selectedValue != '') {
      groupNameOutput = buddyData.selectedValue;
    } else if (buddyData.selectedValue == '' && this.groupNames.length != 0) {
      groupNameOutput = this.groupNames[0];
    } else {
      return;
    }

    let sendBuddy = {
      name: buddyData.name,
      phoneNumber: buddyData.phoneNumber,
      groupName : groupNameOutput
    }

    await this.api.post<string>('/buddy', sendBuddy);
    this.router.navigate(['../../buddy/']);
  }

  async ngOnInit(): Promise<void> {
    this.groupNames = await this.api.get<string>('/buddygroup');
    if (this.groupNames.length != 0) {
      this.checkoutForm.value.selectedValue = this.groupNames[0];
    }
  }
}
