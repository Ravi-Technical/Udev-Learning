using Microsoft.EntityFrameworkCore;
using Udemy_Backend.Database;
using Udemy_Backend.Interface.UI;
using Udemy_Backend.Models.UI;

namespace Udemy_Backend.Services.UI
{
    public class UIService:User_ICategory
    {
        private readonly MyDbContext db;
        public UIService(MyDbContext _db) {
              db = _db;
        }

        public async Task<UIModel> GetAllCategory()
        {
            var getCategory = await db.Categories.ToListAsync();
            if (getCategory == null) {
                return new UIModel
                {
                    Success = false,
                    Message = "Category not found",
                    Data = null
                };
            }
            return new UIModel
            {
                Success = true,
                Message = "",
                Data = getCategory
            };
        }

        // Get Single Category By Id
        public async Task<UIModel> GetSingleCategory(Guid categoryId)
        {
            var category = await db.Categories.SingleOrDefaultAsync(x => x.Id == categoryId);
            if (category == null) {
                return new UIModel
                {
                    Success = false,
                    Message = "Category not found!",
                    Data = null
                };
            }
            return new UIModel
            {
                Success = true,
                Message = "Record Success",
                Data = category
            };
        }
    }
}
