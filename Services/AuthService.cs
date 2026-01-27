using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Ocsp;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Udemy_Backend.Database;
using Udemy_Backend.Interface;
using Udemy_Backend.Models;

namespace Udemy_Backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly MyDbContext database;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        public AuthService(MyDbContext dbContext, IEmailService emailService, IConfiguration configuration) {
            database = dbContext;
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task<bool> GenerateAndSaveOtp(string email)
        {
            var getCurrentUser = await VerifyUser(email);
            if (getCurrentUser == null) return false;
            var otp = new Random().Next(100000, 999999).ToString();
            var otpEntity = new OtpVerification
            {
                Email = email,
                Otp = otp,
                ExpiryTime = DateTime.UtcNow.AddDays(30),
                IsVerified = false,
            };
            if (email == "ravis3682@gmail.com")
            {
                otpEntity.Role = "Admin";
            }
            await database.OtpVerifications.AddAsync(otpEntity);
            await database.SaveChangesAsync();
            await _emailService.SendEmailAsync(email, "Your OTP Code :", $" Your otp is {otp}");
            return true;
        }

        public string GenerateToken(OtpVerifyModel request)
        {
            if (request == null) throw new Exception("Something went wrong!.");
            var claims = new[]
           {
                new Claim(ClaimTypes.Name, request.Email),
                new Claim(ClaimTypes.Role, request.Role ?? "User"),
            };
            var jwtKey = _configuration["Jwt:Key"]
                     ?? throw new ArgumentNullException("Jwt:Key is missing in appsettings.json");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                  issuer: _configuration["Jwt:Issuer"],
                  audience:null,
                  claims: claims,
                  expires:DateTime.Now.AddDays(30),
                  signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<AuthResponse> VerifyOtp(OtpVerifyModel req)
        {
            var verifyOtp = await database.OtpVerifications.FirstOrDefaultAsync(x =>x.Email == req.Email && x.Otp == req.OTP && x.Role == req.Role);

            if (verifyOtp == null) return new AuthResponse { Success = false, Message = "Invalid OTP!, please try again." };
            
            if (verifyOtp.IsVerified == true) return new AuthResponse { Success = false, Message = "OTP already verified" }; 
            
            var userInfo = await database.Users.FirstOrDefaultAsync(x=>x.Email == req.Email);

            if (verifyOtp != null && userInfo !=null)
            {
                verifyOtp.IsVerified = true;
                await database.SaveChangesAsync();
                var token = GenerateToken(req);
                var userTransformData = new UserDto
                {
                    FullName = userInfo.FullName ?? "",
                    Email = userInfo.Email ?? "",
                    Role = userInfo.Role ?? ""
                };
                return new AuthResponse
                {
                    Success = true,
                    Message = "OTP verified successfully",
                    Token = token, 
                    Data = userTransformData
                };
            } 
            return new AuthResponse
            {
                Success = false,
                Message = "Invalid email or OTP! please try again.",
            };
        }
        public async Task<UserDto?> VerifyUser(string email)
        {
            var data = await database.Users.FirstOrDefaultAsync(u=>u.Email == email);

            if (data == null) return null;
            var modifyRes = new UserDto
            {
                FullName = data.FullName ?? "",
                Email = data.Email ?? "",
                Role = data.Role
            };
            return modifyRes;
        } 
    }
}
