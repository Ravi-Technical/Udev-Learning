using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection.Emit;
using Udemy_Backend.Database;
using Udemy_Backend.Interface.course;
using Udemy_Backend.Interface.UI;
using Udemy_Backend.Models.AdminModel;
using Udemy_Backend.Models.UI;

namespace Udemy_Backend.Services.UI
{
    public class UICourseService:User_ICourse
    {
        private readonly MyDbContext database;
        private readonly IConfiguration _configuration;
        public UICourseService(MyDbContext _db, IConfiguration configuration) {
            database = _db;
            _configuration = configuration;
        }
        //************************************ Get All Course ***********************************//
        public async Task<UIModel> GetAllCourses()
        {
            var getCourse = await database.Courses.ToListAsync();
            if (getCourse == null) {
                return new UIModel
                {
                    Success = false,
                    Message = "Course not found",
                    Data = null
                };
            }
            return new UIModel
            {
                Success = true,
                Message = "All course listed success",
                Data = getCourse
            };
        }
        //************************************ Get Filtered Result ****************************** //
        public async Task<List<TopFilterResult>> GetFilterResult()
        {
            var result = new List<TopFilterResult>();
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var command = new SqlCommand("sp_GetCourseFilterCounts", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync()) {
                result.Add(
                         new TopFilterResult
                         {
                             FilterType = reader.GetString(0),
                             FilterValue = reader.GetString(1),
                             TotalCount = reader.GetInt32(2)
                         }
                    );
            }
            return result;
        }
        //********************************** Get Single Course By Id ***********************************//
        public async Task<UIModel> GetSingleCourseById(Guid id)
        {
            var getSingleCourse = await database.Courses.FindAsync(id);
            if (getSingleCourse == null) {
                return new UIModel {
                    Success = false,
                    Message = "Course not found!",
                    Data = null
                };
            }
            return new UIModel {
                Success = true,
                Message = "Course success",
                Data = getSingleCourse,
            };
        }
        //***************************** Test Methods For Search Course *******************************//
        public async Task<CoursePagedResult<CourseSearchDto>> SearchAsync([Service] SearchRequest request)
        {
            var query = from course in database.Courses
                        join category in database.Categories
                        on course.CategoryId equals category.Id
                        where
                            EF.Functions.Collate(course.Title, "Latin1_General_CI_AS").Contains(request.Keyword)
                         || EF.Functions.Collate(course.Description, "Latin1_General_CI_AS").Contains(request.Keyword)
                         || EF.Functions.Collate(category.CategoryName, "Latin1_General_CI_AS").Contains(request.Keyword)
                        select new { course, category };
            if(request?.Filters?.Level?.Any()==true)
            {
                query = query.Where(x=>request.Filters.Level.Contains(x.course.Level));
            }
            if(request?.Filters?.Language?.Any()==true)
            {
                query = query.Where(x=>request.Filters.Language.Contains(x.course.Language));
            }
            if (request?.Filters?.Rating?.Any() == true)
            {
                query = query.Where(x =>x.course.Ratings >= request.Filters.Rating.Min());
            }
            var totalCount = await query.CountAsync();
            var filterGroup = new FilterGroup
            {
                  Levels = await query.GroupBy(x => x.course.Level).Select(l =>
                  new FilterOption
                  {
                      Label = l.Key,
                      Key = l.Key,
                      Count = l.Count()
                  }).ToListAsync(),

                  Languages = await query.GroupBy(x=>x.course.Language).Select(l=>
                  new FilterOption
                  {
                      Label= l.Key,
                      Key = l.Key,
                      Count = l.Count()
                  }).ToListAsync(),

                Ratings = await query .GroupBy(x =>
                x.course.Ratings >= 5 ? 5 :
                x.course.Ratings >= 4 ? 4 :
                x.course.Ratings >= 3 ? 3 : 3)
                .Select(r => new FilterOption
                {
                Label = r.Key.ToString(),
                Key = r.Key.ToString(),          // ✅ string
                Count = r.Count()
                })
                .ToListAsync()
            };
            var items = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new CourseSearchDto
                {
                    Id = x.course.Id,
                    Title = x.course.Title,
                    Description = x.course.Description,
                    CourseVideo = x.course.CourseVideo,
                    CourseContent = x.course.CourseContent,
                    ThumbnailImage = x.course.ThumbnailImage,
                    CategoryId = x.course.CategoryId,
                    CategoryName = x.category.CategoryName,
                    Price = x.course.Price,
                    CourseCode = x.course.CourseCode,
                    Level = x.course.Level,
                    Language = x.course.Language,
                    EnrollCount = x.course.EnrollCount,
                    IsFeatured = x.course.IsFeatured,
                    LastUpdate = x.course.LastUpdate,
                    CertificateAvailable = x.course.CertificateAvailable,
                    Ratings = x.course.Ratings,
                    Timestamp = x.course.Timestamp,
                    TotalHours = x.course.TotalHours,
                    Instructors = x.course.Instructors,
                    Status = x.course.Status,

                }).ToListAsync();
            if (items.Count > 0 && !string.IsNullOrWhiteSpace(request.Keyword))
            {
                var data = new UISearch {SearchKey = request.Keyword};
                if (data != null) { await database.UISearches.AddAsync(data); await database.SaveChangesAsync();}
            }
            return new CoursePagedResult<CourseSearchDto>
            {
                Count = totalCount,
                Page = request.Page,
                PageCount = (int)Math.Ceiling(totalCount / (double)request.PageSize),
                Results = items,
                FilterOptions = filterGroup
            };
        }
    }
}
