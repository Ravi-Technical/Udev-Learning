using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Udemy_Backend.Interface.UI;
using Udemy_Backend.Models.UI;

namespace Udemy_Backend.Controllers.UI
{
    [Route("api/[controller]")]
    [ApiController]
    public class UI_CourseController : ControllerBase
    {
        private readonly User_ICourse userService;
        public UI_CourseController(User_ICourse _user_ICourse) {
          userService = _user_ICourse;
        }

        [HttpGet("GetAllUICourse")]
        public async Task<IActionResult> GetAllCourse()
        {
             var getCourse = await userService.GetAllCourses();
            if (getCourse != null)
            {
                return Ok(getCourse);
            }
            return NotFound(getCourse);
        }

        // Get Single Course END Point
        [HttpGet("GetCourseById/{id}")]
        public async Task<IActionResult> GetSingleCourse(Guid id)
        {
             var isExist = await userService.GetSingleCourseById(id);
            if (isExist != null) {
                return Ok(isExist);
            }
            return NotFound(isExist);
        }

        // UI SEARCH END POINT
        [HttpPost("search")]
        public async Task<IActionResult> SearchCourse([FromBody] SearchRequest request)
        {
            if (request != null)
            {
                var getSearchResult = await userService.SearchAsync(request);
                if (getSearchResult != null)
                {
                    return Ok(getSearchResult);
                }
            }
            return NotFound(new { Success = false, Message = "Course not found!" });
        }
        // Top Search Filter
        [HttpGet("search/topFilter")]
        public async Task<IActionResult> GetTopSearchResultFilter()
        {
            var result = await userService.GetFilterResult();
            if (result != null) {
                return Ok(result);
            }
            return NotFound(new { Message="Course not found!", Success = false, });
        }

    }
}
