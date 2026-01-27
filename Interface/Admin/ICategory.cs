using Udemy_Backend.Dtos;
using Udemy_Backend.Models.AdminModel;

namespace Udemy_Backend.Interface.Admin
{
    public interface ICategory
    {
        Task<CourseDtos> AddNewCategory(CourseCategories req);
        Task<List<CourseCategories>> GetAllCategory();
        Task<CourseCategories?> GetSingleCategory(Guid id);
        Task<CourseDtos> DeleteSingleCategory(Guid id);
        Task<CourseDtos> UpdateCategory(Guid id, CourseCategories data);
    }
}
