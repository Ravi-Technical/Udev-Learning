using Udemy_Backend.Dtos;
using Udemy_Backend.Models;

namespace Udemy_Backend.Interface
{
    public interface IUserService
    {
        Task<UserModel?> NewUserRegister(UserDots data); 
    }
}
