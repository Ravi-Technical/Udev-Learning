using Udemy_Backend.Models.Admin;

namespace Udemy_Backend.Interface.Admin
{
    public interface IHomeSlider
    {
        Task<CommonResponse> AddNewSliderAsync(HomeSliderRequest req); 
        Task<CommonResponse> GetSingleSliderAsync(Guid id);
        Task<List<SendSliderList>> GetAllSliderAsync();
        Task<CommonResponse> UpdateSliderAsync(Guid id, UpdateSliderRequest req);
        Task<CommonResponse> DeleteSliderAsync(Guid id);
    }
}
