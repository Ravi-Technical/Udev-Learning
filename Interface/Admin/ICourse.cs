using Udemy_Backend.Dtos;
using Udemy_Backend.Models.AdminModel;

namespace Udemy_Backend.Interface.course
{
    public interface ICourse
    {
        Task<CourseDtos> Add_New_Course(AddCourseRequest req);
        Task <List<AddCourseModel>?> Get_All_Course();
        Task<AddCourseModel?> GetSingleCourseById(Guid id);
        Task<CourseResponse?> UpdateCourse(Guid id, AddCourseRequest data);
        Task<bool> DeleteCourse(Guid id);
        // New Get Course By Params
        Task<PagedResult<AddCourseModel>> GetPagedCoursesAsync(CoursePaginationRequest req);
    }
}
