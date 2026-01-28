using Udemy_Backend.Models.Admin;

namespace Udemy_Backend.Interface.Admin
{
    public interface IPaymentInterface
    {
        //*************************** PAYMENT GETEWAY INTERFACE ********************************//
        Task<CourseOrderResponse> CreateOrder(CreateOrderRequest req);
        Task<bool> VerifyOrder(VerifyPaymentRequest req);
    }
}
