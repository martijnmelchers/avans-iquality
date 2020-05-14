import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { ApiService } from '@IQuality/core/services/api.service';
import { Tip } from '@IQuality/core/models/tip';
import { Router } from '@angular/router';

@Component({
  selector: 'app-addtip',
  templateUrl: './addtip.component.html',
  styleUrls: ['./addtip.component.scss']
})
export class AddTipComponent implements OnInit {

  actionTypes = ['General','Weight','BloodPressure','Cholesterol']
  constructor(private _api: ApiService, private _route: Router) { 

  }
  

  tipForm = new FormGroup({
    name: new FormControl(''),
    description: new FormControl(''),
    selectedAction: new FormControl(''),
  });

  ngOnInit(): void {
    
  }

  async onSubmit() {
    if(this.tipForm.value.selectedAction == ''){
      this.tipForm.value.selectedAction = 'General';
    }
    console.warn(this.tipForm.value);
    let data = new Tip();
    data.Name = this.tipForm.value.name;
    data.Description = this.tipForm.value.description;
    data.ActionType = this.tipForm.value.selectedAction;

    await this._api.post<Tip>('/doctor/createtip',data);
    this._route.navigateByUrl('/doctor/tips');

  }

}
