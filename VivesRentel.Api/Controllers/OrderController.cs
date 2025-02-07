using Microsoft.AspNetCore.Mvc;
using VivesRental.Services.Abstractions;
using VivesRental.Services.Model.Filters;
using VivesRental.Services.Model.Results;

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
    public async Task<ActionResult<OrderResult>> Create([FromQuery] Guid customerId)
    {
        var result = await _orderService.Create(customerId);
        if (result == null)
        {
            return BadRequest(new { Message = "Failed to create order. Ensure the customer ID is valid." });
        }
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    [HttpPost("{id}/Return")]
    public async Task<IActionResult> Return(Guid id, [FromBody] DateTime returnedAt)
    {
        var success = await _orderService.Return(id, returnedAt);
        if (!success)
        {
            return BadRequest(new { Message = "Failed to return order. Verify order ID and return date." });
        }
        return NoContent();
    }
}
























