using DTO_Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDal_Repository
{
    public interface IDalProd:IDal<DTO_Command.Product>
    {
        public Task<DTO_Command.Product> GetByIdAsync(int prodId);
        public Task<List<Product>> filterAsync(int? minPrice=0, int? maxPrice=0, int? codeCat=0, int? codeComp=0);
        public Task<List<DTO_Command.Product>> thePopulatersProductsAsync();
        //public Task<DTO_Command.Product> updateProduct(int idProd,int amount);

    }
}
