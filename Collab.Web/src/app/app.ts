import { Component, ElementRef, signal, viewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';
import * as signalR from "@microsoft/signalr";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, FormsModule],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})

export class App {
  protected readonly title = signal('Collab.Web');
  tbMessage = "";
  divMsgs = viewChild<ElementRef>('msgs');
  btnSend = viewChild<ElementRef>('btnSend');

  ngAfterViewInit() {
    const username = new Date().getTime();

    if (!this.divMsgs() || !this.btnSend()) return;

    const connection = new signalR.HubConnectionBuilder()
        .withUrl("https://localhost:7411/hub")
        .build();

    connection.on("new-message", (username: string, message: string) => {
      const m = document.createElement("div");

      m.innerHTML = `<div class="message-author">${username}</div><div>${message}</div>`;

      this.divMsgs()?.nativeElement.appendChild(m);
      this.divMsgs()!.nativeElement.scrollTop = this.divMsgs()!.nativeElement.scrollHeight;
    });

    connection.start().catch((err) => document.write(err));

    const send = () => {
      if (!this.tbMessage) return;
      connection.invoke("sendMessage", username.toString(), this.tbMessage)
        .then(() => {
          console.log(username, this.tbMessage);
          this.tbMessage = "";
        }).catch(console.error);
    }

    this.btnSend()?.nativeElement.addEventListener("click", send);
  }
}
