using FoodDelieveryManagementAPI.Business.Interfaces;
using FoodDelieveryManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FoodDelieveryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthBusiness _authBusiness;
        public AuthController(IAuthBusiness authBusiness)
        {
            _authBusiness = authBusiness;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDetails loginDetails)
        {
            try
            {
                await _authBusiness.Login(loginDetails);
            }

            catch (ArgumentException)
            {
                return BadRequest("Input doesn't have any values.");
            }

            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok("Logged In Successfully.");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _authBusiness.Logout();
            }

            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok("Signed Out...");
        }
    }
}
