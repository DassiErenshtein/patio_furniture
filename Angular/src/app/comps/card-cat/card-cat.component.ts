import { Component, Input, Output, output, EventEmitter } from '@angular/core';
import { Category } from '../../classes/Category';
import { Router, RouterLink } from '@angular/router';
@Component({
  selector: 'app-card-cat',
  imports: [RouterLink],
  templateUrl: './card-cat.component.html',
  styleUrls: ['./card-cat.component.css']
})
export class CardCatComponent {
  @Input() nameCard?:string
  @Input() url?:string
  @Input() path?:string

  constructor(public router: Router) { }
  getImg(cat:Category){
    return 'assets/'+cat.nameC+cat.img;
  }
}
