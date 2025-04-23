using Microsoft.AspNetCore.Mvc;

namespace patio_furniture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class clientController : Controller
    {

        IBll_Services.IClientBll clients;
        public clientController(IBll_Services.IClientBll clients)
        {
            this.clients = clients;

        }
        [HttpGet]
        public async Task<List<DTO_Command.Client>> getAllAsync()
        {
            return await clients.getAllAsync();
        }
        [HttpGet("{id}")]
        //קבלת לקוח לפי תז- התחברות (שאר הדברים יבדקו באנגולר) 
        public async Task<ActionResult<DTO_Command.Client>> getById(string id)
        {
            var client= await clients.getById(id);
            if (client == null)
                return NotFound();
            return Ok(client);
        }
        [HttpPost]
        //הוספת לקוח חדש- הרשמה
        public async Task<ActionResult<bool>> Register(DTO_Command.Client client)
        {
            bool ans = await clients.addClient(client);
            if (ans == false)
                return NotFound();
            
            return Ok(true);
        }
    }
}
