using Microsoft.AspNetCore.Mvc;

namespace patio_furniture.Controllers 
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class buyController : Controller
    {

        IBll_Services.IBuyBll buys;
        public buyController(IBll_Services.IBuyBll buys)
        {
            this.buys = buys;

        }
        [HttpGet]
        //קבלת כל הקניות
        public async Task<List<DTO_Command.Buy>> getAllAsync()
        {
            var list= await buys.getAllAsync();
            return list;
        }
        [HttpPost]
        //הוספת קניה
        public async Task<DTO_Command.Buy> addBuyAsync(DTO_Command.Buy buy)
        {
            var a = await buys.addBuyAsync(buy);
            return a;
        }
        [HttpGet("{codeBuy}")]
        //הורדה מהמוצרים ושינוי סטטוס קניה
        public async Task<Dictionary<string, int>> updateBuy(int codeBuy)
        {
            var a = await buys.saveBuyAsync(codeBuy);
            return a;
        }

        [HttpGet("history/{codeClient}")]
        //קניות אחרונות
        public async Task<List<DTO_Command.Buy>> getByIdClientAsync(string codeClient)
        {
            var orders=await buys.getByIdClientAsync(codeClient);
            return orders;
        }

    }
}
