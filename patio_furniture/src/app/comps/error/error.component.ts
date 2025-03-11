import { Component } from '@angular/core';

@Component({
  selector: 'app-error',
  imports: [],
  templateUrl: './error.component.html',
  styleUrl: './error.component.css'
})
export class ErrorComponent {
  //לנסות שוב פעם, שהלקוח לא יתייאש. אולי זו תקלה זמנית
  reloadPage(): void {
    window.location.reload();
  }
}
