using FoodDelieveryManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.Identity;
using FoodDelieveryManagementAPI.Business.Interfaces;

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
        
        public IActionResult UpdateMenu([FromBody] MenuProduct product)
        {
            if(!ModelState.IsValid) { 
                return BadRequest(ModelState);
            }
            
            var userId=User.Identity.GetUserId();

            _menuProductBusiness.UpdateMenu(product, userId);

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
