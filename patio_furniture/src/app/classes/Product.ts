export class Product {
    constructor(public id?: number,
        public nameP?: string,
        public codeCat?: number,
        public codeCom?: number,
        public descrip?: string,
        public price?: number,
        public pic?: string,
        public amount: number=0,
        public tempAmount:number=0,
        public lastUpdate?: Date,
        public nameCat?: string,
        public nameCom?: string) { }
}