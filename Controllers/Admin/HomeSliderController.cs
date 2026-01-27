using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Udemy_Backend.Interface.Admin;
using Udemy_Backend.Models.Admin;

namespace Udemy_Backend.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeSliderController : ControllerBase
    {
        readonly IHomeSlider homeSlider;
        public HomeSliderController(IHomeSlider _homeSlider) { 
            this.homeSlider = _homeSlider;
        }

        // Add New Slider End Point
        [HttpPost]
        public async Task<IActionResult> AddGenericHomeSlider([FromBody] HomeSliderRequest req) {
            var checkData = await homeSlider.AddNewSliderAsync(req);
            if (checkData != null) { 
              return Ok(checkData);
            }
            return NotFound(checkData);
        }

        // Get Single Slider
        [HttpGet("GetSingleSlider/{id}")]
        public async Task<IActionResult> GetSingleHomeSlider(Guid id)
        {
            var getSlider = await homeSlider.GetSingleSliderAsync(id);
            if (getSlider != null) {
                return Ok(getSlider);
            }
            return NotFound(getSlider);
        }

        // Update slider image
        [HttpPut("UpdateSlider")]
        public async Task<IActionResult> UpdateExistingSlider(Guid id, UpdateSliderRequest req)
        {
            var isExist = await homeSlider.UpdateSliderAsync(id, req);
            if (isExist != null) {
                return Ok(isExist);
            }
            return NotFound(isExist);
        }

        // Get All Slider 
        [HttpGet("GetAllSlider")]
        public async Task<IActionResult> GetAllGenericSlider()
        {
            var holdSlider = await homeSlider.GetAllSliderAsync();
            if (holdSlider != null)
            {
                return Ok(holdSlider);
            }
            return NotFound(holdSlider);
        }

        // Delete Slider 
        [HttpDelete("DeleteSliderById/{id}")]
        public async Task<IActionResult> DeleteGenericSliderById(Guid id)
        {
            var isSliderExist = await homeSlider.DeleteSliderAsync(id);
            if (isSliderExist != null)
            {
                return Ok(isSliderExist);
            }
            return NotFound(isSliderExist);

        }
    }
}
