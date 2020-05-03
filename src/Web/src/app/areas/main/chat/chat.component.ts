import {AfterViewChecked, AfterViewInit, Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {FormBuilder, FormControl, FormGroup} from "@angular/forms";
import {ChatService} from "@IQuality/core/services/chat.service";
import { ApiService } from '@IQuality/core/services/api.service';
import { Reminder } from '@IQuality/core/models/reminder';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent {
  public messageFormGroup: FormGroup;
  public messageControl: FormControl;
  @ViewChild('chatScroll') private chatScrollContainer: ElementRef;

  constructor(private formBuilder: FormBuilder, public chatService: ChatService,
              private apiService: ApiService) {

    this.messageControl = new FormControl();
    this.messageFormGroup = this.formBuilder.group({
      message: this.messageControl,
    });

    this.chatService.onChatSelected.push(() => this.onChatSelected());
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

  private initializeScrollContainer() {

    if (!this.chatScrollContainer) return;

    const scrollTop = this.chatScrollContainer.nativeElement.scrollTop;
    const scrollHeight = this.chatScrollContainer.nativeElement.scrollHeight;

    if (scrollTop !== scrollHeight) {
      this.chatScrollContainer.nativeElement.scrollTop = this.chatScrollContainer.nativeElement.scrollHeight;
    }
  }

  public onChatLoaded() {
    this.initializeScrollContainer();
  }

  public async onChatToggle(chatWithBot: boolean) {
    this.chatService.messages = [];
    if (!chatWithBot) {
      this.chatService.messages = this.chatService.databaseMessages;
    }

    let remindersOfToday = await this.retrieveRemindersOfToday();
    remindersOfToday.forEach(element => {
      this.chatService.customBotMessage(element.actionDescription);
    })

    let allRemindersOfToday = await this.retrieveAllRemindersOfToday();
  }

  public closeChat() {
    this.chatService.selected = undefined;
  }

  public async retrieveRemindersOfToday(): Promise<Reminder[]> {
    return await this.apiService.get<Reminder[]>('/reminder/todaysreminders');
  }

  public async retrieveAllRemindersOfToday(): Promise<Reminder[]> {
    return await this.apiService.get<Reminder[]>('/reminder/alloftodaysreminders');
  }
}
