import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from '@IQuality/core/services/api.service';
import { Tip } from '@IQuality/core/models/tip';
import { TipService } from '@IQuality/core/services/tip.service';

@Component({
  selector: 'app-manage',
  templateUrl: './manage.component.html',
  styleUrls: ['./manage.component.scss']
})
export class ManageComponent implements OnInit {

  public tip;
  public open;

  actionTypes = [];

  constructor(private _route: ActivatedRoute, private _navRoute: Router, private _api: ApiService, private _tipService: TipService) {


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
    selectedAction: new FormControl('', [
      Validators.required,
      Validators.minLength(5),
      Validators.maxLength(50)
    ])
  });

  isInvalid(field: string) {
    return !this.tipForm.get(field).valid && this.tipForm.get(field).dirty
  }


  async ngOnInit(): Promise<any> {
    let id = this._route.snapshot.paramMap.get('id');

    await this._tipService.getTipById(id).then((response) => {
      this.tip = response;
      this.tipForm.patchValue({
        name: this.tip.name,
        description: this.tip.description,
        selectedAction: this.tip.actionType
      });
    })

    await this._api.get<any>('/actiontypes').then(resp => {
      this.actionTypes = resp;
    });



  }

  async onSubmit() {
    this.tip.name = this.tipForm.value.name;
    this.tip.description = this.tipForm.value.description;
    this.tip.actionType = this.tipForm.value.selectedAction;
    let id = this._route.snapshot.paramMap.get('id');
    await this._tipService.editTip(id, this.tip);
    this._navRoute.navigateByUrl('/doctor/tips');
  }

  async deleteTip(){

    let id = this._route.snapshot.paramMap.get('id');
    await this._tipService.deleteTip(id);
    this._navRoute.navigateByUrl('/doctor/tips');

  }

  selected($event) {
    console.log($event);
  }

}
