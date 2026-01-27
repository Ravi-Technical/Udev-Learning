using Microsoft.EntityFrameworkCore;
using Udemy_Backend.Database;
using Udemy_Backend.Interface.Admin;
using Udemy_Backend.Models.Admin;

namespace Udemy_Backend.Services.Admin
{
    public class HomeSlider_Service : IHomeSlider
    {
        private readonly MyDbContext database;
        public HomeSlider_Service(MyDbContext db) { 
          database = db;
        }

        // Get Single Slider
        public async Task<CommonResponse> GetSingleSliderAsync(Guid id)
        {
            var getSliderById = await database.HomeSlider.FirstOrDefaultAsync(x => x.Id == id);
            if (getSliderById == null) {
                return new CommonResponse
                {
                    Success = false,
                    Data = null,
                    Message = "Slider not found!"
                };
            }
            return new CommonResponse
            {
                Success = true,
                Data = getSliderById,
                Message = "Slider fetched successfully"
            };
        }
        // Add new fresh slider
        public async Task<CommonResponse> AddNewSliderAsync(HomeSliderRequest req)
        {
            var data = new HomeSlider_Domain_Model
            {
                SliderCode = Guid.NewGuid(),
                ImageUrl = req.ImageUrl,
                Alt_Tag = req.Alt_Tag,
                Status = req.Status,
            };
            await database.HomeSlider.AddAsync(data);
            await database.SaveChangesAsync();
            return new CommonResponse {
                Data = data,
                Success = true,
                Message = "Home slider has been added successfully"
            };
        }
        // Update slider 
        public async Task<CommonResponse> UpdateSliderAsync(Guid id, UpdateSliderRequest req)
        {
            var findSlider = await database.HomeSlider.FirstOrDefaultAsync(x => x.Id == id);
            if (findSlider != null) {
                findSlider.ImageUrl = req.ImageUrl;
                findSlider.Alt_Tag = req.Alt_Tag;   
                findSlider.Status = req.Status;
                await database.SaveChangesAsync();
                return new CommonResponse
                {
                    Success = true,
                    Data = findSlider,
                    Message = "Slider updated successfully"
                };
            }
            return new CommonResponse
            {
                Success = false,
                Data = null,
                Message = "Something went wrong!"
            };
        }
        public async Task<List<SendSliderList>> GetAllSliderAsync()
        {
            return await database.HomeSlider
                .Select(x => new SendSliderList
                {
                    SliderId = x.Id,
                    ImageUrl = x.ImageUrl,
                    AltTag = x.Alt_Tag,
                    Status = x.Status,
                    Datetime = x.Timestamp 
                }).ToListAsync();
        }
        public async Task<CommonResponse> DeleteSliderAsync(Guid id)
        {
            var isExistSlider = await database.HomeSlider.FindAsync(id);
            if (isExistSlider != null) {
                database.HomeSlider.Remove(isExistSlider);
                await database.SaveChangesAsync();
                return new CommonResponse
                {
                    Success = true,
                    Message = "Slider deleted successfully"
                };
            }
            return new CommonResponse {
                Success = false,
                Message = "Slider not found"
            };
        }
    }
}
