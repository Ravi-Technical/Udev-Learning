namespace Udemy_Backend.Models
{
    public class UserModel
    {
         public Guid Id { get; set; }
         public string? FullName { get; set; }
         public string? Email { get; set; }
         public string Role { get; set; } = "User";

    }

    public class LoginModel
    {
        public string? Email { get; set; }
    }

    public class OtpVerification
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Email { get; set; }
        public string? Otp { get; set; }
        public DateTime ExpiryTime { get; set; }
        public string Role { get; set; } = "User";
        public bool IsVerified { get; set; }
    }

    public class EmailSettings
    {
        public string? SmtpServer { get; set; }
        public int Port { get; set; }
        public string SenderName { get; set; } = string.Empty;
        public string SenderEmail { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class OtpVerifyModel
    {
        public string Email { get; set; } = string.Empty;
        public string OTP { get; set; } = string.Empty;
        public string? Role { get; set; } = string.Empty;
    }

    public class AuthResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? Token { get; set; }
        public UserDto? Data { get; set; }
    }

    public class OptRequest
    {
        public string email { get; set; } = string.Empty;
    }

    public class UserDto
    {
        public Guid Id {get;set;}
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Role { get; set; }
    }

}
