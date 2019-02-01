using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace IndriverApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserServices _userServices;

        public AuthController(UserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody]User user)
        {
            var token = await _userServices.Authenticate(user.Login, user.Password);
            if (string.IsNullOrEmpty(token))
                return BadRequest();

            return Ok(new { token });
        }
    }
}