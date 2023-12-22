using FoodDelieveryManagementAPI.Business.Interfaces;
using FoodDelieveryManagementAPI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FoodDelieveryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminBusiness _adminBusiness;

        public AdminController(IAdminBusiness adminBusiness)
        {
            _adminBusiness = adminBusiness;
        }

        [Route("/api/admins")]
        [HttpGet]
        public IActionResult GetAdmins()
        {
            var adminList = _adminBusiness.GetAdminList();

            if (adminList.Count == 0)
                return NotFound("No admin exists..");

            return Ok(adminList);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAdmin(int id)
        {
            if (_adminBusiness.DeleteAdmin(id))
                return Ok("Admin Deleted Successfully...");

            return StatusCode(StatusCodes.Status404NotFound);
        }

        [Route("/api/admin/register")]
        [HttpPost]
        public async Task<IActionResult> RegisterAsRestaurant([FromBody] Register registerDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _adminBusiness.Register(registerDetails, "Admin");

            if (result == true)
            {
                return StatusCode(StatusCodes.Status201Created);
            }

            return BadRequest();
        }

        [HttpPatch]
        public IActionResult Update(JsonPatchDocument<AppUser> update)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var userId = User.Identity.GetUserId();

            _adminBusiness.Update(update, userId);

            return Ok("User updated successfully...");
        }
    }
}
