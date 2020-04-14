import {AfterViewChecked, Component, ElementRef, OnInit, ViewChild} from '@angular/core';
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
  @ViewChild('chatScroll') private chatScrollContainer: ElementRef;

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

  }

  onSubmit(e): void {
    const message = this.messageFormGroup.getRawValue().message;
    if (!message || message === "") return;

    this.chatService.sendMessage(message);

    this.messageControl.setValue("");
    if (e) {
      e.preventDefault();
    }
  }

  private onChatSelected() {

  }

  private initializeScrollContainer(){

    if(!this.chatScrollContainer) return;

    const scrollTop = this.chatScrollContainer.nativeElement.scrollTop;
    const scrollHeight = this.chatScrollContainer.nativeElement.scrollHeight;

    if (scrollTop !== scrollHeight) {
      this.chatScrollContainer.nativeElement.scrollTop = this.chatScrollContainer.nativeElement.scrollHeight;
    }
  }

  public onChatLoaded() {
    this.initializeScrollContainer();
  }

  public closeChat() {
    this.chatService.selected = undefined;
  }
}
