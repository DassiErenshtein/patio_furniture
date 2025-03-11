import { AbstractControl } from "@angular/forms";

export class forLogin{
    constructor(public type:string="",public input:string="",public value:string="",public func:(value: any)=> any=()=>true){}
}