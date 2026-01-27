using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Udemy_Backend.Interface.course;
using Udemy_Backend.Models.AdminModel;

namespace Udemy_Backend.Controllers.Course
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourse _course;
        public CourseController(ICourse course) {
            _course = course;
        }

        //**************************** Add New Course ***********************//
        [HttpPost("AddNewCourse")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddGenericCourse([FromBody] AddCourseRequest data)
        {
            if (data == null) return BadRequest(new {message = "Please enter all fields"});
            var isAdded = await _course.Add_New_Course(data);
            if (isAdded != null) { 
               return Ok(isAdded);
            }
            return BadRequest(new { message = "Something went wrong!." });
        }

        //**************************** Get All Course ***********************//
        [HttpGet("GetAllCourse")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllGenericCourse()
        {
            var allCourse = await _course.Get_All_Course();
            if (allCourse == null)
            {
                return BadRequest(new { message = "No courses found." });
            }
            return Ok(allCourse);
        }

        //**************************** Get All Course ***********************//
        [HttpGet("AllGenericCourse")]
        public async Task<IActionResult> GetAllGenericCoursesForUI()
        {
            var allCourse = await _course.Get_All_Course();
            if (allCourse == null)
            {
                return BadRequest(new { message = "No courses found." });
            }
            return Ok(allCourse);
        }

        //**************************** Get Single Course By Id ***********************//
        [HttpGet("GetSingleCourse/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCourseById(Guid id)
        {
            var courseExist = await _course.GetSingleCourseById(id);
            if (courseExist == null) {
                return BadRequest(new {message = "No course found." });
            }
            return Ok(courseExist);
        }

        //**************************** Update Single Course By Id ***********************//
        [HttpPut("updateCourse/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateExistingCourse(Guid id, [FromBody] AddCourseRequest data)
        {
            var updateCourse = await _course.UpdateCourse(id, data);
            if (updateCourse == null) {
                return BadRequest(updateCourse);
            }
            return Ok(updateCourse);
        }

        //**************************** Delete Single Course By Id ***********************//
        [HttpDelete("removeCourse/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteExistingCourse(Guid id)
        {
            var course = await _course.DeleteCourse(id);
            if (!course) { 
               return NotFound(course);
            }
            return Ok(course);
        }

        //**************************** Get Course List by page-number & page size ***********************//
        [HttpGet("paged")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetPaged([FromQuery] CoursePaginationRequest request)
        {
            var response = await _course.GetPagedCoursesAsync(request);
            return Ok(response);
        }

    }
}

