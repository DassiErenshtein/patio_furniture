import { Component, OnInit } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { LoginComponent } from './comps/login/login.component';
import { ProductService } from './services/product.service';
import { FormsModule } from '@angular/forms';
import { ClientService } from './services/client.service';
import { BuyService } from './services/buy.service';
@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterLink, LoginComponent,FormsModule],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'clothing-websit';
  constructor(public ps:ProductService,public cs:ClientService,public bs:BuyService){
    
  }
  ngOnInit(): void {
    this.bs.start()
  }
  login: boolean = false;
  letLogin(f: boolean): void {
    debugger
    this.login = f||this.cs.flagFormLR;
  }
}
