using Microsoft.AspNetCore.Mvc;

namespace patio_furniture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class productController : Controller
    {

        IBll_Services.IProductBll products;

        public productController(IBll_Services.IProductBll products)
        {
            this.products = products;

        }
        [HttpGet]

        public async Task<List<DTO_Command.Product>> getAllAsync()
        {
            return await products.getAllAsync();
        }
        [HttpGet("byId/{id}")]
        //לפרטי מוצר- שליפה לפי קוד מוצר
        public async Task<DTO_Command.Product> getById(int id)
        {
            return await products.GetByIdAsync(id);
        }

        [HttpGet("filter")]

        public async Task<List<DTO_Command.Product>> filterAsync([FromQuery] short? minPrice = 0, [FromQuery] short? maxPrice = 0, [FromQuery] short? codeCat = 0, [FromQuery] short? codeComp=0)
        {
            
            var r= await products.filterAsync(minPrice, maxPrice, codeCat, codeComp);
            return r;
        }
        [HttpGet("populate")]
        public async Task<List<DTO_Command.Product>> thePopulatersProductsAsync()
        {
            return await products.thePopulatersProductsAsync();
        }
    }
}
