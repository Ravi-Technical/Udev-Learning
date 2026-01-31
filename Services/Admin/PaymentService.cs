using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Razorpay.Api;
using System.Security.Cryptography;
using System.Text;
using Udemy_Backend.Database;
using Udemy_Backend.Interface.Admin;
using Udemy_Backend.Models.Admin;

namespace Udemy_Backend.Services.Admin
{
    public class PaymentService : IPaymentInterface
    {
        private readonly MyDbContext database;
        private readonly IConfiguration _config;
        public PaymentService(MyDbContext myDbContext, IConfiguration configuration) {
            database = myDbContext;
            _config = configuration;
        }
        public async Task<CourseOrderResponse> CreateOrder(CreateOrderRequest req)
        {
            // throw new NotImplementedException();
            var courses = await database.Courses.Where(c => req.CourseIds.Contains(c.Id))
               .Select(x => new { x.Id, x.Price })
               .ToListAsync();
            if (!courses.Any()) throw new ArgumentException("No valid courses found.");
            var total = courses.Sum(x => x.Price);
            var order = new Udemy_Backend.Models.Admin.Order
            {
                Id = Guid.NewGuid(),
                UserId = req.UserId,
                Amount = total,
                Status = "Pending"
            };
            await database.Orders.AddAsync(order);
            foreach (var c in courses)
            {
                await database.OrderItems.AddAsync(
                   new OrderItem
                   {
                       Id = Guid.NewGuid(),
                       OrderId = order.Id,
                       CourseId = c.Id,
                       Price = c.Price,
                   });
            }
            await database.SaveChangesAsync();
            // Create Razorpay Order
            var client = new RazorpayClient(
                  _config["Razorpay:Key"],
                  _config["Razorpay:Secret"]
                );
            var options = new Dictionary<string, object> {
                {"amount", (int)(total * 100)},
                {"currency", "INR" },
                {"receipt", order.Id.ToString()},
            };
            var rzpOrder = client.Order.Create(options);
            order.RazorpayOrderId = rzpOrder["id"].ToString();
            await database.SaveChangesAsync();
            return new CourseOrderResponse
            {
                 OrderId = order.Id,
                 RazorpayOrderId= order.RazorpayOrderId,
                 TotalAmount = total,
                 RazorpayKey = _config["Razorpay:Key"]!
            };
        }
        public async Task<bool> VerifyOrder(VerifyPaymentRequest req)
        {
            var order = await database.Orders.Include(x=>x.Items).FirstOrDefaultAsync(od=>od.Id == req.OrderId);

            if (order == null) throw new ArgumentException("Order not found!");

            string payload = req.RazorpayOrderId + "|" + req.RazorpayPaymentId;

            string secret = _config["Razorpay:Secret"]!;

            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret)); 

            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));

            var generated = Convert.ToHexString(hash).ToLower();

            if (generated != req.RazorpaySignature) throw new ArgumentException("Invalid signature");

            order.Status = "Paid";

            order.TransactionId = req.RazorpaySignature;

            order.PaidOn = DateTime.UtcNow;

            foreach (var item in order.Items) {

                database.UserCourses.Add(new UserCourse {
                   UserId = order.UserId,
                   CourseIds = item.CourseId
                });

            }
            await database.SaveChangesAsync();
            
            return true;
        }

    }
}
