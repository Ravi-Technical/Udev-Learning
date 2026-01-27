namespace Udemy_Backend.Models.Admin
{
    public class Common_Req_Res_Model
    {
    }

    public class HomeSlider_Domain_Model
    {
        public Guid Id { get; set; }
        public Guid SliderCode { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string Alt_Tag { get; set; } = string.Empty;
        public Boolean Status { get; set; } = true;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

    public class HomeSliderRequest
    {
        public string ImageUrl { get; set; } = string.Empty;
        public string Alt_Tag { get; set; } = string.Empty;
        public Boolean Status { get; set; } = true;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
    public class UpdateSliderRequest
    {
        public string ImageUrl { get; set; } = string.Empty;
        public string Alt_Tag { get; set; } = string.Empty;
        public Boolean Status { get; set; } = true;
    }
    public class SendSliderList
    {  
        public Guid SliderId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string AltTag { get; set; } = string.Empty;
        public DateTime Datetime { get; set; } = DateTime.UtcNow;
        public bool Status { get; set; } = true;
    }
    public class CommonResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public Object? Data { get; set; }
    }

}
