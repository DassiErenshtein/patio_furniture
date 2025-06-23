import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { Product } from '../../classes/Product';
import { ButtonComponent } from "../button/button.component";

@Component({
  selector: 'app-cart-item',
  imports: [ButtonComponent],
  templateUrl: './cart-item.component.html',
  styleUrl: './cart-item.component.css'
})
export class CartItemComponent implements OnInit{
  bigPic:string=""
  pics: Array<string> = new Array<string>
  @Input() item!: Product;
  @Input() line: boolean = false;
  @Output() add = new EventEmitter<void>();
  @Output() subtract = new EventEmitter<void>();
  @Output() remove = new EventEmitter<void>();
  ngOnInit(): void {
    this.pics = this.item && this.item.pic ? this.item.pic.split(",") : new Array<string>()
    //ניתוב לתמונה הראשונה במערך התמונות שלו כדי שתהיה תמונה למוצר בסל
    this.bigPic="assets/"+this.item.nameCat + this.pics[0];  }
    
  }
