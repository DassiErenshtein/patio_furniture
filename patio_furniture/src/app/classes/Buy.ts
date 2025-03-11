import { Product } from "./Product";
import { PurchaseBuy } from "./PurchaseBuy";

export class Buy {
    constructor(
        public id?: number,
        public codeClient?: string,
        public date?: Date,
        public sumPrice: number=0,
        public note?: string,
        public products?: Array<Product>,
        public finished:Array<Product>=new Array<Product>(),
        public count:number=0
    ) { }
}