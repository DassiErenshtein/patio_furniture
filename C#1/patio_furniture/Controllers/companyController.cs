using Microsoft.AspNetCore.Mvc;

namespace patio_furniture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class companyController : Controller
    {

        IBll_Services.IBll<DTO_Command.Company> companies;
        public companyController(IBll_Services.IBll<DTO_Command.Company> companies)
        {
            this.companies = companies;

        }
        [HttpGet]
        public async Task<List<DTO_Command.Company>> getAllAsync()
        {
            return await companies.getAllAsync();
        }
    }
}
