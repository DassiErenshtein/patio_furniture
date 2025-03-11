using DTO_Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDal_Repository
{
    public interface IDalBuy:IDal<DTO_Command.Buy>
    {
        public Task<DTO_Command.Buy> addBuyAsync(DTO_Command.Buy buy);
        public Task<Dictionary<string,int>> saveBuyAsync(int codeBuy);

        public Task<DTO_Command.Buy> existsBuy(short codeBuy);
        public Task updateSum(int buyId, double sumPrice);
        public Task<List<DTO_Command.Buy>> getByIdClientAsync(string codeClient);


    }
}