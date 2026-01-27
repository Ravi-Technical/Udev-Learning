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
}
