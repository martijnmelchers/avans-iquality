import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { ApiService } from "@IQuality/core/services/api.service";
import { AuthenticationService } from "@IQuality/core/services/authentication.service";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public form: FormGroup;
  public hasError: boolean = false;
  private chatId: string;

  constructor(private _fb: FormBuilder, private _api: ApiService, private _auth: AuthenticationService, private _router: Router, private _route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this._route.queryParams.subscribe((params) => {
      this.chatId = params.chat_id
    });

    // Initialize the form
    this.form = this._fb.group({
      email: ['', {
        validators: [Validators.required, Validators.email],
        updateOn: 'blur'
      }],
      password: ['', {
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
      const token = await this._api.post<string>('/authorize/login', this.form.getRawValue(), null, {
        disableAuthentication: true,
        responseType: 'text',
        headers: {}
      });
      this._auth.saveToken(token);
      this.navigateToUserPage()
    } catch (e) {
      this.hasError = true;
    }
  }

  //Decide which page belongs to the current role and navigate to the page.
  navigateToUserPage() {
    const role = this._auth.getRole;

    let route = "/chat";
    if (role == "Doctor" || role == "Admin") {
      route = `/${role.toLowerCase()}`
    }

    if (this.chatId) {
      this._router.navigate(['/chat', this.chatId])
      return;
    }

    this._router.navigate([route]);
  }
}
