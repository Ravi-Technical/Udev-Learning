using Udemy_Backend.Models.UI;

namespace Udemy_Backend.Interface.UI
{
    public interface User_ICategory
    {
        Task<UIModel> GetAllCategory();
        Task<UIModel> GetSingleCategory(Guid categoryId);
    }
}
