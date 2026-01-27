using Microsoft.EntityFrameworkCore;
using Udemy_Backend.Database;
using Udemy_Backend.Dtos;
using Udemy_Backend.Interface;
using Udemy_Backend.Models;

namespace Udemy_Backend.Services
{
    public class UserService : IUserService
    {
        private readonly MyDbContext _dbContext;
        private readonly IAuthService _authService;
        public UserService(MyDbContext UserDB, IAuthService authService) { 
          _dbContext = UserDB;
          _authService = authService;
        }

        public async Task<UserModel?> NewUserRegister(UserDots data)
        {
            var isValid = await _dbContext.Users.FirstOrDefaultAsync(d=>d.Email == data.Email);
            if (isValid != null) return null;
            var userModify = new UserModel
            {
                FullName = data.FullName,
                Email = data.Email
            };
            await _dbContext.Users.AddAsync(userModify);
            await _dbContext.SaveChangesAsync();
            return userModify;
        }

         
    }
}
