import { Component, Input } from '@angular/core';
import { Product } from '../../classes/Product';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { BuyService } from '../../services/buy.service';
import { CurrencyPipe, NgClass } from '@angular/common';
import { BaughtDirective } from '../../directives/baught.directive';

@Component({
  selector: 'app-card-shop',
  imports: [FormsModule,RouterLink,CurrencyPipe,BaughtDirective,NgClass],
  templateUrl: './card-shop.component.html',
  styleUrl: './card-shop.component.css'
})
export class CardShopComponent {
  @Input() product?: Product
  num:number=1
  pics: Array<string> = new Array<string>
  urlPic: string = ""
  constructor(public ps:ProductService,public bs:BuyService){}
  //מעדכנת את מערך התמונות למוצר הנוכחי ומעדכנת ניתוב לתמונה הראשונה שהיא זו שתוצג
  ngOnInit() {
    this.pics = this.product && this.product.pic?this.product.pic.split(",") : new Array<string>
    this.urlPic = this.product?.nameCat + this.pics[0]
  }
  //שנשלח לדירקטיב משתנה אם הוא יכול להתווסף לסל (וכך גם בתצוגה יראו ליד הסל כמה הוזמן ממוצר זה) NUM בעת הוספה לסל ישלח לפונקציה במסד והמשתנה 
  addToCart(){
    if(this.product && this.product.amount && this.product!.amount>0 && this.bs.addToCart(this.product)==true)
        this.num++
  }
}