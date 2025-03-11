import { Component } from '@angular/core';
import { CardCatComponent } from "../card-cat/card-cat.component";
import { Category } from '../../classes/Category';
import { CategoryService } from '../../services/category.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-shop',
  imports:[CardCatComponent],
  templateUrl: './shop.component.html',
  styleUrls:[ './shop.component.css']
})
export class ShopComponent {
  constructor(public catS:CategoryService){   
  }
  categories:Array<Category>=[]
  ngOnInit():void{
    //לקיחת כל הקטגוריות
    debugger
    this.catS.getAll().subscribe(
      x=>
        this.categories=x
    )
    
  }
  getImg(cat:Category){
    return cat.nameC+cat.img;
  }
  
}
