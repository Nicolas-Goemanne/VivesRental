using Microsoft.AspNetCore.Mvc;
using VivesRental.Sdk;
using VivesRental.Services.Model.Filters;
using VivesRental.Services.Model.Requests;
using VivesRental.Services.Model.Results;

namespace VivesRental.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductSdk _productSdk;

        public ProductController(ProductSdk productSdk)
        {
            _productSdk = productSdk;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResult>> Get(Guid id)
        {
            var result = await _productSdk.Get(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductResult>>> Find([FromQuery] ProductFilter? filter)
        {
            var results = await _productSdk.Find(filter);
            return Ok(results);
        }

        [HttpPost]
        public async Task<ActionResult<ProductResult>> Create(ProductRequest request)
        {
            var result = await _productSdk.Create(request);
            if (result == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductResult>> Edit(Guid id, ProductRequest request)
        {
            var result = await _productSdk.Edit(id, request);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var success = await _productSdk.Remove(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost("{id}/generate-articles")]
        public async Task<IActionResult> GenerateArticles(Guid id, int amount)
        {
            var success = await _productSdk.GenerateArticles(id, amount);
            if (!success)
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}

