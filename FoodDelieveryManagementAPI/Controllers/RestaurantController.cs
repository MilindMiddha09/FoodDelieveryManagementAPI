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
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantBusiness _restaurantBusiness;
        
        public RestaurantController(IRestaurantBusiness restaurantBusiness)
        {
            _restaurantBusiness = restaurantBusiness;
        }

        [Route("/api/restaurants")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetRestaurants()
        {
            try
            {
                var restaurantsList = _restaurantBusiness.GetRestaurants();
                if (restaurantsList.Count == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                return Ok(restaurantsList);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            try
            {
                await _restaurantBusiness.DeleteRestaurant(id);
            }
            catch (ArgumentException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok("Restaurant Deleted Successfully...");
        }

        [Route("/api/restaurant/register")]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterAsRestaurant([FromBody] RegisterDetails registerDetails)
        {
            try
            {
                await _restaurantBusiness.Register(registerDetails, "Restaurant");
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

        [Route("/api/menu/{id}")]
        [HttpGet]
        [Authorize(Roles = "Customer")]
        public IActionResult GetRestaurantMenu(int id)
        {
            try
            {
                var menu = _restaurantBusiness.GetRestaurantMenu(id);

                if (menu.Count == 0)
                    return StatusCode(StatusCodes.Status404NotFound);

                return Ok(menu);
            }
            catch(Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        
        [HttpGet("{id}")]
        [Authorize(Roles = "Restaurant")]
        public IActionResult GetRestaurantDetailsById(int id)
        {
            try
            {
                var restaurant = _restaurantBusiness.GetRestaurantDetailsById(id);

                if (restaurant == null)
                    return StatusCode(StatusCodes.Status404NotFound);

                return Ok(restaurant);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPatch]
        [Authorize(Roles = "Restaurant")]
        public IActionResult Update([FromBody] JsonPatchDocument<AppUser> updates)
        {
            var userId = User.Identity.GetUserId();
            try
            {
                _restaurantBusiness.Update(updates, userId);
            }
            catch (Exception)
            {
                return BadRequest("Something bad Occured.");
            }

            return Ok("User updated successfully...");
        }
    }
}
