import { Component, OnInit } from '@angular/core';
import {AbstractControl, FormBuilder, FormGroup, ValidationErrors, Validators} from "@angular/forms";
import {ApiService} from "@IQuality/core/services/api.service";
import {AuthenticationService} from "@IQuality/core/services/authentication.service";
import {ActivatedRoute, Router} from "@angular/router";
import {Invite} from "@IQuality/core/models/invite";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  public form: FormGroup;
  public hasError: boolean = false;
  private link: Invite;
  private inviteTypes: Array<string> = ["buddy", "patient", "doctor", "admin"];
  private inviteToken: string;
  constructor(private _route: ActivatedRoute, private _fb: FormBuilder, private _api: ApiService, private _auth: AuthenticationService, private _router: Router) {
  }


  ngOnInit(): void {
    this._route.params.subscribe(async (params) => {
      const inviteToken = params.inviteToken;
      this.link = await this._auth.getInviteLink(inviteToken);
      this.inviteToken = inviteToken;
    });

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

      confirmPassword: ['', {
        validators: [Validators.required, RegisterComponent.matchValues('password')]
      }]
    });
  }

  public static matchValues(
    matchTo: string // name of the control to match to
  ): (AbstractControl) => ValidationErrors | null {
    return (control: AbstractControl): ValidationErrors | null => {
      return !!control.parent &&
      !!control.parent.value &&
      control.value === control.parent.controls[matchTo].value
        ? null
        : { isMatching: false };
    };
  }


  isInvalid(field: string) {
    return !this.form.get(field).valid && this.form.get(field).dirty
  }

  async submit() {
    const registerType = this.inviteTypes[this.link.inviteType];
    try {
      let userData: any = await this._api.post<any>(`/authorize/register/${registerType}/${this.inviteToken}`, this.form.getRawValue(), null, { disableAuthentication: true, responseType: 'text', headers: {} });
      userData = JSON.parse(userData);
      await this._router.navigate([`/authenticate`], {queryParams: {chat_id: userData.item1}});
    } catch(e) {
      console.log(e);
      this.hasError = true;
    }
  }

}
