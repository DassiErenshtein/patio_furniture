using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDal_Repository
{
    public interface IDalPurchase:IDal<DTO_Command.PurchaseDetail>
    {
        public Task<DTO_Command.Product> addPurchaceDetailAsync(DTO_Command.Product buy, int codeBuy);
        public Task<bool> deleteAllInBuy(short codeBuy);

    }
}
