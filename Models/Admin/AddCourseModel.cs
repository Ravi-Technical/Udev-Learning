using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Udemy_Backend.Models.AdminModel;

namespace Udemy_Backend.Models.AdminModel
{
    public class AddCourseModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CourseVideo { get; set; } = string.Empty.ToString();
        public string CourseContent { get; set; } = string.Empty;
        public string ThumbnailImage {  get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public int Price { get; set; }
        public string CourseCode { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public string Language {  get; set; } = "English";
        public int EnrollCount { get; set; } = 0;
        public bool IsFeatured { get; set; } = false;
        public DateTime LastUpdate {  get; set; } = DateTime.UtcNow;
        public bool CertificateAvailable { get; set; } = true; 
        // Navigation Property  
        public int Ratings { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public int TotalHours { get; set; }
        public string Instructors { get; set; } = string.Empty;
        public bool Status { get; set; } = true;
    }
 
    public class CourseCategories
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public bool Status { get; set; } = true;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

}
/// Request Dto <summary>
public class AddCourseRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string CourseVideo { get; set; } = string.Empty.ToString();
    public string CourseContent { get; set; } = string.Empty;
    public string ThumbnailImage { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public int Price { get; set; }
    public string CourseCode { get; set; } = string.Empty;
    public string Level { get; set; } = "All Level";
    public string Language { get; set; } = "English";
    public int EnrollCount { get; set; } = 0;
    public bool IsFeatured { get; set; } = false;
    public DateTime LastUpdate { get; set; } = DateTime.UtcNow;
    public bool CertificateAvailable { get; set; } = true;
    // Navigation Property  
    public int Ratings { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public int TotalHours { get; set; }
    public string Instructors { get; set; } = string.Empty;
    public bool Status { get; set; } = true;
 
}

public class CourseCategoryRequest
{
    public string CategoryName { get; set; } = string.Empty;
    public bool Status { get; set; } = true;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}

public class CourseResponse
{
   public string Message {  get; set; } = string.Empty;
   public bool Status { get; set; } 

}

public class CoursePaginationRequest
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 5;
    //public string? Search { get; set; } = "";
}

public class PagedResult<T>
{
    public List<T>? Items { get; set; }
    public int TotalCount { get; set; }

}




































