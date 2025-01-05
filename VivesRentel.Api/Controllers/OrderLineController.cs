using Microsoft.AspNetCore.Mvc;
using VivesRental.Sdk;
using VivesRental.Services.Model.Filters;
using VivesRental.Services.Model.Results;

namespace VivesRental.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderLineController : ControllerBase
    {
        private readonly OrderLineSdk _orderLineSdk;

        public OrderLineController(OrderLineSdk orderLineSdk)
        {
            _orderLineSdk = orderLineSdk;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderLineResult>> Get(Guid id)
        {
            var result = await _orderLineSdk.Get(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderLineResult>>> Find([FromQuery] OrderLineFilter? filter)
        {
            var results = await _orderLineSdk.Find(filter);
            return Ok(results);
        }

        [HttpPost("rent")]
        public async Task<IActionResult> Rent(Guid orderId, Guid articleId)
        {
            var success = await _orderLineSdk.Rent(orderId, articleId);
            if (!success)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpPost("rent/multiple")]
        public async Task<IActionResult> Rent(Guid orderId, IList<Guid> articleIds)
        {
            var success = await _orderLineSdk.Rent(orderId, articleIds);
            if (!success)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpPost("return")]
        public async Task<IActionResult> Return(Guid orderLineId, DateTime returnedAt)
        {
            var success = await _orderLineSdk.Return(orderLineId, returnedAt);
            if (!success)
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}
