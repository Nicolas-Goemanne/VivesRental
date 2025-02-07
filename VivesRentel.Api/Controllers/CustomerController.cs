using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VivesRental.Sdk;
using VivesRental.Services.Abstractions;
using VivesRental.Services.Model.Filters;
using VivesRental.Services.Model.Requests;
using VivesRental.Services.Model.Results;

namespace VivesRental.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CustomerResult?>> Get([FromRoute] Guid id)
        {
            var result = await _customerService.Get(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<CustomerResult>>> Find([FromQuery] CustomerFilter? filter)
        {
            var result = await _customerService.Find(filter);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerResult>> Create([FromBody] CustomerRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Message = "Invalid input",
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }

            try
            {
                var result = await _customerService.Create(request);

                if (result == null)
                {
                    return BadRequest(new { Message = "Failed to create customer. Please check the input data." });
                }

                return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Create: {ex.Message}");
                return StatusCode(500, new { Message = "An error occurred while creating the customer." });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerResult>> Edit(Guid id, [FromBody] CustomerRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Message = "Invalid input",
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }

            try
            {
                var result = await _customerService.Edit(id, request);

                if (result == null)
                {
                    return NotFound(new { Message = "Customer not found or update failed." });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Edit: {ex.Message}");
                return StatusCode(500, new { Message = "An error occurred while updating the customer." });
            }
        }


        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var success = await _customerService.Remove(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}














