using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Udemy_Backend.Interface.Admin;
using Udemy_Backend.Models.AdminModel;

namespace Udemy_Backend.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategory categoryService;
        public CategoryController(ICategory category)
        {
            categoryService = category;
        }
        // Insert New Category Here
        [HttpPost("addNewCategory")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GenericAddNewCategory([FromBody] CourseCategories req)
        {
            if (req.CategoryName == null) { return BadRequest(new { message = "Please enter the all fields" }); };
            var checks = await categoryService.AddNewCategory(req);
            if (checks != null)
            {
                return Ok(checks);
            }
            return BadRequest(new { message = "Something went wrong!" });
        }
        // Get All Generic Category
        [HttpGet("GetAllCategory")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllGenericCategory()
        {
            var getCategories = await categoryService.GetAllCategory();
            if (getCategories != null)
            {
                return Ok(getCategories);
            }
            return BadRequest(new { message = "Something went wrong!" });
        }
        // Get Single Generic Category
        [HttpGet("singleCategory/{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetSingleGenericCategory(Guid Id)
        {
            var getCategory = await categoryService.GetSingleCategory(Id);
            if (getCategory != null)
            {
                return Ok(getCategory);
            }
            return BadRequest(new { message = "Category Not Found!" });
        }
        // Delete Single Generic Category
        [HttpDelete("deleteCategory/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSingleRowOfCategory(Guid id)
        {
            var getCategory = await categoryService.DeleteSingleCategory(id);
            if (getCategory != null)
            {
                return Ok(getCategory);
            }
            return BadRequest(new { message = "Not deleted" });
        }
        // Delete Single Generic Category
        [HttpPut("updateCategory/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateSingleCategory(Guid id, [FromBody] CourseCategories data)
        {
             var fetchCategory = await categoryService.UpdateCategory(id, data);
             if (fetchCategory != null) { 
                 return Ok(fetchCategory);
              }
            return BadRequest(new {message = "Something went wrong!"});
        }



    }  // END CLASS HERE
} // NAME SPACE HERE
