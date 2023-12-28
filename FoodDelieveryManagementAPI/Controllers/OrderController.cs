using FoodDelieveryManagementAPI.Business.Interfaces;
using FoodDelieveryManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult UpdateHistory([FromBody] List<OrderProducts> order, int restaurantId, int customerId)
        {
            if (_orderBusiness.UpdateHistory(order, restaurantId, customerId))
                return StatusCode(StatusCodes.Status201Created);

            return BadRequest();
        }
    }
}
