using Arizona.APIs.Errors;
using Arizona.Core.Entities;
using Arizona.Core.Entities.OrderAggregate;
using Arizona.Core.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace Arizona.APIs.Controllers
{
    
    public class PaymentController : BaseApiController
    {
        // This is your Stripe CLI webhook secret for testing your endpoint locally.
        private const string whSecret = "whsec_93e40888c932e8f54f209263e0135df41a161461b33110646b8e3f6f10d0253f";


        private readonly IPaymentService _payment;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IPaymentService payment , ILogger<PaymentController> logger)
        {
            _payment = payment;
            _logger = logger;
        }

        [Authorize]
        [ProducesResponseType(typeof(CustomerBasket) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse) , StatusCodes.Status400BadRequest)]
        [HttpPost("{basketid}")]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await _payment.CreateOrUpdatePaymentIntent(basketId);
            if (basket is null) return BadRequest(new ApiResponse(400, "An Error With Your Basket"));
            
            return Ok(basket);
        }

        
        [HttpPost("webhook")]
        public async Task<IActionResult> WebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], whSecret , 300 , false);

            var paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
            // Handle the event
            Order? order;

            switch (stripeEvent.Type)
            {
                case Events.PaymentIntentSucceeded:
                    order = await _payment.UpdateOrderStatus(paymentIntent.Id, true);
                    _logger.LogInformation("Order is Succeeded {0}", order?.PaymentInternId);
                    _logger.LogInformation("Unhandled event type: {0}", stripeEvent.Type);
                    break;

                case Events.PaymentIntentPaymentFailed:
                    order = await _payment.UpdateOrderStatus(paymentIntent.Id, false);
                    _logger.LogInformation("Order is Failed {0}", order?.PaymentInternId);
                    _logger.LogInformation("Unhandled event type: {0}", stripeEvent.Type);
                    break;
            }

            return Ok();
        }



    }
}
