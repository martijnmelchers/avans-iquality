import {Component, OnDestroy, OnInit} from '@angular/core';
import {Button} from "carbon-components-angular";
import {ActivatedRoute, Params, Router} from '@angular/router';
import {Observable} from "rxjs";
import {AuthenticationService} from "@IQuality/core/services/authentication.service";
import {Invite} from "@IQuality/core/models/invite";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ChatContext} from "@IQuality/core/models/chat-context";
import {ChatService} from "@IQuality/core/services/chat.service";

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
  inviteTypes: Array<string> = ["Buddy", "Patient", "Doctor", "Admin"];
  chatId: string;
  success: boolean = false;
  form: FormGroup;
  constructor(private route: ActivatedRoute, private _authService: AuthenticationService, private router: Router, private _fb: FormBuilder, private _chatService: ChatService ) { }
  ngOnInit(): void {
    this.role = this._authService.getRole;
      this.sub = this.route.params.subscribe( params => {
        this.id = params['id'];
        this.isSend = this.id == null;

        if(this.id){
          this.getInvite()
        }
      });

    this.route.params.subscribe((params) => {
      if(this._authService.getRole == "admin"){
        this.form = this._fb.group({
          email: ['', {
            validators: [Validators.required, Validators.email],
            updateOn: 'blur'
          }],
        });
      }
      else{
        if(params.chatId){
          const chatId: string = params.chatId;
          this.chatId = chatId;


          // Initialize the form
          this.form = this._fb.group({
            email: ['', {
              validators: [Validators.required, Validators.email],
              updateOn: 'blur'
            }],
          });
        }
        else{
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

  async submit(){
    const values = this.form.getRawValue();

    let isBuddyChat = false;

    if(this._authService.getRole == "patient"){
      isBuddyChat = true;
    }

    const chats = await  this._chatService.getChats();
    let chat = chats.find((chat) => chat.chat.name == values.chatName);
    if(chat && chat.chat.initiatorId != 'ApplicationUsers-admin@iquality.nl'){
    }
    else{

      let chatId: string;
      if(this.chatId){
        chatId = this.chatId;
      }
      else{
        chatId = (await this._chatService.createBuddychat(values.chatName, isBuddyChat)).chat.id;
      }

      if(this._authService.getRole == "admin")
          chatId = null;

      let link = await this._authService.createInviteLink(chatId, values.email);
      this.inviteToken = `http://localhost:4200/invite/${link.token}`;

      this.success = true;
    }
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  async getInvite(){
    this.invite = await this._authService.getInviteLink(this.id);
  }


  acceptInvite(){
    this.router.navigate(['/authenticate', 'register', this.id]);
  }

  declineInvite(){
    this.router.navigate(['/']);
  }

  isInvalid(field: string) {
    return !this.form.get(field).valid && this.form.get(field).dirty
  }

}
