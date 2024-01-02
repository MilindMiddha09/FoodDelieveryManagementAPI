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
    public class AdministrationController : ControllerBase
    {
        private readonly IAdministrationBusiness _administrationBusiness;
        public AdministrationController(IAdministrationBusiness administrationBusiness)
        {
            _administrationBusiness = administrationBusiness;
        }

        [HttpPost("createrole")]
        public async Task<IActionResult> CreateRole([FromBody] Usertype role)
        {
            try
            {
                await _administrationBusiness.CreateRole(role);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok("User Role Added Successfully.");
        }
    }
}
