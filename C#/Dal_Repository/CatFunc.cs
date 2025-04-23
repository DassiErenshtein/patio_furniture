using Dal_Repository.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
namespace Dal_Repository
{
    public class CatFunc:IDal_Repository.IDal<DTO_Command.Category>
    {
        PatioFurnitureContext db;
        public CatFunc(PatioFurnitureContext dB)
        {
            db = dB;
        }
        public async Task<List<DTO_Command.Category>> selectAllAsync()
        {
            var list1 = await db.Categories.ToListAsync();
            return converters.CategoryConverter.toListDtoCat(list1);
        }
    }
}
