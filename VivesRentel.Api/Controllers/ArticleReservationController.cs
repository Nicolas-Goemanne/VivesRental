using Microsoft.AspNetCore.Mvc;
using VivesRental.Services.Model.Filters;
using VivesRental.Services.Model.Requests;
using VivesRental.Services.Model.Results;
using VivesRental.Services.Abstractions;

namespace VivesRental.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleReservationController : ControllerBase
    {
        private readonly IArticleReservationService _articleReservationService;

        public ArticleReservationController(IArticleReservationService articleReservationService)
        {
            _articleReservationService = articleReservationService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleReservationResult>> Get(Guid id)
        {
            var result = await _articleReservationService.Get(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<ArticleReservationResult>>> Find([FromQuery] ArticleReservationFilter? filter)
        {
            var results = await _articleReservationService.Find(filter);
            return Ok(results);
        }

        [HttpPost]
        public async Task<ActionResult<ArticleReservationResult>> Create([FromBody] ArticleReservationRequest request)
        {
            var result = await _articleReservationService.Create(request);
            if (result == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var success = await _articleReservationService.Remove(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}


