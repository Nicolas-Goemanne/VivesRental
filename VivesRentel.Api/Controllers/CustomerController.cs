﻿using Microsoft.AspNetCore.Mvc;
using VivesRental.Services.Abstractions;
using VivesRental.Services.Model.Filters;
using VivesRental.Services.Model.Requests;
using VivesRental.Services.Model.Results;

namespace VivesRental.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResult>> Get(Guid id)
        {
            var result = await _customerService.Get(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerResult>>> Find([FromQuery] CustomerFilter? filter)
        {
            var results = await _customerService.Find(filter);
            return Ok(results);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerResult>> Create(CustomerRequest request)
        {
            var result = await _customerService.Create(request);
            if (result == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerResult>> Edit(Guid id, CustomerRequest request)
        {
            var result = await _customerService.Edit(id, request);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
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









