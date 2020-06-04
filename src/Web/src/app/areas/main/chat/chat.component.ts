import {AfterViewChecked, AfterViewInit, Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {FormBuilder, FormControl, FormGroup} from "@angular/forms";
import {ChatService} from "@IQuality/core/services/chat.service";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit {


  constructor(private formBuilder: FormBuilder, public chatService: ChatService, private route: ActivatedRoute) {
    this.chatService.onChatSelected.push(() => this.onChatSelected());
  }

  ngOnInit(){
    this.route.params.subscribe((params) => {
      let chatName = params.chatName;
      console.log("Connected to:" + chatName);
    });

    this.chatService.connectWithChats()
  }



  private onChatSelected() {
  }
}
