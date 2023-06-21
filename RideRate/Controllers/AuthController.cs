﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RideRate.DTOModels;
using RideRate.Helpers;
using RideRate.Services.Auth;

namespace RideRate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILoginServices _iloginServices;
        public AuthController(ILoginServices iloginServices)
        {
            _iloginServices= iloginServices;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest,Type = typeof(GenericResponse<string>))]
        public async Task<ActionResult> Login(LoginDTO request)
        {
            var result = await _iloginServices.Login(request);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost("refresh-token")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<string>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(GenericResponse<string>))]
        public async Task<ActionResult> RefreshToken()
        {
            var result = await _iloginServices.RefreshToken();
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
