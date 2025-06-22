using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal_Repository.converters
{
    internal class ProductConverter
    {
        public static DTO_Command.Product toDtoProd(Models.Product prod)
        {
            DTO_Command.Product newProd = new DTO_Command.Product();
            newProd.Id = prod.Id;
            newProd.Amount = prod.Amount;
            newProd.NameCom = prod.CodeComNavigation?.NameC;
            newProd.NameCat = prod.CodeCatNavigation?.NameC;
            newProd.CodeCom = prod.CodeCom;
            newProd.CodeCat = prod.CodeCat;
            newProd.Price= prod.Price;
            newProd.Pic= prod.Pic;
            newProd.Descrip=prod.Descrip;
            newProd.LastUpdate = prod.LastUpdate;
            newProd.NameP = prod.NameP;
            return newProd;
        }
        public static List<DTO_Command.Product> toListDtoProd(List<Models.Product> products)
        {
            List<DTO_Command.Product> newProds = new List<DTO_Command.Product>();
            foreach (var prod in products)
            {
                newProds.Add(toDtoProd(prod));
            }
            return newProds;
        }
    }
}
