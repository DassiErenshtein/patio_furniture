import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { Product } from '../../classes/Product';
import { CardCatComponent } from "../card-cat/card-cat.component";
import { ShopComponent } from "../shop/shop.component";
import { ActivatedRoute, RouterLink } from '@angular/router';
import { Category } from '../../classes/Category';
import { CategoryService } from '../../services/category.service';

@Component({
  selector: 'app-home-page',
  imports: [CardCatComponent],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.css'
})
export class HomePageComponent implements OnInit {
  constructor(public ps: ProductService, public catS: CategoryService, public ar: ActivatedRoute) {
    this.getPopulates()
  }
  categories: Array<Category> = []

  async ngOnInit() {
    debugger
    //מעדכנת את כל הקטגוריות הקיימות במערכת
    this.catS.getAll().subscribe(x =>
      this.categories = x
    )
    await this.ar.params.subscribe(x => {
      let element = x['nameScroll'] ? x['nameScroll'] : ""
      element = document.getElementById(element)
      if (element != "") {
        element.scrollIntoView({ behavior: 'smooth', block: 'start' });
      }
    })    //בודקת איך הגיעו לקומפוננטה, אם דרך האודות או דרך דף הבית הרגיל. אם דרך אודות, יגלול לשם
    this.ar.url.subscribe(urlSegments => {
      const fullPath = urlSegments.map(segment => segment.path);
      let element
      if (fullPath[fullPath.length - 1] == 'about')
        element = document.getElementById('about');
      if (element) {
        element.scrollIntoView({ behavior: 'smooth', block: 'start' });
      }

    });

  }
  //מערך של תמונות לדף הבית
  imgs: Array<string> = ['14.jpg', '359288.jpg', 'dokran-raz_category---_-_-_.jpg', 'a241.webp', 'a212.webp', 'dokran-raz_category---_-_-_.jpg', 'a202.webp', '578539.jpg', '576601.jpg', '543154.jpg', '530950.jpg', 'p3a.webp']
  population?: Array<Product>
  //קבלת ניתוב לפי הקטגוריה והניתוב של הראשון במערך הניתובים
  getUrl(product: Product) {
    let arr = product.pic?.split(',')
    return product.nameCat + arr![0]
  }
  //הפונקציה תחזיר לי את המוצרים הנקנים ביותר
  getPopulates() {
    this.ps.getPopulation().subscribe(data => {
      this.population = data
    })
  }
  scrollDown() {
    let element = document.getElementById('categories')
    if (element)
      element.scrollIntoView({ behavior: 'smooth', block: 'start' })
  }
}
