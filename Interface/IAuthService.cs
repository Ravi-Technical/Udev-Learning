using Udemy_Backend.Models;

namespace Udemy_Backend.Interface
{
    public interface IAuthService
    {
        Task<UserDto?> VerifyUser(string email);
        Task<bool> GenerateAndSaveOtp(string email);
        Task<AuthResponse> VerifyOtp(OtpVerifyModel req);
        string GenerateToken(OtpVerifyModel req);
    }
}
