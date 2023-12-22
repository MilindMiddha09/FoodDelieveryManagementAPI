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
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantBusiness _restaurantBusiness;
        
        public RestaurantController(IRestaurantBusiness restaurantBusiness)
        {
            _restaurantBusiness = restaurantBusiness;
        }

        [Route("/api/restaurants")]
        [HttpGet]
        public IActionResult GetRestaurants()
        {
            var restaurantsList = _restaurantBusiness.GetRestaurants();
            if (restaurantsList == null)
            {
                return NotFound();
            }
            return Ok(restaurantsList);
        }

        [HttpDelete]
        public IActionResult DeleteRestaurant(int id)
        {
            if (_restaurantBusiness.DeleteRestaurant(id))
                return Ok("Restaurant Deleted Successfully...");

            return StatusCode(StatusCodes.Status404NotFound);
        }

        [Route("/api/restaurant/register")]
        [HttpPost]
        public async Task<IActionResult> RegisterAsRestaurant([FromBody] Register registerDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _restaurantBusiness.Register(registerDetails, "Restaurant");

            if (result == true)
            {
                return StatusCode(StatusCodes.Status201Created);
            }

            return BadRequest();
        }

        [Route("/api/menu/{id}")]
        [HttpGet]
        public IActionResult GetRestaurantMenu(int id)
        {
            var menu = _restaurantBusiness.GetRestaurantMenu(id);

            if (menu.Count == 0)
                return BadRequest();

            return Ok(menu);
        }

        
        [HttpGet("{id}")]
        public IActionResult GetRestaurantDetailsById(int id)
        {
            var restaurant = _restaurantBusiness.GetRestaurantDetailsById(id);

            if(restaurant == null)
                return BadRequest();

            return Ok(restaurant);
        }

        [Route("/api/restaurant/menu")]
        [HttpGet]
        public IActionResult GetMenu()
        {
            var userId = User.Identity.GetUserId();

            var menu = _restaurantBusiness.GetMenu(userId);
            
            if(menu.Count==0)
                return BadRequest("No menu exists...");

            return Ok(menu);
        }

        [HttpPatch]
        public IActionResult Update([FromBody] JsonPatchDocument<AppUser> updates)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userId = User.Identity.GetUserId();

            _restaurantBusiness.Update(updates, userId);

            return Ok("User updated successfully...");
        }
    }
}
