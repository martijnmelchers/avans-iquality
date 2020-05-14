import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { ApiService } from '@IQuality/core/services/api.service';

@Component({
  selector: 'app-addtip',
  templateUrl: './addtip.component.html',
  styleUrls: ['./addtip.component.scss']
})
export class AddTipComponent implements OnInit {

  actionTypes = ['General','Weight','Blood Pressure','Cholesterol']
  constructor(private api: ApiService) { }

  profileForm = new FormGroup({
    name: new FormControl(''),
    description: new FormControl(''),
    selectedAction: new FormControl(''),
  });

  ngOnInit(): void {
  }

  onSubmit() {
    // TODO: Use EventEmitter with form value
    console.warn(this.profileForm.value);
  }

}
