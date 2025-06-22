using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal_Repository.converters
{
    internal class ClientConverter
    {
        public static DTO_Command.Client toDtoClient(Models.Client client)
        {
            DTO_Command.Client newC = new DTO_Command.Client();
            newC.Id = client.Id;
            newC.Name = client.NameC;
            newC.Email = client.Email;
            if (client.BearthDate != null)
                newC.BearthDate = (DateTime)client.BearthDate;
            else
                newC.BearthDate = new DateTime();
            newC.Phone = client.Phone;
            return newC;
        }
        public static Models.Client toSqlClient(DTO_Command.Client client)
        {
            Models.Client newC = new Models.Client();
            newC.Id = client.Id;
            newC.NameC = client.Name;
            newC.Email = client.Email;
            newC.BearthDate = client.BearthDate;
            newC.Phone = client.Phone;
            return newC;
        }
        public static List<DTO_Command.Client> toListDtoClients(List<Models.Client> clients)
        {
            List<DTO_Command.Client> newCleints = new List<DTO_Command.Client>();
            foreach (var client in clients)
            {
                newCleints.Add(toDtoClient(client));
            }
            return newCleints;
        }
    }
}
