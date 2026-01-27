using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Udemy_Backend.Database;
using Udemy_Backend.Dtos;
using Udemy_Backend.Interface.course;
using Udemy_Backend.Models.AdminModel;

namespace Udemy_Backend.Services.Course
{
    public class CourseServices : ICourse
    { 
        private readonly MyDbContext _dbContext;
        public CourseServices(MyDbContext database) {
            _dbContext = database; 
        }
        // Add New Fresh Course
        public async Task<CourseDtos> Add_New_Course(AddCourseRequest req)
        {
            var isCouseExists = await _dbContext.Courses.AnyAsync(x => x.CourseCode == req.CourseCode);

            if (!isCouseExists) {
                var dataMapping = new AddCourseModel
                {
                    Title = req.Title,
                    Description = req.Description,
                    CourseVideo = req.CourseVideo,
                    CourseContent = req.CourseContent,
                    ThumbnailImage = req.ThumbnailImage,
                    CategoryId = req.CategoryId,
                    Price = req.Price,
                    CourseCode = req.CourseCode,
                    Level = req.Level,
                    Language = req.Language,
                    EnrollCount = req.EnrollCount,
                    IsFeatured = req.IsFeatured,
                    Ratings = req.Ratings,
                    Timestamp = req.Timestamp,
                    TotalHours = req.TotalHours,
                    Instructors = req.Instructors,
                    Status = req.Status,
                };
                await _dbContext.Courses.AddAsync(dataMapping);
                await _dbContext.SaveChangesAsync();
                return new CourseDtos
                {
                    Success = true,
                    Message = "Course has been added successfully",
                }; 
            };

            return new CourseDtos
            {
                Success = false,
                Message = "Course is already exists"
            };

        }

        // Delete API Method
        public async Task<bool> DeleteCourse(Guid id)
        {
            var courseExist = await _dbContext.Courses.FirstOrDefaultAsync(x=>x.Id == id);
            if (courseExist == null) {
                throw new Exception("Course not found");
            } 
             _dbContext.Courses.Remove(courseExist);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        // Pagination Service for Course
        public async Task<PagedResult<AddCourseModel>> GetPagedCoursesAsync(CoursePaginationRequest req)
        {
            var query = _dbContext.Courses.AsQueryable();
            int totalRecords = await query.CountAsync();
            var items = await query.OrderBy(x => x.Id)
                .Skip((req.PageNumber - 1) * req.PageSize) // (1-1) * 5 = 0 
                .Take(req.PageSize) // 5
                .ToListAsync();
            return new PagedResult<AddCourseModel>
            {
                Items = items,
                TotalCount = totalRecords, 
            };
        }

        // Get Single Course By ID 
        public async Task<AddCourseModel?> GetSingleCourseById(Guid id)
        {
            var isCourseExits = await _dbContext.Courses.FirstOrDefaultAsync(x=>x.Id == id);
            if (isCourseExits == null) {
                return null;
            }
            return isCourseExits;
        }

        // Get All Course 
        public async Task<List<AddCourseModel>?> Get_All_Course()
        {
            var data = await _dbContext.Courses.ToListAsync();
            if(data != null)
            {
                return data;
            }
            return null;
        }

        // Update Course By ID
        public async Task<CourseResponse?> UpdateCourse(Guid id, AddCourseRequest data)
        {
            var isExist = await _dbContext.Courses.FirstOrDefaultAsync(x => x.Id == id);
            if (isExist == null)
            {
                return new CourseResponse
                {
                    Status = false,
                    Message = "Course not found!",
                };
            }
            isExist.Title = data.Title;
            isExist.Description = data.Description;
            //isExist.CourseCode = data.CourseCode;
            isExist.CourseVideo = data.CourseVideo;
            isExist.CourseContent = data.CourseContent;
            isExist.ThumbnailImage = data.ThumbnailImage;
            isExist.CategoryId = data.CategoryId;
            isExist.Price = data.Price;
            isExist.Level = data.Level;
            isExist.Language = data.Language;
            isExist.EnrollCount = data.EnrollCount;
            isExist.IsFeatured = data.IsFeatured;
            isExist.CertificateAvailable = data.CertificateAvailable;
            isExist.Ratings = data.Ratings;
            isExist.LastUpdate = data.LastUpdate;
            isExist.TotalHours = data.TotalHours;
            isExist.Instructors = data.Instructors;
            isExist.Status = data.Status;
            await _dbContext.SaveChangesAsync();
            return new CourseResponse
            {
                Status = true,
                Message = "Course has been updated successfully",
            };
        }
    }
}
