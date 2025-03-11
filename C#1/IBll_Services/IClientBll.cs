using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBll_Services
{
    public interface IClientBll:IBll<DTO_Command.Client>
    {
        public Task<DTO_Command.Client> getById(string id);
        public Task<bool> addClient(DTO_Command.Client client);


    }
}
