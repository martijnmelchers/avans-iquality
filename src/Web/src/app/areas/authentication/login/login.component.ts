import { Component, OnInit } from '@angular/core';
import { RequestStatusService } from "@IQuality/core/services/request-status.service";
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { ApiService } from "@IQuality/core/services/api.service";
import { AuthenticationService } from "@IQuality/core/services/authentication.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public form: FormGroup;
  public hasError: boolean = false;

  constructor(private _fb: FormBuilder, private _api: ApiService, private _auth: AuthenticationService) {
  }

  ngOnInit(): void {

    // Initialize the form
    this.form = this._fb.group({
      email: ['', {
        validators: [Validators.required, Validators.email],
        updateOn: 'blur'
      }],
      password: ['',  {
        validators: [Validators.required, Validators.minLength(6)],
        updateOn: 'blur'
      }],
      rememberMe: [false]
    });
  }

  isInvalid(field: string) {
    return !this.form.get(field).valid && this.form.get(field).dirty
  }

  async submit() {
    try {
      const token = await this._api.post<string>('/authorize/login', this.form.getRawValue(), null, { disableAuthentication: true, responseType: 'text', headers: {} });
      this._auth.saveToken(token);

      console.log(await this._api.post<string>('/authorize/invite', null, null, { responseType: 'text', headers: {} }));
    } catch(e) {
      console.log(e);
      this.hasError = true;
    }
  }
}
