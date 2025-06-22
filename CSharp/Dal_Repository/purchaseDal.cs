using Dal_Repository.models;
using Dal_Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal_Repository
{
    public class purchaseDal:IDal_Repository.IDalPurchase
    {
        Byac3kvjhqok4gtkxgttContext db;
        public purchaseDal(Byac3kvjhqok4gtkxgttContext dB)
        {
            db = dB;
        }
        //מקבלת מוצר שנקנה וקוד קניה
        //TEMPAMOUNT - מוסיפה פרטי קניה למוצר שקבלה (במוצר יהיה גם כמות שנלקחה 
        //מחזירה את המוצר החדש שנקנה, ומעדכנת את הכמות הזמנית שלו לכמות שנקנתה
        public async Task<DTO_Command.Product> addPurchaceDetailAsync(DTO_Command.Product product, int codeBuy)
        {
            var thisProduct=await db.Products.FirstOrDefaultAsync(p => p.Id == product.Id);
            Models.PurchaseDetail p = new Models.PurchaseDetail();
            p.CodeBuy = (short?)codeBuy;
            p.CodeProd = product.Id;
            p.Amount = product.TempAmount;
            await db.PurchaseDetails.AddAsync(p);
            if (await db.SaveChangesAsync() > 0)
            {
                var newProduct = converters.ProductConverter.toDtoProd(p.CodeProdNavigation);
                newProduct.TempAmount = p.Amount;
                return newProduct;
            }
                
            return null;
        }
        //מוחקת את כל פרטי הקניה שהוזמנו, כדי לאתחל אותם מחדש במקרה של עדכון קניה-
        //אם הלקוח בצע קניה, לא שלם, ושינה את הקניה- אז ימחקו הפרטים הישנים ותתווסף קניה חדשה.
        public async Task<bool> deleteAllInBuy(short codeBuy)
        {
            db.RemoveRange(db.PurchaseDetails.Where(p => p.CodeBuy == codeBuy));
            return await db.SaveChangesAsync() > 0;
        }

        public Task<List<DTO_Command.PurchaseDetail>> selectAllAsync()
        {
            throw new NotImplementedException();
        }
        

    }
}
