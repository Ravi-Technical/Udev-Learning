using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Udemy_Backend.Interface.UI;

namespace Udemy_Backend.Controllers.UI
{
    [Route("api/[controller]")]
    [ApiController]
    public class UI_CategoryController : ControllerBase
    {
        private readonly User_ICategory _category;
        public UI_CategoryController(User_ICategory category) {
         _category = category;
        }

        // Get All Category
        [HttpGet("GetAllCategory")]
        public async Task<IActionResult> GetAllCategory()
        {
            var getCat = await _category.GetAllCategory();
            return Ok(getCat);
        }

        // Get Single Category By Id 
        [HttpGet("CategoryById/{id}")]
        public async Task<IActionResult> GetSingleCategoryById(Guid id)
        {
            var isCategoryExist = await _category.GetSingleCategory(id);
            return Ok(isCategoryExist);
        }
    }
}
