import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Category } from '../classes/Category';
import { Product } from '../classes/Product';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  constructor(public cf: HttpClient) { }
  public url: string = 'https://patio-furniture.onrender.com/api/product'
  getByCat(catId: number): Observable<Array<Product>> {
    debugger
    return this.cf.get<Array<Product>>(`https://patio-furniture.onrender.com/api/product/byCat/${catId}`)
  }
  getAllProd(): Observable<Array<Product>> {
    return this.cf.get<Array<Product>>(this.url)
  }
  getProdById(id: number): Observable<Product> {
    debugger
    return this.cf.get<Product>(`${this.url}/byId/${id}`)
  }
  filter1(selected: Array<number>): Observable<Array<Product>> {
    debugger
    return this.cf.get<Array<Product>>(`https://patio-furniture.onrender.com/api/product/filter?minPrice=${selected[2]}&maxPrice=${selected[3]}&codeCat=${selected[0]}&codeComp=${selected[1]}`)

  }
  getPopulation(): Observable<Array<Product>> {
    debugger
    return this.cf.get<Array<Product>>(this.url+"/populate")
  }
}