import {AfterViewChecked, Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {FormBuilder, FormControl, FormGroup} from "@angular/forms";
import {ChatService} from "@IQuality/core/services/chat.service";
import {Message} from "@IQuality/core/models/message";

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit, AfterViewChecked {
  public messageFormGroup: FormGroup;
  public messageControl: FormControl;
  @ViewChild('scrollMe') private myScrollContainer: ElementRef;

  constructor(private formBuilder: FormBuilder, public chatService: ChatService) {
    this.messageControl = new FormControl();
    this.messageFormGroup = formBuilder.group({
      message: this.messageControl,
    });
  }

  ngOnInit(): void {
    this.chatService.onChatSelected.push(() => this.onChatSelected());
  }

  ngAfterViewChecked(): void {
    const scrollTop
      = this.myScrollContainer.nativeElement.scrollTop;
    const scrollHeight = this.myScrollContainer.nativeElement.scrollHeight;
    if (scrollTop !== scrollHeight) {
      this.myScrollContainer.nativeElement.scrollTop = this.myScrollContainer.nativeElement.scrollHeight;
    }
  }


  onSubmit(e): void {
    const message = this.messageFormGroup.getRawValue().message;
    this.chatService.sendMessage(message);

    this.messageControl.setValue("");
    if (e) {
      e.preventDefault();
    }
  }

  private onChatSelected() {

  }
}
