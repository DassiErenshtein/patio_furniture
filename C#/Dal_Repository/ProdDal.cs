using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dal_Repository.models;
using DTO_Command;
using Microsoft.EntityFrameworkCore;

namespace Dal_Repository
{
    public class ProdDal : IDal_Repository.IDalProd
    {
        PatioFurnitureContext db;
        public ProdDal(PatioFurnitureContext dB)
        {
            db = dB;
        }
        

        public async Task<List<DTO_Command.Product>> selectAllAsync()
        {
            var list1 = await db.Products.Include(p => p.CodeCatNavigation).Include(p => p.CodeComNavigation).Take(20).ToListAsync();
            return converters.ProductConverter.toListDtoProd(list1);
        }
        public async Task<DTO_Command.Product> GetByIdAsync(int prodId)
        {
            var product = await db.Products.Include(p => p.CodeCatNavigation).Include(p => p.CodeComNavigation)
                .FirstOrDefaultAsync(p => p.Id == prodId);
            if (product == null)
                return null;
            return converters.ProductConverter.toDtoProd(product);
        }
        public async Task<List<DTO_Command.Product>> filterAsync(int? minPrice = 0, int? maxPrice = int.MaxValue, int? codeCat = 0, int? codeComp = 0)
        {
            if (maxPrice == 0)
                maxPrice = int.MaxValue;
            var list1 = await db.Products.Include(p => p.CodeCatNavigation).Include(p => p.CodeComNavigation).ToListAsync();
            list1 = list1.Where(p => (codeComp == 0 || p.CodeCom == codeComp)).Where(p => p.Price <= maxPrice && p.Price >= minPrice).ToList();
            if (codeCat != 0)
                list1 = list1.Where(p => p.CodeCat == codeCat).ToList();
            list1 = list1.Take(20).ToList();
            return converters.ProductConverter.toListDtoProd(list1);
        }
        public async Task<List<DTO_Command.Product>> thePopulatersProductsAsync()
        {
            var products = await db.PurchaseDetails
               .Include(x => x.CodeProdNavigation)
            .ThenInclude(x => x.CodeCatNavigation)
            .Include(x => x.CodeProdNavigation)
            .ThenInclude(x => x.CodeComNavigation).ToListAsync();
            var prod1=products.GroupBy(x => x.CodeProdNavigation)
            .Select(x => new { product = x.Key, count = x.Count() })                
            .OrderByDescending(x => x.count).Take(4).Select(x => x.product)
            .ToList();
            return converters.ProductConverter.toListDtoProd(prod1);
        }

        //public async Task<DTO_Command.Product> updateProduct(int idProd, int amount)
        //{
        //    var prod =await db.Products.FirstOrDefaultAsync(p => p.Id == idProd);
        //    if (prod.Amount >= amount)
        //    {
        //        prod.Amount -= (short?)amount;
        //    }
        //    else
        //        prod.Amount = 0;
        //    prod.LastUpdate = DateTime.Now;
        //    await db.SaveChangesAsync();
        //    return converters.ProductConverter.toDtoProd(prod);
        //}
    }
}
