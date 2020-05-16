import { Component, OnInit } from '@angular/core';
import {ModalService, NotificationService, PlaceholderService} from "carbon-components-angular";
import {ChatService} from "@IQuality/core/services/chat.service";

@Component({
  selector: 'app-doctor',
  templateUrl: './doctor.component.html',
  styleUrls: ['./doctor.component.scss'],
  providers: [ModalService, PlaceholderService]
})
export class DoctorComponent implements OnInit {

  constructor(private _notificationService: NotificationService, private _chatService: ChatService) { }

  ngOnInit(): void {
    this._chatService.getChatObservable().subscribe((message) => {
      console.log(message);
      this._notificationService.showToast({
        type: "info",
        title: message.chatName,
        subtitle: message.userName,
        caption: message.content,
        target: ".notification-container",
        duration: 2000
      });
    });
  }

  invitePatient(): void {

  }
}
