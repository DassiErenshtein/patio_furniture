using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll_Services
{
    public class funcComp:IBll_Services.IBll<DTO_Command.Company>
    {
        public IDal_Repository.IDal<DTO_Command.Company> dr;
        public funcComp(IDal_Repository.IDal<DTO_Command.Company> dr)
        {
            this.dr = dr;
        }

        public async Task<List<DTO_Command.Company>> getAllAsync()
        {
            return await dr.selectAllAsync();
        }
    }
}
