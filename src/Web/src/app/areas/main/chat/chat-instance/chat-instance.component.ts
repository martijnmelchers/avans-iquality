import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {FormBuilder, FormControl, FormGroup} from "@angular/forms";
import {ChatService} from "@IQuality/core/services/chat.service";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-chat-instance',
  templateUrl: './chat-instance.component.html',
  styleUrls: ['./chat-instance.component.scss']
})
export class ChatInstanceComponent implements OnInit {
  public messageFormGroup: FormGroup;
  public messageControl: FormControl;

  public scrollHeight: number;

  @ViewChild('chatScroll') public chatScrollContainer: ElementRef;
  constructor(private formBuilder: FormBuilder, public chatService: ChatService, private route: ActivatedRoute) { }

  ngOnInit(): void {

    this.scrollHeight = 0;

    this.route.params.subscribe((params) => {
      let chatId = params.chatId;
      this.initializeChat(chatId);
    });

    this.messageControl = new FormControl();
    this.messageFormGroup = this.formBuilder.group({
      message: this.messageControl,
    });
  }

  initializeChat(chatId: string){
    this.chatService.selectChatWithId(chatId).then(r => console.log(r));
  }

  onSubmit(e): void {
    const message = this.messageFormGroup.getRawValue().message;
    if (!message || message === "") return;

    this.chatService.sendMessage(message).then(() =>{
      this.chatScrollContainer.nativeElement.scrollIntoView({behavior: "smooth", block: "end", inline: "nearest"});
    });

    this.messageControl.setValue("");
    if (e) {
      e.preventDefault();
    }
  }

  public onChatToggle(chatWithBot: boolean) {
  }
}
