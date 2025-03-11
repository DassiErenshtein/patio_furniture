using Dal_Repository.models;
using DTO_Command;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal_Repository
{
    public class ClientDal : IDal_Repository.IDalClient
    {
        PatioFurnitureContext db;
        public ClientDal(PatioFurnitureContext dB)
        {
            db = dB;
        }
        public async Task<List<DTO_Command.Client>> selectAllAsync()
        {
            var clients = await db.Clients.ToListAsync();
            return converters.ClientConverter.toListDtoClients(clients);
        }
        public async Task<DTO_Command.Client> getById(string id)
        {
            var client=await db.Clients.Where(c=>c.Id.Equals(id)).FirstOrDefaultAsync();
            if(client==null)
                return null;
            return converters.ClientConverter.toDtoClient(client);
        }
        public async Task<bool> addClient(DTO_Command.Client client)
        {
            
            var client1 = await db.Clients.Where(c => c.Id.Equals(client.Id)).FirstOrDefaultAsync();
            if (client1 == null)
            {
                await db.Clients.AddAsync(converters.ClientConverter.toSqlClient(client));
                return await db.SaveChangesAsync()>0;
            }
            return false;
        }

    }
}
