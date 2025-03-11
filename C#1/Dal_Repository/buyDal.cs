using Dal_Repository.models;
using DTO_Command;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal_Repository
{
    public class BuyDal : IDal_Repository.IDalBuy
    {
        PatioFurnitureContext db;
        public BuyDal(PatioFurnitureContext dB)
        {
            db = dB;
        }
        public async Task<List<DTO_Command.Buy>> selectAllAsync()
        {
            var list1 = await db.Buys.Include(x => x.PurchaseDetails).ThenInclude(x => x.CodeProdNavigation).ToListAsync();
            return await converters.BuyConverter.toListDtoBuy(list1);
        }

        //הוספת קניה בלי להוסיף שום פרטי קניה. מחזיר את הקניה עם הקוד שלה כדי שיוכלו לעדכן בפרטי קניה את הקוד
        public async Task<DTO_Command.Buy> addBuyAsync(DTO_Command.Buy buy)
        {
            var newBuy = converters.BuyConverter.toModelBuy(buy);
            var a = await db.Buys.AddAsync(newBuy);
            if (await db.SaveChangesAsync() > 0)
                return await converters.BuyConverter.toDtoBuy(newBuy);
            return null;
           
        }
        //NULL בודקת האם קיימת קניה כזו, אם כן- מחזירה אותה. אחרת, מחזירה 
        public async Task<DTO_Command.Buy> existsBuy(short codeBuy)
        {
            var buy = await db.Buys.Include(x=>x.PurchaseDetails).FirstOrDefaultAsync(b => b.Id == codeBuy);
            if(buy != null)
                return await converters.BuyConverter.toDtoBuy(buy);
            return null;
            
        }
        public async Task<List<DTO_Command.Buy>> getByIdClientAsync(string codeClient)
        {
            var orders = await db.Buys.Include(x => x.PurchaseDetails).ThenInclude(x=>x.CodeProdNavigation)
                .ThenInclude(x=>x.CodeCatNavigation)
                .Where(x => x.CodeClient == codeClient&&x.StatusBuy==true)
                .OrderByDescending(x => x.Date).Take(5).ToListAsync();
            return await converters.BuyConverter.toListDtoBuy(orders);
        }
        //מקבלת קניה וסכום חדש, ומעדכנת את הסכום החדש עבור הקניה. במקרה של עדכון קניה
        public async Task updateSum(int buyId,double sumPrice)
        {
            var a=await db.Buys.FirstOrDefaultAsync(x => x.Id == buyId);
            a.SumPrice = sumPrice;
            await db.SaveChangesAsync();
        }
        //שומרת את הקניה לאחר שלחצו על אישור תשלום. מקבלת קוד קניה, מוצאת אותה, ,מעדכנת את הסטטוס ואת פרטי הקניה שלה
        public async Task<Dictionary<string, int>> saveBuyAsync(int codeBuy)
        {
            //לוקחת קניה עם פרטי הקניה שלה והמוצרים שקשורים אליה
            var buy=await db.Buys.Include(x => x.PurchaseDetails).ThenInclude(x => x.CodeProdNavigation)
                .FirstOrDefaultAsync(x => x.Id == codeBuy);
            buy.StatusBuy = true;
            //המוצרים שנגמרו
            Dictionary<string,int> finished = new Dictionary<string,int>();
            var listPurchase = buy.PurchaseDetails.ToList();
            //עוברת על כל פרטי הקניה, ועבור כל אחד מהם מעדכנת את הכמות של המוצר שלו להיות פחות הכמות שקבל
            for (int i = 0; i < listPurchase.Count(); i++)
            {
                var prod = buy.PurchaseDetails.ToList()[i].CodeProdNavigation;
                //אם אין מספיק מהמוצר, מוסיפה למערך של המוצרים שאין מהם מספיק, ומעדכנת את הכמות של הקניה להיות זו שיש במוצר
                if (prod.Amount < listPurchase[i].Amount )
                {
                    //כמה מהמוצר לא נקנה כי לא היה במלאי
                    finished.Add(prod.NameP, (int)(listPurchase[i].Amount-prod.Amount));
                    listPurchase[i].Amount = prod.Amount;
                    prod.Amount = 0;
                    prod.LastUpdate=DateTime.Now;
                }
                // אחרת, מפחיתה את הכמות מהמוצר ומעדכנת תאריך
                else
                {
                    prod.Amount -= listPurchase[i].Amount;
                    prod.LastUpdate = DateTime.Now;
                }
            }
            await db.SaveChangesAsync();
            //מחזירה את המוצרים שלא היה במלאי, עם הכמות החסרה.
            return finished;
        }
    }
}
