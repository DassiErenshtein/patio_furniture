using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBll_Services
{
    public interface IBuyBll:IBll<DTO_Command.Buy>
    {
        public Task<DTO_Command.Buy> addBuyAsync(DTO_Command.Buy buy);
        //public Task<DTO_Command.Buy> addBuyAsync(Dictionary<int,int> buy);

        //public Task<bool> saveBuyAsync(int codeBuy);
        //public Task<bool> saveBuyAsync(DTO_Command.Buy buy);
        public Task<Dictionary<string, int>> saveBuyAsync(int codeBuy);

        public Task<List<DTO_Command.Buy>> getByIdClientAsync(string codeClient);

    }
}
