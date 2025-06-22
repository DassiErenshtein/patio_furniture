using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal_Repository.converters
{
    public class CategoryConverter
    {
        public static DTO_Command.Category toDtoCat(Models.Category category)
        {
            DTO_Command.Category newCat = new DTO_Command.Category();
            newCat.Id = category.Id;
            newCat.NameC = category.NameC;
            newCat.Img = category.Img;
            return newCat;
        }
        public static List<DTO_Command.Category> toListDtoCat(List<Models.Category> categories)
        {
            List<DTO_Command.Category> newCats = new List<DTO_Command.Category>();
            foreach (var category in categories)
            {
                newCats.Add(toDtoCat(category));
            }
            return newCats;
        } 
        

    }
}
