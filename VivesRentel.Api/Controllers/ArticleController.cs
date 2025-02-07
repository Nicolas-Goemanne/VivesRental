using Microsoft.AspNetCore.Mvc;
using VivesRental.Enums;
using VivesRental.Services.Abstractions;
using VivesRental.Services.Model.Filters;
using VivesRental.Services.Model.Requests;
using VivesRental.Services.Model.Results;

namespace VivesRental.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleResult>> Get(Guid id)
        {
            var result = await _articleService.Get(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<ArticleResult>>> Find([FromQuery] Guid? productId = null)
        {
            // Als er geen productId is, geef een lege filter door
            var filter = productId.HasValue ? new ArticleFilter { ProductId = productId.Value } : null;

            var results = await _articleService.Find(filter);

            if (results == null || results.Count == 0)
            {
                // Retourneer een lege lijst in plaats van een 404-fout
                return Ok(new List<ArticleResult>());
            }

            return Ok(results);
        }

        [HttpPost]
        public async Task<ActionResult<ArticleResult>> Create(ArticleRequest request)
        {
            var result = await _articleService.Create(request);
            if (result == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] ArticleStatus status)
        {
            var success = await _articleService.UpdateStatus(id, status);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var success = await _articleService.Remove(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}


















