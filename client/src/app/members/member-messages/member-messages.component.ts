import { Component, inject, input, OnInit, output, ViewChild } from '@angular/core';
import { MessageService } from "../../_services/message.service";
import { Message } from "../../_models/messages";
import { TimeagoModule } from "ngx-timeago";
import { HttpClient } from "@angular/common/http";
import { MessagesComponent } from "../../messages/messages.component";
import { FormsModule, NgForm } from "@angular/forms";

@Component({
  selector: 'app-member-messages',
  standalone: true,
  imports: [
    TimeagoModule,
    FormsModule
  ],
  templateUrl: './member-messages.component.html',
  styleUrl: './member-messages.component.css'
})
export class MemberMessagesComponent {

  @ViewChild('messageForm') messageForm?: NgForm;
  username = input.required<string>();
  private messageService = inject(MessageService);
  messages = input.required<Message[]>();
  messageContent  = '';
  updateMessages = output<Message>();

  sendMessage() {
    this.messageService.sendMessage(this.username(), this.messageContent).subscribe({
      next: message => {
        this.updateMessages.emit(message);
        this.messageForm?.reset();
      }
    })
  }

}