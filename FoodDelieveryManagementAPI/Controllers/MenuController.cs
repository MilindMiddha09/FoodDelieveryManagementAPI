using FoodDelieveryManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.Identity;
using FoodDelieveryManagementAPI.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System;

namespace FoodDelieveryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuBusiness _menuProductBusiness;
        

        public MenuController(IMenuBusiness menuProductBusiness)
        {
            _menuProductBusiness = menuProductBusiness;
        }
        
        [Route("/api/restaurant/updatemenu")]
        [HttpPost]
        [Authorize(Roles = "Restaurant")]
        public IActionResult UpdateMenu([FromBody] MenuProduct product)
        {   
            var userId=User.Identity.GetUserId();

            try
            {
                _menuProductBusiness.UpdateMenu(product, userId);
            }
            catch(ArgumentException)
            {
                return BadRequest("Log in first to update menu");
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
