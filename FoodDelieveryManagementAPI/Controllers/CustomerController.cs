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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerBusiness _customerBusiness;

        public CustomerController(ICustomerBusiness customerBusiness)
        {
            _customerBusiness = customerBusiness;
        }

        [Route("/api/customers")]
        [HttpGet]
        public IActionResult GetCustomers()
        {
            var CustomerList = _customerBusiness.GetCustomerList();

            return Ok(CustomerList);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            if (_customerBusiness.DeleteCustomer(id))
            {
                return Ok("Customer Deleted Successfully");
            }

            return StatusCode(StatusCodes.Status404NotFound);
        }

        [Route("/api/customer/register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Register registerDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _customerBusiness.Register(registerDetails, "Customer");

            if (result == true)
            {
                return StatusCode(StatusCodes.Status201Created);
            }

            return BadRequest("One of the above methods fail..");
        }

        [HttpPatch]
        public IActionResult Update([FromBody] JsonPatchDocument<AppUser> updates)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var userId = User.Identity.GetUserId();
            _customerBusiness.Update(updates, userId);
            return Ok("User updated Successfully...");
        }

    }
}
