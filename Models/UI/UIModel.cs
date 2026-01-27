using Udemy_Backend.Interface.UI;
using Udemy_Backend.Models.AdminModel;

namespace Udemy_Backend.Models.UI
{
    public class UIModel
    {
        public Boolean Success { get; set; }
        public string? Message { get; set; }
        public Object? Data { get; set; }
    }

    public class UISearch
    {
        public Guid Id { get; set; }
        public string SearchKey { get; set; } = string.Empty;
        public DateTime DateTime { get; set; } = DateTime.Now;
    }

    public class CourseSearchRequest {
        public string Keyword { get; set; } = string.Empty;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class SearchResponse
    {
        public int? TotalCount { get; set; }
        public Boolean? Success { get; set; }
        public string? Message { get; set; }
        public List<AddCourseModel> Result {get;set;} = new();
    }


    // *********************************** Test Searching Models ********************************* //
    public class CourseSearchDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CourseVideo { get; set; } = string.Empty.ToString();
        public string CourseContent { get; set; } = string.Empty;
        public string ThumbnailImage { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public int Price { get; set; }
        public string CourseCode { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public string Language { get; set; } = "English";
        public int EnrollCount { get; set; }
        public bool IsFeatured { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool CertificateAvailable { get; set; }
        // Navigation Property  
        public int Ratings { get; set; }
        public DateTime Timestamp { get; set; }
        public int TotalHours { get; set; }
        public string Instructors { get; set; } = string.Empty;
        public bool Status { get; set; } = true;
    }

    // ********************************* Send to response/request of Search/Filter Result ************************************//
    public class SearchRequest
    {
        public string Keyword { get; set; } = string.Empty;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 12;
        public TopFilterRequest? Filters { get; set; }
    }
    public class TopFilterRequest
    {
         public List<string>? Level { get; set; }
         public List<string>? Language { get; set; }
         public List<int>? Rating { get; set; }
         public List<string>? Hours { get; set; } 
         public List<string>? VideoLength { get; set; }
    }

    public class CourseSearchFilterResponse
    {
        public List<CourseSearchDto>? Items { get; set; }
        public int TotalCount { get; set; }
    }
    // Testing
    public class SearchAndFilterResponse
    {
        public string? Keyword { get; set; }
        public int Page { get; set; } = 0;
        public int PageSize { get; set; } = 24;
        public string SortOrder { get; set; } = "RELEVANCE";
        public List<string>? Levels { get; set; }
        public List<string>? Languages { get; set; }
        public List<int>? Ratings { get; set; }
    }

    public class CoursePagedResult<T>
    {
        public int Count { get; set; }
        public int Page { get; set; }
        public int PageCount { get; set; }
        public List<T>? Results { get; set; }
        public FilterGroup? FilterOptions { get; set; }
    }
    public class FilterGroup
    {
        public List<FilterOption>? Levels { get; set; }
        public List<FilterOption>? Languages { get; set; }
        public List<FilterOption>? Ratings { get; set; }
    }

    public class FilterOption
    {
        public string? Label { get; set; }
        public string? Key { get; set; }
        public int Count { get; set; }
    }
























    // Result for response model 
    public class TopFilterResult
    {
        public string? FilterType { get; set; }
        public string? FilterValue { get; set; }
        public int TotalCount { get; set; }
    }
}
