namespace Udemy_Backend.Models.Admin
{
    public class AdminCountryAddModel
    {
        public string? Name { get; set; }
        public string? CountryCode { get; set; }
    }
    public class AdminCommonResponseModel<T>
    {
        public Boolean? Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
    public class AdminStateAddModel
    {
        public string? StateName { get; set; }
        public string? StateCode { get; set; } 
        public Guid CountryId { get; set; }
    }
    public class StateDto
    {
        public Guid Id { get; set; }
        public string? StateName { get; set; }
        public string? StateCode { get; set; }
        public CountryMiniDto? Country { get; set; }
        public Guid? CountryId { get; set; }
        public DateTime? Timestamp { get; set; }
        public bool? IsActive { get; set; }
    }

    public class CountryMiniDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }
        public string? CountryCode { get; set; }
    }
    public class CountryDto
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? CountryCode { get; set; }
        public ICollection<State> State { get; set; } = new List<State>();
        public DateTime Timestamp { get; set; }
        public bool? IsActive { get; set; }
    }

    //************************************* Payment Gateway Request/Response Model *************************************//
    public class CourseOrderResponse
    {
        public Guid OrderId { get; set; }
        public string RazorpayOrderId { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public string RazorpayKey { get; set; } = null!;
    }
    public class CreateOrderRequest
    {
        public Guid UserId { get; set; }
        public List<Guid> CourseIds { get; set; } = new();
    }
    public class VerifyPaymentRequest
    {
        public Guid OrderId { get; set; }
        public string RazorpayOrderId { get; set; } = null!;
        public string RazorpayPaymentId { get; set; } = null!;
        public string RazorpaySignature { get; set; } = null!;
    }
    public class UserCourseDto
    {
        public Guid UserId { get; set; }
        public List<Guid> CourseIds { get; set; } = new List<Guid>();

    }

} // END MAIN CLASS HERE
