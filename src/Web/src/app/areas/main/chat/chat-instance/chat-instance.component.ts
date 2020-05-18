import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from "@angular/forms";
import { ChatService } from "@IQuality/core/services/chat.service";
import { ActivatedRoute } from "@angular/router";
import { Location } from '@angular/common';

@Component({
  selector: 'app-chat-instance',
  templateUrl: './chat-instance.component.html',
  styleUrls: ['./chat-instance.component.scss']
})
export class ChatInstanceComponent implements OnInit, AfterViewInit {
  public messageFormGroup: FormGroup;
  public messageControl: FormControl;

  public scrollHeight: number;
  public botIsTyping: boolean;

  private chatId: string;

  @ViewChild('chatScroll', { static: false }) public chatScrollContainer: ElementRef;

  constructor(private formBuilder: FormBuilder, public chatService: ChatService, private route: ActivatedRoute, private _location: Location) { }

  async ngOnInit() {
    this.route.params.subscribe(async (params) => {
      this.chatId = params.chatId;
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

    this.botIsTyping = this.chatService.chatWithBot;

    this.messageControl.setValue("");
    await this.chatService.sendMessage(message);

    this.botIsTyping = false;

    if (e) {
      e.preventDefault();
    }
  }

  back(): void{
    this._location.back();
  }

  async ngAfterViewInit() {
    await this.initializeChat(this.chatId);
    setTimeout(() => this.scrollToBottom(), 100);
  }
}
