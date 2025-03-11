import { Component, Output, EventEmitter } from '@angular/core';
import { CartItemComponent } from "../cart-item/cart-item.component";
import { Product } from '../../classes/Product';
import { ProductService } from '../../services/product.service';
import Swal from 'sweetalert2';
import { ClientService } from '../../services/client.service';
import { Router } from '@angular/router';
import { BuyService } from '../../services/buy.service';
import { Buy } from '../../classes/Buy';

@Component({
  selector: 'app-basket',
  imports: [CartItemComponent],
  templateUrl: './basket.component.html',
  styleUrl: './basket.component.css'
})
export class BasketComponent {
  constructor(public ps: ProductService, public cs: ClientService, public router: Router, public bs: BuyService) {
  }
  //בודקת גם האם הכמות אפשרית למוצר או לא localstorageהוספה לסל מעדכנת את הסכום לתשלום ואת הכמות המקומיים וגם ב
  addToCart(item: Product) {
    let flag = this.bs.addToCart(item)
  }
  //כנ"ל רק בהורדה. לא כולל בדיקת כמות. (תמיד אפשר להוריד, כשמגיע ל0 נמחק)
  reduceFromCart(item: Product) {
    let flag = this.bs.reduceFromCart(item)
  }
  //מחיקה מהסל- מרשימת המוצרים שבסל, כולל עדכון כמות ומחיר
  removeFromCart(id: number) {
    let flag = this.bs.removeFromCart(id)
  }
  //עדכון קניה- מעדכן בשרת שהקניה אושרה, השרת יעדכן את הסטטוס ואת המוצרים יפחית מהם.
  //יחזור אובייקט של מוצר שלא היה וכמות חסרה והפונקציה תציג אותם ללקוח  
  updateBuy(buy: Buy) {
    debugger
    this.bs.saveBuy(buy.id || 0).subscribe((data) => {
      let y = Object.entries(data).map(([key, value]) => ({
        key,
        value,
      }));
      this.bs.restart()
      let str = "";
      let flag = false;
      if (y.length != 0) {
        y.forEach(element => {
          str += "מהמוצר " + element.key + ":" + element.value + " חסרים במלאי...";
        });
        str += "\n מצטערים, נסה שוב בהזדמנות מאוחרת יותר... 🤔🤔"
        Swal.fire("👍","הקניה נשמרה בהצלחה!\n " + str, "success");
      }        
      else {
        Swal.fire("👍","הקניה נשמרה בהצלחה!\n ", "success");
      }
      this.router.navigate(['products'])
    })
  }
  //מוסיפה קניה לשרת, מוסיפה פרטי קניה.
  //  במקרה שזה פעם שניה תשלח הפעם את פרטי הקניה עם קוד הקניה הקיים והשרת ידע להסתדר עם זה
  //LOCALSTORAGE וגם מה SERVICEבמידה והלקוח ילחץ על אישור קניה- יעברו לעדכון קניה. במידה וילחץ על מחיקה, הסל ימחק גם מה
  //במידה ובחר לבטל את ההזמנה, קוד ההזמנה כבר נשמר לו, ואם יקנה שוב- יתעדכנו המוצרים במסד.
  async toPay() {
    if (!this.cs.thisClient.id) {
      this.router.navigate(['login/login'])
    }

    else {
      await this.bs.addBuy().subscribe(x => {        
        this.bs.setCartId(x.id || 0)
        localStorage.setItem('cart', JSON.stringify(x))
        Swal.fire({
          title: "הקניה שלך נשמרה, הסכום הסופי:" + x.sumPrice,
          showDenyButton: true,
          showCancelButton: true,
          confirmButtonText: "לאישור קניה",
          denyButtonText: `לביטול הקניה`,
          cancelButtonText: "חזור"
        }).then((result) => {
          /* Read more about isConfirmed, isDenied below */
          if (result.isConfirmed) {
            this.updateBuy(x)
          } else if (result.isDenied) {
            Swal.fire({
              title: "האם אתה בטוח?",
              text: "לאחר מכן לא תוכל לחזור לסל שלך!",
              icon: "warning",
              showCancelButton: true,
              confirmButtonColor: "#3085d6",
              cancelButtonColor: "#d33",
              confirmButtonText: "כן, מחק את הסל!"
            }).then(async (result) => {
              if (result.isConfirmed) {
                await this.bs.restart()
                Swal.fire({
                  title: "הסל נמחק!",
                  text: "הסל נמחק בהצלחה.",
                  icon: "success"
                });
              }
            });
          }
        });
      })
    }
  }
}
