using FoodDelieveryManagementAPI.Business.Interfaces;
using FoodDelieveryManagementAPI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
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
        [Authorize(Roles ="Admin")]
        public IActionResult GetAdmins()
        {
            try
            {
                var adminList = _adminBusiness.GetAdminList();

                if (adminList.Count == 0)
                    return NotFound("No admin exists..");

                return Ok(adminList);
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            try
            {
                await _adminBusiness.DeleteAdmin(id);
            }
            catch(ArgumentException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok("Admin Deleted Successfully...");
        }

        [Route("/api/admin/register")]
        [HttpPost]
        public async Task<IActionResult> RegisterAsAdmin([FromBody] RegisterDetails registerDetails)
        {
            try
            {
                await _adminBusiness.Register(registerDetails, "Admin");
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPatch]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(JsonPatchDocument<AppUser> update)
        {
            var userId = User.Identity.GetUserId();

            try
            {
                _adminBusiness.Update(update, userId);
            }
            catch
            {
                return BadRequest("Something Bad occured..");
            }
            return Ok("User updated successfully...");
        }
    }
}
