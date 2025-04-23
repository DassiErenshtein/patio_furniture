import { RouterModule, Routes } from '@angular/router';
import { HomePageComponent } from './comps/home-page/home-page.component'
import { ShopComponent } from './comps/shop/shop.component'
import { BasketComponent } from './comps/basket/basket.component'
import { ErrorComponent } from './comps/error/error.component'
import { LoginComponent } from './comps/login/login.component'
import { ProductsComponent } from './comps/products/products.component';
import { DetailsProdComponent } from './comps/details-prod/details-prod.component';
import { HistoryCartsComponent } from './comps/history-carts/history-carts.component';

export const routes: Routes = [
    { path: 'home page', component: HomePageComponent, title: 'דף הבית' },
    { path: 'home page/:nameScroll', component: HomePageComponent },
    {
        path: 'shop', component: ShopComponent, title: 'המוצרים שלנו',
    },
    { path: 'login/:nameArr', component: LoginComponent, title: 'הרשמה ' },
    { path: 'login', component: LoginComponent },

    { path: 'basket', component: BasketComponent, title: 'סל' },
    { path: 'detailsProd/:prodId', component: DetailsProdComponent, title: 'פרטים' },
    { path: 'products', component: ProductsComponent, title: 'מוצרים' },
    { path: 'products/:codeCat', component: ProductsComponent, title: 'מוצרים' },
    { path: 'history', component: HistoryCartsComponent, title: 'הסטורית קניות' },
    { path: '', component: HomePageComponent },
    { path: '**', component: ErrorComponent }

];
