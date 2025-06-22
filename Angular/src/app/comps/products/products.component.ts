import { Component, Input } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { Product } from '../../classes/Product';
import { ActivatedRoute } from '@angular/router';
import { CardShopComponent } from '../card-shop/card-shop.component';
import { Category } from '../../classes/Category';
import { CategoryService } from '../../services/category.service';
import { Company } from '../../classes/Company';
import { CompanyService } from '../../services/company.service';
import { FormsModule } from '@angular/forms';
import { ButtonComponent } from "../button/button.component";

@Component({
  selector: 'app-products',
  imports: [CardShopComponent, FormsModule, ButtonComponent],
  templateUrl: './products.component.html',
  styleUrl: './products.component.css'
})
export class ProductsComponent {
  codeCategory: number = 0;
  
  // אתחול המחלקה עם השירותים לניהול מוצרים, קטגוריות וחברות
  constructor(
    public prodS: ProductService,
    public catS: CategoryService,
    public compS: CompanyService,
    public ar: ActivatedRoute
  ) {}

  products: Array<Product> = new Array<Product>();
  categorys: Array<Category> = new Array<Category>();
  companies: Array<Company> = new Array<Company>();
  selected: Array<number> = [0, 0, 0, 0];

  // בעת טעינת הרכיב, מאחזר נתונים מהנתיב ומבצע שליפות מהמאגרים
  async ngOnInit() {
    await this.ar.params.subscribe(params => {
      this.codeCategory = params['codeCat'] ? params['codeCat'] : undefined;
    });

    this.codeCategory
      ? await this.prodS.filter1([this.codeCategory, 0, 0, 0]).subscribe(
          data => (this.products = data)
        )
      : await this.prodS.getAllProd().subscribe(
          data => (this.products = data)
        );

    await this.catS.getAll().subscribe(data => {
      this.categorys = data;
    });

    await this.compS.getAll().subscribe(data => {
      this.companies = data;
    });
  }

  // סינון המוצרים לפי הקריטריונים שנבחרו
  filter1() {
    if (this.codeCategory) this.selected[0] = this.codeCategory;
    this.prodS.filter1(this.selected).subscribe(data => {
      debugger
      this.products = data;
    });
  }

  // בדיקה אם ערך בשדה סינון ריק - אם כן, הגדרת ברירת מחדל 0
  checkEmpty(i: number) {
    console.log(this.selected[i]);
    if (!this.selected[i]) this.selected[i] = 0;
  }

  // ממיין את רשימת המוצרים לפי פונקציה מותאמת אישית
  sorting(func: (x: Product, y: Product) => number = () => 0) {
    this.products = this.products.sort(func);
  }

  // מיון מוצרים מהנמוך לגבוה לפי מחיר
  bigToSmall(x: Product, y: Product) {
    return (x.price || 0) - (y.price || 0);
  }

  // מיון מוצרים מהגבוה לנמוך לפי מחיר
  smallToBig(x: Product, y: Product) {
    return (y.price || 0) - (x.price || 0);
  }
}
