using Microsoft.AspNetCore.Mvc;

namespace patio_furniture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class categoryController : Controller
    {

        IBll_Services.IBll<DTO_Command.Category> categories;
        public categoryController(IBll_Services.IBll<DTO_Command.Category> categories)
        {
            this.categories = categories;

        }
        [HttpGet]
        public async Task<List<DTO_Command.Category>> getAllAsync()
        {
            return await categories.getAllAsync();
        }
    }
}
