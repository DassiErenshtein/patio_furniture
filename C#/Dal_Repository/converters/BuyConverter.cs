using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal_Repository.converters
{
    internal class BuyConverter
    {        
        public static async Task< DTO_Command.Buy> toDtoBuy(models.Buy buy)
        {
            DTO_Command.Buy newB = new DTO_Command.Buy();
            newB.Id = buy.Id;
            newB.products = new List<DTO_Command.Product>();
            if (buy.PurchaseDetails != null && buy.PurchaseDetails.Count() > 0 )
                foreach(var item in buy.PurchaseDetails)
                {
                    if (item.CodeProdNavigation == null)
                        break;
                    else
                    {
                        DTO_Command.Product newP = converters.ProductConverter.toDtoProd(item.CodeProdNavigation);
                        newP.TempAmount=item.Amount;
                        newB.products.Add(newP);
                    }
                }

            newB.SumPrice = buy.SumPrice;
            newB.CodeClient = buy.CodeClient;
            newB.Date=buy.Date;
            newB.Note = buy.Note;
            return newB;
        }
        public static models.Buy toModelBuy(DTO_Command.Buy buy)
        {
            models.Buy newB = new models.Buy();
            newB.SumPrice = buy.SumPrice;
            newB.CodeClient = buy.CodeClient;
            newB.Date = DateTime.Now;
            newB.Note = buy.Note;
            newB.StatusBuy = false;
            return newB;
        }
        public static async Task<List<DTO_Command.Buy>> toListDtoBuy(List<models.Buy> buys)
        {
            List<DTO_Command.Buy> newBuys = new List<DTO_Command.Buy>();
            foreach (var b in buys)
            {
                newBuys.Add(await toDtoBuy(b));
            }
            return newBuys;
        }
    }
}
