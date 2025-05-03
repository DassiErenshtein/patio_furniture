import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Company } from '../classes/Company';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {

  constructor(public cf:HttpClient) { }
    
    public url:string='https://patio-furniture.onrender.com/api/company'
    getAll():Observable<Array<Company>>{
      debugger
      return this.cf.get<Array<Company>>(this.url)
    }
}
