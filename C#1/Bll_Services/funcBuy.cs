using DTO_Command;
using IBll_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll_Services
{
    public class funcBuy : IBll_Services.IBuyBll
    {
        public IDal_Repository.IDalBuy dr;
        public IDal_Repository.IDalPurchase dp;
        public IDal_Repository.IDalClient dc;
        public IDal_Repository.IDalProd dprod;

        public funcBuy(IDal_Repository.IDalBuy dr, IDal_Repository.IDalPurchase dp, IDal_Repository.IDalClient dc, IDal_Repository.IDalProd dalProd)
        {
            this.dr = dr;
            this.dp = dp;
            this.dc = dc;
            dprod = dalProd;
        }
        public async Task<List<Buy>> getAllAsync()
        {
            return await dr.selectAllAsync();
        }
        public static int percent = 20;
        //הפונקציה מחשבת את הסכום, כולל בדיקות על הלקוח שזה תקין
        public async Task<double> calculateSum(Buy buy)
        {
            double sum = 0;
            for (int i = 0; i < buy.products.Count(); i++)
            {
                if (buy.products[i].Amount - buy.products[i].TempAmount >= 0)
                {
                    sum += (double)(buy.products[i].TempAmount * buy.products[i].Price);
                }
                else
                {
                    sum += (double)(buy.products[i].Amount * buy.products[i].Price);
                }
            }
            Client client = await dc.getById(buy.CodeClient);
            if (client.BearthDate?.Month == DateTime.Now.Month)
            {
                sum = (sum * (100 - percent)) / 100;
            }
            return sum;
        }

        public async Task<Buy> addPurchaces(Buy buy)
        {

            for (int i = 0; i < buy.products.Count(); i++)
            {
                DTO_Command.Product p = await dp.addPurchaceDetailAsync(buy.products[i], buy.Id);
                //buy.products.Add(p);
                if (p.TempAmount != buy.products[i].TempAmount)
                    buy.finished.Add(p);
            }
            return buy;
        }
        //מעדכנת קניה ופרטי קניה בלי עדכון מוצר
        public async Task<DTO_Command.Buy> addBuyAsync(Buy buy)
        {
            bool flag = false;
            Buy b = new Buy();
            //בודקת אם הקוד שווה ל0 או לא קיים:
            
            if (buy.Id == 0)
            {
                flag = true;
            }
            else
            {
                b = await dr.existsBuy(buy.Id);
            }
            //אם אכן זה נכון:
            if (flag || b == null)
            {
                //1. מחשבת את הסכום לתשלום
                buy.SumPrice = await calculateSum(buy);
                //2. מוסיפה קניה עם הסכום לתשלום החדש
                Buy newBuy = await dr.addBuyAsync(buy);
                //3. מוסיפה פרטי קניה עם קוד הקניה החדש ומחזירה את הקניה שהגיעה עם קוד חדש
                if (newBuy != null)
                {
                    buy.Id = newBuy.Id;
                    return await addPurchaces(buy);
                }
                return null;
            }
            //אם זה לא נכון:
            //1. מחשבת את הסכום לתשלום
            b.SumPrice = await calculateSum(buy);
            //2. מעדכנת את הסכום של הקניה 
            await dr.updateSum(buy.Id, (double)b.SumPrice);
            //מוחקת את הקניה הקודמת
            await dp.deleteAllInBuy(b.Id);
            //מוסיפה את פרטי הקניה החדשים
            return await addPurchaces(buy);
        }
        public async Task<Dictionary<string, int>> saveBuyAsync(int codeBuy)
        {
            //לשנות סטטוס ולקבל את כל המוצרים שאינם יכולים להמכר DALBUY קריאה לפונקציה ב
            return await dr.saveBuyAsync(codeBuy);
        }
        public async Task<List<DTO_Command.Buy>> getByIdClientAsync(string codeClient)
        {
            return await dr.getByIdClientAsync(codeClient);
        }

    }
}
