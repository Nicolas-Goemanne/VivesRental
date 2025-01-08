using Microsoft.AspNetCore.Mvc;
using VivesRental.Services.Abstractions;
using VivesRental.Services.Model.Filters;
using VivesRental.Services.Model.Requests;
using VivesRental.Services.Model.Results;

namespace VivesRental.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResult>> Get(Guid id)
        {
            var result = await _productService.Get(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductResult>>> Find([FromQuery] ProductFilter? filter)
        {
            var results = await _productService.Find(filter);
            return Ok(results);
        }

        [HttpPost]
        public async Task<ActionResult<ProductResult>> Create(ProductRequest request)
        {
            var result = await _productService.Create(request);
            if (result == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductResult>> Edit(Guid id, ProductRequest request)
        {
            var result = await _productService.Edit(id, request);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var success = await _productService.Remove(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}











