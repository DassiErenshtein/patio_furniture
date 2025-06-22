import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Category } from '../classes/Category';
import { configService } from './configService';

@Injectable({
  providedIn: 'root'
})
export class CategoryService{
  
  constructor(public cf:HttpClient,public configService:configService) {
    this.url = `${this.configService.apiUrl}category`;
   }
  
  public url:string
  // ='https://patio-furniture.onrender.com/api/category'
  getAll():Observable<Array<Category>>{
    return this.cf.get<Array<Category>>(this.url)
  }
}
