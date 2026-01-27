

using Udemy_Backend.Models.UI;

namespace Udemy_Backend.Interface.UI
{
    public interface User_ICourse
    {
        Task<UIModel> GetAllCourses();
        Task<UIModel> GetSingleCourseById(Guid id);
        Task<CoursePagedResult<CourseSearchDto>> SearchAsync(SearchRequest request);
        Task<List<TopFilterResult>> GetFilterResult();
    }

    public interface IPagedResult<T>
    {
        int Count { get; }
        int Page { get; }
        int PageCount { get; }
        IReadOnlyList<T> Results { get; }
        FilterGroup FilterOptions { get; }
    }

}
