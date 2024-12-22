using Microsoft.AspNetCore.Mvc;
using VivesRental.Sdk;
using VivesRental.Services.Model.Filters;
using VivesRental.Services.Model.Results;

namespace VivesRental.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderSdk _orderSdk;

        public OrderController(OrderSdk orderSdk)
        {
            _orderSdk = orderSdk;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResult>> Get(Guid id)
        {
            var result = await _orderSdk.Get(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderResult>>> Find([FromQuery] OrderFilter? filter)
        {
            var results = await _orderSdk.Find(filter);
            return Ok(results);
        }

        [HttpPost]
        public async Task<ActionResult<OrderResult>> Create(Guid customerId)
        {
            var result = await _orderSdk.Create(customerId);
            if (result == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPost("{id}/return")]
        public async Task<IActionResult> Return(Guid id, DateTime returnedAt)
        {
            var success = await _orderSdk.Return(id, returnedAt);
            if (!success)
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}
