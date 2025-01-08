﻿using Microsoft.AspNetCore.Mvc;
using VivesRental.Services;
using VivesRental.Services.Model.Requests;

namespace VivesRental.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IdentityService _identityService;

        public IdentityController(IdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn(UserSignInRequest request)
        {
            var result = await _identityService.SignIn(request);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            var result = await _identityService.Register(request);
            return Ok(result);
        }
    }
}















