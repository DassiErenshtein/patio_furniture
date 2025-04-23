import { Component, Input, Output,EventEmitter } from '@angular/core';

@Component({
  selector: 'app-button',
  imports: [],
  templateUrl: './button.component.html',
  styleUrl: './button.component.css'
})
export class ButtonComponent {
  @Input() iconName?:string
  @Input() text?:string
  @Output() myEvent=new EventEmitter();
  //מפעילה את האירוע
  emitEvent(){
    this.myEvent.emit()
  }
}
