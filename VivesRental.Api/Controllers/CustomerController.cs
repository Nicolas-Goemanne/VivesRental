using Microsoft.AspNetCore.Mvc;
using VivesRental.Sdk;
using VivesRental.Services.Model.Filters;
using VivesRental.Services.Model.Requests;
using VivesRental.Services.Model.Results;

namespace VivesRental.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerSdk _customerSdk;

        public CustomerController(CustomerSdk customerSdk)
        {
            _customerSdk = customerSdk;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResult>> Get(Guid id)
        {
            var result = await _customerSdk.Get(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerResult>>> Find([FromQuery] CustomerFilter? filter)
        {
            var results = await _customerSdk.Find(filter);
            return Ok(results);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerResult>> Create(CustomerRequest request)
        {
            var result = await _customerSdk.Create(request);
            if (result == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerResult>> Edit(Guid id, CustomerRequest request)
        {
            var result = await _customerSdk.Edit(id, request);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var success = await _customerSdk.Remove(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
