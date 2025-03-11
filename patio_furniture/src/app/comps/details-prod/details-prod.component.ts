import { Component, Input } from '@angular/core';
import { Product } from '../../classes/Product';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { BuyService } from '../../services/buy.service';
import { Location } from '@angular/common';
import { FormsModule } from '@angular/forms';
@Component({
  selector: 'app-details-prod',
  imports: [FormsModule],
  templateUrl: './details-prod.component.html',
  styleUrl: './details-prod.component.css'
})
export class DetailsProdComponent {
  constructor(public ar: ActivatedRoute, public prodS: ProductService, public bs: BuyService, public l: Location) { }
  codeProduct: number = 0
  product: Product = new Product()
  pics: Array<string> = new Array<string>
  bigPic: string = ""
  count: number = 0
  async ngOnInit() {
    //קבלת קוד המוצר מהניתוב
    await this.ar.params.subscribe(x => {
      this.codeProduct = x['prodId'] ? x['prodId'] : 0
    })
    //קבלת המוצר של הקוד בשליחה לפונקציה בסרוויס ועדכון המוצר, מערך התמונות שלו והתמונה הראשונה להצגה
    await this.codeProduct != 0 && this.prodS.getProdById(this.codeProduct).subscribe(data => {
      this.product = data;
      this.pics = this.product && this.product.pic ? this.product.pic.split(",") : new Array<string>
      this.bigPic = this.product?.nameCat + this.pics[0];
    })

  }
  //קבלת הניתוב עפ"י אינדקס שעומדים בו במערך, כדי לקבל את כל התמונות של המוצר
  getUrlPic(i: number) {
    return this.product?.nameCat + this.pics[i];
  }
  //התמונה הגדולה תשתנה בהתאם ללחיצה על שאר התמונות
  changePic(i: number) {
    this.bigPic = this.product?.nameCat + this.pics[i];
  }
  //הוספה לסל, כמו שראינו בהרבה מקומות אחרים. רק צריך לבדוק שבאמת יש אפשרות כזו. הפונקציה בסרוויס בודקת את זה.
  addToCart() {
    if (this.product)
      this.bs.addToCart(this.product, this.count)
  }
  //בעת לחיצה על החץ, חוזרים למיקום הקודם שממנו הגיעו לפרטים
  return() {
    this.l.back()
  }
}
