using DTO_Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll_Services
{
    public class funcClient : IBll_Services.IClientBll
    {
        public IDal_Repository.IDalClient dr;
        public funcClient(IDal_Repository.IDalClient dr)
        {
            this.dr = dr;
        }
        public async Task<List<Client>> getAllAsync()
        {
            return await dr.selectAllAsync();
        }

        public async Task<Client> getById(string id)
        {
            return await dr.getById(id);
        }
        public async Task<bool> addClient(DTO_Command.Client client)
        {
            return await dr.addClient(client);
        }
    }
}
