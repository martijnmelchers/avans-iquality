import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from "@angular/forms";
import { ChatService } from "@IQuality/core/services/chat.service";
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: 'app-chat-instance',
  templateUrl: './chat-instance.component.html',
  styleUrls: ['./chat-instance.component.scss']
})
export class ChatInstanceComponent implements OnInit, AfterViewInit {
  public messageFormGroup: FormGroup;
  public messageControl: FormControl;

  public scrollHeight: number;

  @ViewChild('chatScroll') public chatScrollContainer: ElementRef;




  constructor(private formBuilder: FormBuilder, public chatService: ChatService, private route: ActivatedRoute) {
  }

  async ngOnInit() {

    this.scrollHeight = 0;

    this.route.params.subscribe(async (params) => {
      let chatId = params.chatId;
      await this.initializeChat(chatId);
    });

    this.messageControl = new FormControl();
    this.messageFormGroup = this.formBuilder.group({
      message: this.messageControl,
    });
  }

  async initializeChat(chatId: string) {
    await this.chatService.selectChatWithId(chatId);
    this.chatService.messageSubject.subscribe(() => {
      setTimeout(() => this.scrollToBottom(), 100);
    });
  }

  private scrollToBottom() {
    this.chatScrollContainer.nativeElement.scrollIntoView({ behavior: "smooth", block: "end", inline: "nearest" })
  }

  async onSubmit(e): Promise<void> {
    const message = this.messageFormGroup.getRawValue().message;
    if (!message || message === "") return;
    await this.chatService.sendMessage(message);

    this.messageControl.setValue("");
    if (e) {
      e.preventDefault();
    }
  }

  ngAfterViewInit(): void {
    setTimeout(() => this.scrollToBottom(), 100);
  }
}
