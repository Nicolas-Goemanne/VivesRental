using Microsoft.AspNetCore.Mvc;
using VivesRental.Services.Abstractions;
using VivesRental.Services.Model.Filters;
using VivesRental.Services.Model.Results;

[ApiController]
[Route("api/[controller]")]
public class OrderLineController : ControllerBase
{
    private readonly IOrderLineService _orderLineService;

    public OrderLineController(IOrderLineService orderLineService)
    {
        _orderLineService = orderLineService;
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderLineResult>>> Find([FromQuery] OrderLineFilter? filter)
    {
        var results = await _orderLineService.Find(filter);
        return Ok(results);
    }

    [HttpPost("Rent")]
    public async Task<IActionResult> Rent([FromQuery] Guid orderId, [FromQuery] Guid articleId)
    {
        var success = await _orderLineService.Rent(orderId, articleId);
        if (!success)
        {
            return BadRequest(new { Message = "Failed to rent article. Verify the order ID and article ID." });
        }
        return NoContent();
    }


    [HttpPost("{orderLineId}/Return")]
    public async Task<IActionResult> Return(Guid orderLineId, [FromBody] DateTime returnedAt)
    {
        var success = await _orderLineService.Return(orderLineId, returnedAt);
        if (!success)
        {
            return BadRequest(new { Message = "Failed to return order line. Verify order line ID and return date." });
        }
        return NoContent();
    }
}




