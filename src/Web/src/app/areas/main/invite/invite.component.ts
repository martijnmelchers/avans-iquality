import {Component, OnDestroy, OnInit} from '@angular/core';
import {Button} from 'carbon-components-angular';
import {ActivatedRoute, Params, Router} from '@angular/router';
import {Observable} from 'rxjs';
import {AuthenticationService} from '@IQuality/core/services/authentication.service';
import {Invite} from '@IQuality/core/models/invite';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ChatContext} from '@IQuality/core/models/chat-context';
import {ChatService} from '@IQuality/core/services/chat.service';

@Component({
  selector: 'app-invite',
  templateUrl: './invite.component.html',
  styleUrls: ['./invite.component.scss']
})
export class InviteComponent implements OnInit, OnDestroy {
  isSend: boolean ;
  id: string;
  sub: any;
  role: string;
  inviteToken: string;
  invite: Invite;
  inviteTypes: Array<string> = ['buddy', 'patient', 'doctor', 'admin'];
  chatId: string;
  success = null;
  errorMessage = 'There was an unknown error, please try again at a later time';
  form: FormGroup;

  constructor(
    private _route: ActivatedRoute,
    private _authService: AuthenticationService,
    private _router: Router,
    private _fb: FormBuilder,
    private _chatService: ChatService ) { }



    async ngOnInit() {
      this.role = this._authService.getRole;
      this.sub = this._route.params.subscribe( async params =>  {
          this.id = params.id;
          this.isSend = this.id == null;

          if (this.id) {
            this.invite = await this._authService.getInviteLink(this.id);
            console.log(this.invite);
          }
    });

      this._route.params.subscribe((params) => {
      if (this._authService.getRole === 'admin') {
        this.form = this._fb.group({
          email: ['', {
            validators: [Validators.required, Validators.email],
            updateOn: 'blur'
          }],
        });
      } else {
        if (params.chatId) {
          const chatId: string = params.chatId;
          this.chatId = chatId;


          // Initialize the form
          this.form = this._fb.group({
            email: ['', {
              validators: [Validators.required, Validators.email],
              updateOn: 'blur'
            }],
          });
        } else {
          // Initialize the form
          this.form = this._fb.group({
            email: ['', {
              validators: [Validators.required, Validators.email],
              updateOn: 'blur'
            }],
            chatName: ['',  {
              validators: [Validators.required, Validators.minLength(6)],
              updateOn: 'blur'
            }],
          });
        }
      }
    });

      this.role = this._authService.getRole;

  }

  async submit() {
    const values = this.form.getRawValue();

    let isBuddyChat = false;

    if (this._authService.getRole === 'patient') {
      isBuddyChat = true;
    }

    const chats = await this._chatService.getChats();
    const chat = chats.find((chatItem) => chatItem.chat.name === values.chatName);

    let chatId: string;
    if (this.chatId) {
      chatId = this.chatId;
    }
    if (this._authService.getRole === 'admin') {
      chatId = null;
    }

    try {
      if (this.chatId) {
        values.chatName = null;
      }


      const link = await this._authService.createInviteLink(chatId, values.email, values.chatName);
      this.inviteToken = `http://localhost:4200/invite/${link.token}`;
      this.success = true;

    } catch (e) {
      this.success = false;
      this.errorMessage = e.error;
    }
  }


  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  acceptInvite() {
    this._router.navigate(['/authenticate', 'register', this.id]);
  }

  declineInvite() {
    this._router.navigate(['/']);
  }

  isInvalid(field: string) {
    return !this.form.get(field).valid && this.form.get(field).dirty;
  }

}
