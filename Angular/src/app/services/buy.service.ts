import { HttpClient } from '@angular/common/http';
import { AfterViewInit, Injectable, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Buy } from '../classes/Buy';
import { Product } from '../classes/Product';
import { ClientService } from './client.service';
import { PurchaseBuy } from '../classes/PurchaseBuy';
import { ProductService } from './product.service';
import Swal from 'sweetalert2';
import { configService } from './configService';

@Injectable({
  providedIn: 'root'
})
export class BuyService {

  constructor(public cf: HttpClient, public cs: ClientService, public ps: ProductService,public configService:configService) {
    this.cart = new Buy(0, this.cs.thisClient.id || "", new Date(), 0, "", new Array<Product>())
    this.url = `${this.configService.apiUrl}buy`;
  }
  cart!: Buy
  public url:string
  // : string = 'https://patio-furniture.onrender.com/api/buy'
  //להמשיך איתו ולא ליצור חדש STORAGEאתחול הסל, אם קיים ב
  start() {
    let cart = localStorage.getItem('cart')
    if (cart != undefined)
      this.cart = JSON.parse(cart)
    else
      this.cart = new Buy(0, this.cs.thisClient.id || "", new Date(), 0, "", new Array<Product>())
  }
  //מחיקת הסל
  restart() {
    this.cart = new Buy(0, this.cs.thisClient.id || "", new Date(), 0, "", new Array<Product>())
    localStorage.setItem('cart', JSON.stringify(this.cart))
  }
  //כל המוצרים
  getAll(): Observable<Array<PurchaseBuy>> {
    return this.cf.get<Array<PurchaseBuy>>(this.url)
  }
  //עריכת הקוד של הסל, במקרה של קניה- הקוד משתנה...
  setCartId(id: number) {
    this.cart.id = id;
  }
  //הוספה לסל, כמו שראינו בהרבה מקומות אחרים. רק צריך לבדוק שבאמת יש אפשרות להוסיף לסל.
  // אם המוצר כבר קיים, מוסיפים כמות למוצר ששם, אחרת מוסיפים מוצר חדש
  // יכולה לקבל כמות ולעדכן, ואם לא מקבלת כמות, מעדכנת 1.
  addToCart(p: Product, count?: number): boolean {
    debugger    
    let iProd = this.cart.products?this.cart.products.findIndex(p1 => p1.id == p.id):-1
    if (iProd == -1) {
      if (p.amount - p.tempAmount < 1 || (count && p.amount - p.tempAmount < count + 1)) {
        return false;
      }
      p.tempAmount += count || 1;
      this.cart.products?.push(p)
      this.cart.count += count || 1;
      this.cart.sumPrice += (p.price || 0) * (count || 1);
      localStorage.setItem('cart', JSON.stringify(this.cart))
      return true;
    }
    else {
      if (this.cart.products&&this.cart.products[iProd].amount - this.cart.products[iProd].tempAmount < 1 || 
        (count &&this.cart.products&& this.cart.products[iProd].amount - this.cart.products[iProd].tempAmount < count + 1)) {
        return false;
      }
      if (this.cart.products)
        this.cart.products[iProd].tempAmount += count||1;
      this.cart.count += count || 1;
      this.cart.sumPrice += (p.price || 0) * (count || 1)
      localStorage.setItem('cart', JSON.stringify(this.cart))
      return true;
    }
  }
  //כנ"ל פשוט בהפחתה. וכאן תמיד מקבלת 1 להפחית
  reduceFromCart(p: Product): boolean {
    let iProd = this.cart.products?.findIndex(p1 => p1.id == p.id)
    if (iProd != -1) {
      if(p.tempAmount<2)
        return false;
      // if (p.tempAmount > 2) {
        p.tempAmount--;
      // }
      // else {
      //   this.cart.products!.splice(iProd||0, 1)
      // }
      this.cart.sumPrice -= p.price || 0
      this.cart.count--;
      localStorage.setItem('cart', JSON.stringify(this.cart))
      return true;
    }
    return false;
  }
  //מחיקה מהסל
  removeFromCart(id: number): boolean {
    let iProd = this.cart.products!.findIndex(p1 => p1.id == id)
    if (iProd != -1) {
      this.cart.count -= (this.cart.products![iProd].tempAmount || 0);
      this.cart.sumPrice -= (this.cart.products![iProd].tempAmount) * (this.cart.products![iProd].price || 0)
      this.cart.products!.splice(iProd, 1)
      localStorage.setItem('cart', JSON.stringify(this.cart))
      return true;
    }
    return false;
  }
  //הוספת קניה
  addBuy(): Observable<Buy> {
    this.cart.codeClient = this.cs.thisClient.id;
    return this.cf.post<Buy>(this.url, this.cart)
  }
  //שמירת הקניה שנוספה- הסטטוס שלה והמוצרים- השתנתה הכמות שלהם
  saveBuy(id: number): Observable<{ [key: string]: number }> {
    return this.cf.get<{ [key: string]: number }>(this.url + "/" + id)
  }
  //קבלת 5 הקניות האחרונות
  getHistory(): Observable<Array<Buy>> {
    return this.cf.get<Array<Buy>>(`${this.url}/history/${this.cs.thisClient.id}`)
  }
}
