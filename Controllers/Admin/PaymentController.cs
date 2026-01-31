using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Udemy_Backend.Interface.Admin;
using Udemy_Backend.Models.Admin;

namespace Udemy_Backend.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentInterface paymentService;
        public PaymentController(IPaymentInterface paymentInterface) {
             paymentService= paymentInterface;
        }

        //**************** PAYMENT CREATE ENDPOINT This is the sample test of azure/github ******************//
        [HttpPost("createOrder")]
        public async Task<IActionResult> CreateNewOrder(CreateOrderRequest req)
        {
            try { 
                 var orderCreated = await paymentService.CreateOrder(req);
                 return Ok(orderCreated);
            }
            catch (ArgumentException ex) { 
               return BadRequest(ex.Message);
            }
        }
        // PAYMENT VERIFY ENDPOINT
        [HttpPost("orderVerify")]
        public async Task<IActionResult> PaymentVerify(VerifyPaymentRequest req)
        {
            try
            {
                var isValid = await paymentService.VerifyOrder(req);
                return Ok(isValid);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
