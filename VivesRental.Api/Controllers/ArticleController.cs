using Microsoft.AspNetCore.Mvc;
using VivesRental.Enums;
using VivesRental.Model;
using VivesRental.Repository.Core;
using VivesRental.Sdk;
using VivesRental.Services.Model.Filters;
using VivesRental.Services.Model.Requests;
using VivesRental.Services.Model.Results;

namespace VivesRental.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly ArticleSdk _articleSdk;

        public ArticleController(ArticleSdk articleSdk)
        {
            _articleSdk = articleSdk;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleResult>> Get(Guid id)
        {
            var result = await _articleSdk.Get(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<ArticleResult>>> Find([FromQuery] ArticleFilter? filter)
        {
            var results = await _articleSdk.Find(filter);
            return Ok(results);
        }

        [HttpPost]
        public async Task<ActionResult<ArticleResult>> Create(ArticleRequest request)
        {
            var result = await _articleSdk.Create(request);
            if (result == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, ArticleStatus status)
        {
            var success = await _articleSdk.UpdateStatus(id, status);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var success = await _articleSdk.Remove(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
