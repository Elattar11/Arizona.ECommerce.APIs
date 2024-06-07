using Arizona.APIs.Errors;
using Arizona.Core.Entities;
using Arizona.Core.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Arizona.APIs.Controllers
{
    [Authorize]
    public class PaymentController : BaseApiController
    {
        private readonly IPaymentService _payment;

        public PaymentController(IPaymentService payment)
        {
            _payment = payment;
        }

        [ProducesResponseType(typeof(CustomerBasket) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse) , StatusCodes.Status400BadRequest)]
        [HttpGet("{basketid}")]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await _payment.CreateOrUpdatePaymentIntent(basketId);
            if (basket is null) return BadRequest(new ApiResponse(400, "An Error With Your Basket"));
            
            return Ok(basket);
        }



    }
}
