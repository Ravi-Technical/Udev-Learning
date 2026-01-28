namespace Udemy_Backend.Models.Admin
{
    public class Country
    {
         public Guid Id { get; set; }
         public string Name { get; set; } = string.Empty;
         public string CountryCode { get; set; } = string.Empty;
         public ICollection<State> State { get; set; } = new List<State>();
         public DateTime  Timestamp { get; set; } = DateTime.UtcNow;
         public Boolean IsActive { get; set; } = true;
    }
    public class State
    {
        public Guid Id { get; set; }
        public string? StateName { get; set; }
        public string? StateCode { get; set; } 
        public Guid CountryId { get; set; }
        public Country? Country { get; set; } = null;
        public DateTime? Timestamp { get; set; } = DateTime.UtcNow;
        public Boolean IsActive { get; set; } = true;
    }
    //*************************************** Order Models **********************************************//
    public class Order { 
         public Guid Id { get; set; }
         public Guid UserId { get; set; }
         public decimal Amount { get; set; }
         public string Status { get; set; } = "Pending";
         public string? RazorpayOrderId { get; set; }
         public string? TransactionId { get; set; }
         public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
         public DateTime? PaidOn { get; set; }
         public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
    public class OrderItem
     {
         public Guid Id { get; set; }
         public Guid OrderId { get; set; }
         public Guid CourseId { get; set; }
         public decimal Price { get; set; }
     }
    public class UserCourse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public List<Guid> CourseIds { get; set; } = new List<Guid>();
        public bool IsActive { get; set; } = true;
        public DateTime CreatedOn { get; set;} = DateTime.UtcNow;

    }

}
