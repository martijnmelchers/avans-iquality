import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ApiService } from '@IQuality/core/services/api.service';
import { Tip } from '@IQuality/core/models/tip';
import { Router } from '@angular/router';

@Component({
  selector: 'app-addtip',
  templateUrl: './addtip.component.html',
  styleUrls: ['./addtip.component.scss']
})
export class AddTipComponent implements OnInit {

  actionTypes = [];
  constructor(private _api: ApiService, private _route: Router) {

  }


  tipForm = new FormGroup({
    name: new FormControl('', [
      Validators.required,
      Validators.minLength(5),
      Validators.maxLength(20)
    ]),
    description: new FormControl('', [
      Validators.required,
      Validators.minLength(5),
      Validators.maxLength(50)
    ]),
    selectedAction: new FormControl('')
  });

  async ngOnInit(): Promise<any> {
    await this._api.get<any>('/action').then(resp => {
      this.actionTypes = resp;
    });



  }

  async onSubmit() {
    if (this.tipForm.value.selectedAction == '') {
      this.tipForm.value.selectedAction = this.actionTypes[0];
    }
    let data = new Tip();
    data.Name = this.tipForm.value.name;
    data.Description = this.tipForm.value.description;
    data.ActionType = this.tipForm.value.selectedAction;

    console.log(this.tipForm.value);
    await this._api.post<Tip>('/tip',data);
    this._route.navigateByUrl('/doctor/tips');

  }

}
