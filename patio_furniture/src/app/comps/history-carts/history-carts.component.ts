import { Component, OnInit } from '@angular/core';
import { HistoryOrderComponent } from "../history-order/history-order.component";
import { BuyService } from '../../services/buy.service';
import { Buy } from '../../classes/Buy';
import { ClientService } from '../../services/client.service';

@Component({
  selector: 'app-history-carts',
  imports: [HistoryOrderComponent],
  templateUrl: './history-carts.component.html',
  styleUrl: './history-carts.component.css'
})
export class HistoryCartsComponent implements OnInit {
  constructor(public bs: BuyService, public cs: ClientService) { }
  ngOnInit(): void {
    debugger
    //מציגה את ההסטוריה של הלקוח, את כל הקניות האחרונות שלו (5)
    if (this.cs.thisClient.id != undefined)
      this.bs.getHistory().subscribe(data => {
        this.orders = data
      })
  }
  orders?: Array<Buy>
}
