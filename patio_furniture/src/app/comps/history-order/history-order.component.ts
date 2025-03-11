import { Component, Input, OnInit } from '@angular/core';
import { Buy } from '../../classes/Buy';
import { Product } from '../../classes/Product';

@Component({
  selector: 'app-history-order',
  imports: [],
  templateUrl: './history-order.component.html',
  styleUrl: './history-order.component.css'
})
export class HistoryOrderComponent implements OnInit {
  @Input() order!: Buy
  ngOnInit(): void {
  }
  //מקבלת מוצר ומחזירה את הניתוב של התמונה שלו- כל הניתובים נמצאים בתוך תקיות תואמות לקטגוריה
  getUrlPic(product: Product) {
    debugger
    if (product && product.pic)
      return product.nameCat + product.pic.split(',')[0]
    else
      return ""
  }
}
