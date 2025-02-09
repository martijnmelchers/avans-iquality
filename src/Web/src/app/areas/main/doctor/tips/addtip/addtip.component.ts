import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ApiService } from '@IQuality/core/services/api.service';
import { Tip } from '@IQuality/core/models/tip';
import { Router } from '@angular/router';
import { TipService } from '@IQuality/core/services/tip.service';

@Component({
  selector: 'app-addtip',
  templateUrl: './addtip.component.html',
  styleUrls: ['./addtip.component.scss']
})
export class AddTipComponent implements OnInit {

  actionTypes = [];
  constructor(private _api: ApiService, private _route: Router, private _tipService: TipService) {

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
    await this._api.get<any>('/actiontypes').then(resp => {
      this.actionTypes = resp;
    });

  }

  isInvalid(field: string) {
    return !this.tipForm.get(field).valid && this.tipForm.get(field).dirty
  }

  async onSubmit() {
    if (this.tipForm.value.selectedAction == '') {
      this.tipForm.value.selectedAction = this.actionTypes[0];
    }
    let data = new Tip();
    data.Name = this.tipForm.value.name;
    data.Description = this.tipForm.value.description;
    data.ActionType = this.tipForm.value.selectedAction;

    await this._tipService.createTip(data);
    this._route.navigateByUrl('/doctor/tips');
  }

}
