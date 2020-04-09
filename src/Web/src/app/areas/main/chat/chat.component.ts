import {AfterViewChecked, Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {Message} from "./message/message";
import {FormBuilder, FormControl, FormGroup} from "@angular/forms";
import {ChatService} from "@IQuality/core/services/chat.service";

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit, AfterViewChecked {
  public messageFormGroup: FormGroup;
  public messageControl: FormControl;
  messages: Array<Message>;
  @ViewChild('scrollMe') private myScrollContainer: ElementRef;

  constructor(private formBuilder: FormBuilder, private chatService: ChatService) {
    this.messageControl = new FormControl();
    this.messageFormGroup = formBuilder.group({
      message: this.messageControl,
    });


    this.messages = new Array<any>();
    this.messages.push({
      string: "WOW",
      senderId: "Huseyin",
      isOtherUser: false,
    });
    this.messages.push({
      string: "Dit is een bericht",
      senderId: "Storm",
      isOtherUser: true,
    });
    this.messages.push({
      string: "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
      senderId: "Huseyin",
      isOtherUser: false,
    });
    this.messages.push({
      string: "Lorem Ipsum is simply dummy text of the printing ",
      senderId: "Storm",
      isOtherUser: true,
    });
  }

  ngOnInit(): void {
    this.chatService.onChatSelected.push(() => this.onChatSelected());
  }

  ngAfterViewChecked(): void {
    const scrollTop = this.myScrollContainer.nativeElement.scrollTop;
    const scrollHeight = this.myScrollContainer.nativeElement.scrollHeight;
    if (scrollTop !== scrollHeight) {
      this.myScrollContainer.nativeElement.scrollTop = this.myScrollContainer.nativeElement.scrollHeight;
    }
  }

  onSubmit(e): void {
    const message = this.messageFormGroup.getRawValue().message;

    this.messages.push({
      string: message,
      senderId: "huseyin",
      isOtherUser: false,
    });

    this.messageControl.setValue("");
    if (e) {
      e.preventDefault();
    }
  }

  private onChatSelected() {

  }
}
