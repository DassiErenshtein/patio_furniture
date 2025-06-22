using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal_Repository.converters
{
    public class CompanyConverter
    {
        public static DTO_Command.Company toDtoComp(Models.Company company)
        {
            DTO_Command.Company newC = new DTO_Command.Company();
            newC.Id = company.Id;
            newC.NameC = company.NameC;
            return newC;
        }
        public static List<DTO_Command.Company> toListDtoComp(List<Models.Company> companies)
        {
            List<DTO_Command.Company> newComps = new List<DTO_Command.Company>();
            foreach (var company in companies)
            {
                newComps.Add(toDtoComp(company));
            }
            return newComps;
        }
    }
}
