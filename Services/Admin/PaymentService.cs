using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Udemy_Backend.Database;
using Udemy_Backend.Interface.Admin;
using Udemy_Backend.Models.Admin;

namespace Udemy_Backend.Services.Admin
{
    public class PaymentService : IPaymentInterface
    {
        private readonly MyDbContext database;
        public PaymentService(MyDbContext myDbContext) { database = myDbContext; }
        public async Task<CourseOrderResponse> CreateOrder(CreateOrderRequest req)
        {
            throw new NotImplementedException();
            // var courses = await database.Courses.Where(c=>req.CourseIds.Contains(c.Id))
            //    .Select(x=> new {x.Id, x.Price})
            //    .ToListAsync();
            //if (!courses.Any()) throw new ArgumentException("No valid courses found.");
            //var total = courses.Sum(x=>x.Price);
            //var order = new Order
            //{
            //    Id = Guid.NewGuid(),
            //    UserId = req.UserId,
            //    Amount = total,
            //    Status = "Pending"
            //};
            //await database.Orders.AddAsync(order);
            //foreach(var c in courses)
            //{
            //  await database.OrderItems.AddAsync(
            //     new OrderItem
            //     {
            //         Id = Guid.NewGuid(),
            //         OrderId = order.Id,
            //         CourseId = c.Id,
            //         Price = c.Price,
            //     });
            //}
            //await database.SaveChangesAsync();



        }

        public Task<bool> VerifyOrder(VerifyPaymentRequest req)
        {
            throw new NotImplementedException();
        }

    }
}
