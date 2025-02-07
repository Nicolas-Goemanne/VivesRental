using Microsoft.AspNetCore.Mvc;
using VivesRental.Services.Abstractions;
using VivesRental.Services.Model.Filters;
using VivesRental.Services.Model.Requests;
using VivesRental.Services.Model.Results;

namespace VivesRental.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductResult?>> Get([FromRoute] Guid id)
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
            var result = await _productService.Find(filter);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ProductResult>> Create([FromBody] ProductRequest request)
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
                var result = await _productService.Create(request);

                if (result == null)
                {
                    return BadRequest(new { Message = "Failed to create product. Please check the input data." });
                }

                return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Create: {ex.Message}");
                return StatusCode(500, new { Message = "An error occurred while creating the product." });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductResult>> Edit(Guid id, [FromBody] ProductRequest request)
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
                var result = await _productService.Edit(id, request);

                if (result == null)
                {
                    return NotFound(new { Message = "Product not found or update failed." });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Edit: {ex.Message}");
                return StatusCode(500, new { Message = "An error occurred while updating the product." });
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var success = await _productService.Remove(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost("{id:guid}/generate-articles")]
        public async Task<IActionResult> GenerateArticles([FromRoute] Guid id, [FromQuery] int amount)
        {
            var success = await _productService.GenerateArticles(id, amount);
            if (!success)
            {
                return BadRequest(new { Message = "Failed to generate articles." });
            }
            return Ok();
        }
    }
}


















