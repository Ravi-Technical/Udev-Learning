using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Udemy_Backend.Database;
using Udemy_Backend.Dtos;
using Udemy_Backend.Interface.Admin;
using Udemy_Backend.Models.AdminModel;

namespace Udemy_Backend.Services.Admin
{
    public class CategoryService : ICategory
    {
        private readonly MyDbContext database;
        public CategoryService(MyDbContext myDbContext) { 
           database = myDbContext;
        }

        // Inset New Category 
        public async Task<CourseDtos> AddNewCategory(CourseCategories req)
        {
            var isExist = await database.Categories.AnyAsync(x=>x.CategoryName == req.CategoryName);
            if (isExist) {
                return new CourseDtos
                {
                    Success = false,
                    Message = "Category is already exist",
                };
            }
            var modelMapping = new CourseCategories
            {
                CategoryName = req.CategoryName,
                Status = req.Status,
            };
            await database.AddAsync(modelMapping);
            await database.SaveChangesAsync();
            return new CourseDtos
            {
               
                Success = true,
                Message = "Category has been added successfully"
            };
        }
        // Delete Category 
        public async Task<CourseDtos> DeleteSingleCategory(Guid id)
        {
            var isCategoryExits = await database.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (isCategoryExits == null)
            {
                return new CourseDtos
                {
                    Success = false,
                    Message = "Category not found"
                };
            }
                 database.Categories.Remove(isCategoryExits);
                await database.SaveChangesAsync();
                var result = new CourseDtos
                {
                    Success = true,
                    Message = "Category successfully deleted"
                };
                return result;
        }
        // Get All Category 
        public async Task<List<CourseCategories>> GetAllCategory()
        {
            return await database.Categories.OrderBy(x=>x.CategoryName).ToListAsync();
        }
        // Get Single Category 
        public async Task<CourseCategories?> GetSingleCategory(Guid Id)
        {
            var isExists = await database.Categories.FindAsync(Id);
            return isExists;
        }

        public async Task<CourseDtos> UpdateCategory(Guid id, CourseCategories data)
        {
            var getCategory = await database.Categories.FirstOrDefaultAsync(x=>x.Id==id);
            if (getCategory == null) {
                return new CourseDtos
                {
                    Success = false,
                    Message = "Category not found!"
                };
            }
            getCategory.CategoryName = data.CategoryName;
            await database.SaveChangesAsync();
            return new CourseDtos
            {
                Success = true,
                Message = "Category has been updated successfully"
            };
        }
    }
}
