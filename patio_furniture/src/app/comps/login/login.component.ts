import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ClientService } from '../../services/client.service';
import { FormsModule } from '@angular/forms';
import { Client } from '../../classes/Client';
import { ActivatedRoute ,Router} from '@angular/router';
import { NgClass } from '@angular/common';
import { forLogin } from '../../classes/forLogin';
import swal from 'sweetalert2'
import { Location } from '@angular/common';
@Component({
  selector: 'app-login',
  standalone: true,
  imports: [NgClass, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  //אז מקבל משתנה זה, אחרת יקבל את זה בניתוב וצריך לשלוף אותו וגם בעת סגירה, לבצע דברים שונים NAVאם הגיע דרך ה
  //כדי לדעת מאיפה הגעתי, ומאיפה צריך לסגור את הקומפוננטה  FlagFromUrl לכן יש את משתנה 
  //האם בדרך של חזרה, או בדרך של הפעלת הפונקציה שהגיעה. (שסוגרת)
  //של האובייקט INPUT שמצביע על פונקציה שמפעילה בדיקת תקינות על ה FUNC בכל אובייקט במערך יש גם מאפיין 
  @Input() nameArr?: string
  @Output() close = new EventEmitter<void>();
  // את סוג התגית וגם מכילה אובייקט שאחראי על קליטת התוכן INPUTשאזדקק להם בצורה ששומרת את שם ה INPUTמערך שמכיל את כל ה
  //עם NGMODEL
  public arrayForm: Array<forLogin> = new Array<forLogin>
  newClient: Client = new Client()
  //כדי שנדע מאיפה לקחת את שם המערך ולאיפה לחזור
  flagFromUrl: boolean = false
  constructor(public cs: ClientService, public ar: ActivatedRoute, public l: Location,public router:Router) {}
   //בעת טעינת הדף תתבצע טעינת המערך מהסרוויס לפי שם המערך שקבלתי
  //כמובן שקודם הוא בודק את זה, ומאתחל את המשתנה הבוליאני בהתאם
  async ngOnInit() {   
    if (!this.nameArr) {
      this.flagFromUrl = true;
      await this.ar.params.subscribe(x => {
        this.nameArr = x['nameArr'] ? x['nameArr'] : ''
      })
    } 
    this.setArr()
  }
  //הפונקציה לוקחת את המערך התואם מהסרוויס לפי שם המערך שנשלח אליה
  async setArr() {      
    this.arrayForm = this.cs[this.nameArr as keyof ClientService] as Array<forLogin>
  }
  //סגירת הקומפוננטה- בדיקה האם המשתנה הבוליאני מרמז על כך שהגיעו דרך הניתוב, אם כן חוזרים בניתוב.
  //  אחרת מפעילים את הפונקציה שנשלחה שסוגרת את הקומפוננטה
  closeLogin() {
    debugger
    if (this.flagFromUrl == true)
      this.l.back();
    else
      this.close.emit();
  }
  //במקרה שעברו מטופס אחד לשני (אם סטטוס הלקוח שונה) צריך לשנות את שם המערך,
  //ולאחר מכן לקרא לפונקציה ששולפת לפי השם מהסרוויס
  changArr(string: string) {
    this.nameArr = string
    this.setArr()
  }
  //יש משתנה אחד שהוא אינו כמו כולם, והוא מכיל את הפונקציה של השמירה, לאחר הפעלתה נסגור את הקומפוננטה
  async save(func: any) {
    let a=await func({ arrayForm: this.arrayForm, newClient: this.newClient, fromUrl: this.flagFromUrl })
    this.closeLogin()
  }
}