using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll_Services
{
    public class funcCat:IBll_Services.IBll<DTO_Command.Category>
    {
        public IDal_Repository.IDal<DTO_Command.Category> dr;
        public funcCat(IDal_Repository.IDal<DTO_Command.Category> dr)
        {
            this.dr = dr;
        }

        public async Task<List<DTO_Command.Category>> getAllAsync()
        {
            return await dr.selectAllAsync();
        }
    }
}
