using DTO_Command;
using IBll_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll_Services
{
    public class funcProd:IBll_Services.IProductBll
    {
        public IDal_Repository.IDalProd dr;
        public funcProd(IDal_Repository.IDalProd dr)
        {
            this.dr = dr;
        }

        public async Task<List<DTO_Command.Product>> getAllAsync()
        {
            return await dr.selectAllAsync();
        }

       
        public async Task<DTO_Command.Product> GetByIdAsync(int prodId)
        {
            return await dr.GetByIdAsync(prodId);
        }
        public async Task<List<DTO_Command.Product>> filterAsync(int? minPrice = 0, int? maxPrice = 0, int? codeCat = 0, int? codeComp = 0)
        {
            var list1 = await dr.filterAsync(minPrice,maxPrice,codeCat,codeComp);
            return list1;
            
        }
        public async Task<List<DTO_Command.Product>> thePopulatersProductsAsync()
        {
            var list = await dr.thePopulatersProductsAsync();
            return list;
        }

    }
}
