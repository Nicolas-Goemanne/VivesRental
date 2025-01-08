using Microsoft.AspNetCore.Mvc;
using VivesRental.Services.Abstractions;
using VivesRental.Services.Model.Filters;
using VivesRental.Services.Model.Results;

namespace VivesRental.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResult>> Get(Guid id)
        {
            var result = await _orderService.Get(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderResult>>> Find([FromQuery] OrderFilter? filter)
        {
            var results = await _orderService.Find(filter);
            return Ok(results);
        }

        [HttpPost]
        public async Task<ActionResult<OrderResult>> Create(Guid customerId)
        {
            var result = await _orderService.Create(customerId);
            if (result == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut("{id}/return")]
        public async Task<IActionResult> Return(Guid id, DateTime returnedAt)
        {
            var success = await _orderService.Return(id, returnedAt);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}





















