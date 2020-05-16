import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from '@IQuality/core/services/api.service';
import { Tip } from '@IQuality/core/models/tip';

@Component({
  selector: 'app-manage',
  templateUrl: './manage.component.html',
  styleUrls: ['./manage.component.scss']
})
export class ManageComponent implements OnInit {

  public tip;

  constructor(private _route: ActivatedRoute, private _navRoute: Router, private _api: ApiService) {


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
    ])
  });


  async ngOnInit(): Promise<any> {
    let id = this._route.snapshot.paramMap.get('id');

    await this._api.get<any>(`/doctor/gettipbyid/${id}`).then(resp => {
      this.tip = resp;
      this.tipForm.patchValue({
        name: this.tip.name,
        description: this.tip.description
      })
    });
    console.log(this.tip);



  }

  async onSubmit() {    
    let id = this._route.snapshot.paramMap.get('id');
    await this._api.put(`/doctor/edit/${id}`,this.tip);
    this._navRoute.navigateByUrl('/doctor/tips');
  }

  async deleteTip(){
    
    let id = this._route.snapshot.paramMap.get('id');
    await this._api.delete(`/doctor/delete/${id}`);
    this._navRoute.navigateByUrl('/doctor/tips');
    
  }

}
