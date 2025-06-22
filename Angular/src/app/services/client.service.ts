import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Client } from '../classes/Client';
import { forLogin } from '../classes/forLogin';
import { AbstractControl } from '@angular/forms';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import Swal from 'sweetalert2';
import { BuyService } from './buy.service';
import { configService } from './configService';

@Injectable({
  providedIn: 'root'
})
export class ClientService {
  flagFormLR: boolean = false
  //מערך מסוג אובייקט שיצרתי, שמשמעותו:
  // type- input הסוג של ה
  // input-כותרת הINPUT
  // value-NGMODEL משתנה שאמור לקבל את הערך של האינפוט בלקוח 
  // func-פונקציית בדיקת תקינות הערך שיוכנס באינפוט 
  // כל אחד מהמערכים האלו מכיל רשימה של אינפוטים ואינפוט להגשה ואינפוט להחלפת המערך
  //הקומפוננטה מסדרת את המערך בצורת טופס, ומבצעת את בדיקות התקינות שנשלחו אליה
  register: Array<forLogin> = [
    {
      type: "string", input: "id", value: "", func: (id: string): boolean => {
        if (id.length != 9)
          return false;
        else {
          let sum = 0, i1 = 1, check = +id.charAt(id.length - 1), num = 0;
          id = id.substring(0, id.length - 1)
          for (let i = 0; i < id.length; i++) {
            try {
              +id.charAt(i)
            }
            catch (error) {
              return false
            }
            num = i1 * +id.charAt(i)
            while (num >= 1) {
              sum += num % 10
              num -= num % 10
              num /= 10
            }
            if (i1 == 2)
              i1 = 1
            else
              i1 = 2
          }
          if (check == 10 - sum % 10 || (check == 0 && 0 == sum % 10))
            return true;
          return false
        }

      }
    },
    {
      type: "string", input: "name", value: "", func: (name: string): boolean => {
        for (let i = 0; i < name.length; i++) {
          if (!name.charAt(i).match(/[a-zא-תA-Z]/i))
            return false;
        }
        return true;
      }
    },
    {
      type: "string", input: "phone", value: "", func: (phone: any): boolean => {
        const phoneRegex = /^\d{9,10}$/;
        return phoneRegex.test(phone);
      }
    },
    {
      type: "email", input: "email", value: "", func: (email: any): boolean => {
        // const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        // return emailRegex.test(email);
        return true
      }
    },
    {
      type: "date", input: "bearthDate", value: "", func: (birthDate: any): boolean => {
        const today = new Date();
        const birthDate1 = new Date(birthDate);
        return birthDate1 < today;
      }
    },
    {
      type: "changePage", input: "already registered?", value: "login", func: async (a: any): Promise<void> => {
        await this.route.navigate([a.path])
      }
    },
    {
      type: "submit", input: "", value: "", func: async (a: any) => {
        debugger
        a.newClient.id = a.arrayForm[0].value
        a.newClient.name = a.arrayForm[1].value
        a.newClient.phone = a.arrayForm[2].value
        a.newClient.email = a.arrayForm[3].value
        a.newClient.bearthDate = new Date(a.arrayForm[4].value!)
        await this.register1(a.newClient)
          .subscribe(x => {
            if (x == true) {
              Swal.fire("good👍", "נרשמת בהצלחה!", "success")
              this.thisClient = a.newClient
              return true
            }
            else {
              Swal.fire("שגיאה", " לקוח זה כבר קיים במערכת!", "error")
              return false
            }

          }
            , err => {
              Swal.fire("שגיאה", " ארעה תקלה, אנא נסו שנית!", "error")

              return false
            })
      }
    }
  ]
  login: Array<forLogin> = [
    {
      type: "string", input: "email", value: "", func: (email: any): boolean => {
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return emailRegex.test(email);
      }
    },
    {
      type: "string", input: "id", value: "", func: (id: string): boolean => {
        if (id.length != 9)
          return false;
        else {
          let sum = 0, i1 = 1, check = +id.charAt(id.length - 1), num = 0;
          id = id.substring(0, id.length - 1)
          for (let i = 0; i < id.length; i++) {
            try {
              +id.charAt(i)
            }
            catch (error) {
              return false
            }
            num = i1 * +id.charAt(i)
            while (num >= 1) {
              sum += num % 10
              num -= num % 10
              num /= 10
            }
            if (i1 == 2)
              i1 = 1
            else
              i1 = 2
          }
          if (check == 10 - sum % 10 || (check == 0 && 0 == sum % 10))
            return true;
          return false
        }

      }
    },
    {
      type: "changePage", input: "don't have an account?", value: "register", func: async (a: any): Promise<void> => {
      }
    },
    {
      type: "submit", input: "", value: "",
      func: async (a: any) => {
        debugger
        a.newClient.email = a.arrayForm[0].value
        a.newClient.id = a.arrayForm[1].value
        let flag = false
        await this.login1(a.newClient.id)
          .subscribe(x => {
            if (x && x.email == a.newClient.email) {
              Swal.fire("good👍", `היי ${x.name},ברוכ/ה הבאה!!`, "success")
              this.thisClient = x
              flag = true;
              return true;
            }
            else {
              Swal.fire("שגיאה", "ערכים אינם תואמים לקיים במערכת!", "error")
              flag = true
              return false
            }

          }
            , err => {
              Swal.fire("שגיאה", " לקוח זה אינו קיים במערכת!", "error")
              flag = true
              return false
            })
      }
    }
  ]
  public url: string 
  // = 'https://patio-furniture.onrender.com/api/client'
  thisClient: Client = new Client()
  constructor(public cf: HttpClient, public route: Router, public l: Location,public configService:configService) { 
    this.url = `${this.configService.apiUrl}client`;
  }
  //שליחה לפונקציות אלו מפונקציות השליחה שנשלחו מכאן
  // התחברות
  login1(id: number): Observable<Client> {
    return this.cf.get<Client>(this.url + "/" + id)
  }
  //הרשמה
  register1(client: Client): Observable<boolean> {
    return this.cf.post<boolean>(this.url, client)
  }
}
