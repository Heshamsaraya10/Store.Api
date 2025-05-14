using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public  class PaymentController(IServiceManager serviceManager) : ApiController
    {
        [HttpPost("{basketId}")]
        public async Task<IActionResult> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await serviceManager.PaymentService.CreateOrUpdatePaymentIntentAsync(basketId);
            return Ok(basket);
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> Webhook()
        {
            var request = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            await serviceManager.PaymentService.UpdateOrderPaymentStatusAsync(request , Request.Headers["Stripe-Signature"]);
            return Ok();
        }
    }
}

 