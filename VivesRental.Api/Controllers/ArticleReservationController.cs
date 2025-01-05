using Microsoft.AspNetCore.Mvc;
using VivesRental.Sdk;
using VivesRental.Services.Model.Filters;
using VivesRental.Services.Model.Requests;
using VivesRental.Services.Model.Results;

namespace VivesRental.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleReservationController : ControllerBase
    {
        private readonly ArticleReservationSdk _articleReservationSdk;

        public ArticleReservationController(ArticleReservationSdk articleReservationSdk)
        {
            _articleReservationSdk = articleReservationSdk;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleReservationResult>> Get(Guid id)
        {
            var result = await _articleReservationSdk.Get(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<ArticleReservationResult>>> Find([FromQuery] ArticleReservationFilter? filter)
        {
            var results = await _articleReservationSdk.Find(filter);
            return Ok(results);
        }

        [HttpPost]
        public async Task<ActionResult<ArticleReservationResult>> Create(ArticleReservationRequest request)
        {
            var result = await _articleReservationSdk.Create(request);
            if (result == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var success = await _articleReservationSdk.Remove(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
