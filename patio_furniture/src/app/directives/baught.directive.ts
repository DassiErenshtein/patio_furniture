import { Directive, ElementRef, HostListener, Input } from '@angular/core';

@Directive({
  selector: '[appBaught]'
})
export class BaughtDirective {
  @Input() appBaught:number=1
  //בעת לחיצה, שם את המספר שקבל מהתגית בתוך התגית שקבל
  @HostListener('click') onClick() {
    this.er.nativeElement.innerText=this.appBaught;
  }
  constructor(public er:ElementRef) { }

}
