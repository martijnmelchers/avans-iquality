import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from '@IQuality/core/services/api.service';
import { Tip } from '@IQuality/core/models/tip';

@Component({
  selector: 'app-manage',
  templateUrl: './manage.component.html',
  styleUrls: ['./manage.component.scss']
})
export class ManageComponent implements OnInit {

  private tip = {};
  constructor(private _route: ActivatedRoute, private _api: ApiService) { }

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
    await this._api.get<Tip>('/doctor/gettip').then(resp => {
      this.tip = resp;
    });
  }

  async onSubmit(){

  }

}
