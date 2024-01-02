using FoodDelieveryManagementAPI.Business.Interfaces;
using FoodDelieveryManagementAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace FoodDelieveryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBusiness _orderBusiness;

        public OrderController(IOrderBusiness orderBusiness)
        {
            _orderBusiness = orderBusiness;
        }

        [Route("/api/order/updatehistory")]
        [HttpPost]
        [Authorize(Roles = "Customer")]
        public IActionResult UpdateHistory([FromBody] List<OrderProducts> order, int restaurantId, int customerId)
        {
            try
            {
                _orderBusiness.UpdateHistory(order, restaurantId, customerId);
            }
            catch (ArgumentException)
            {
                return BadRequest("Enter Ordered products.");
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
