using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Udemy_Backend.Dtos;
using Udemy_Backend.Interface;
using Udemy_Backend.Models;

namespace Udemy_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericUserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;
        public GenericUserController(IUserService UService, IAuthService authService, IEmailService emailService) { 
         _userService = UService;
         _authService = authService;
         _emailService = emailService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> VerifyUserRegister(UserDots UData)
        {
            var validUser = await _userService.NewUserRegister(UData);
            if (validUser == null)
            {
                return BadRequest(new { success = false, message = "Email address is already exists" });
            }
            return CreatedAtAction(nameof(VerifyUserRegister), new
            {
                success = true,
                message = "User Successfully added.",
                data = validUser
            });
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] OtpVerifyModel inputData)
        { 
            var isValidOtp = await _authService.VerifyOtp(inputData);
            if (isValidOtp.Token == null) BadRequest(new { success = isValidOtp.Success, message = isValidOtp.Message });
            return Ok(new { status = true, token = isValidOtp.Token, message = isValidOtp.Message, data = isValidOtp.Data });
        }
        [HttpPost("otpSend")]
        public async Task<IActionResult> sendOTP([FromBody] OptRequest req)
        {
            if (string.IsNullOrEmpty(req.email)) return BadRequest(new { message = "Email is required" });
                var result = await _authService.GenerateAndSaveOtp(req.email);
            if (result == false) return NotFound(new { message = "User not found" });
                return Ok(new { message = "OTP sent to email" });
        }
        // Authorized API END Point
        [HttpGet("getUserDetail")]
        [Authorize]
        public async Task<IActionResult> getUserDetails(string email)
        {
            var authHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrEmpty(email)) return BadRequest(new { message = "Email is required" });
            var userData = await _authService.VerifyUser(email);
            if (userData == null) return NotFound(new { message = "User not found" });
            return Ok(userData);
        }
        // Admin Role
        [HttpGet("admin-dashboard")]
        [Authorize(Roles = "Admin")]
        public IActionResult adminDashboard()
        {
            return Ok("Welcome to Admin");
        }

        [HttpGet("userProfile")]
        [Authorize(Roles = "User")]
        public IActionResult userDashboard()
        {
            return Ok("Welcome to User");
        }
    }
}
