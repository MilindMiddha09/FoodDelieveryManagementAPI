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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerBusiness _customerBusiness;

        public CustomerController(ICustomerBusiness customerBusiness)
        {
            _customerBusiness = customerBusiness;
        }

        [Route("/api/customers")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetCustomers()
        {
            try
            {
                var customerList = _customerBusiness.GetCustomerList();

                if (customerList.Count == 0)
                    return StatusCode(StatusCodes.Status404NotFound);

                return Ok(customerList);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                await _customerBusiness.DeleteCustomer(id);
            }
            catch(ArgumentException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok("Customer Deleted Successfully"); 
        }

        [Route("/api/customer/register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDetails registerDetails)
        {
            try
            {
                await _customerBusiness.Register(registerDetails, "Customer");
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {   
                //Logging krni h.
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return StatusCode(StatusCodes.Status201Created);
            
        }

        [HttpPatch]
        [Authorize(Roles = "Customer")]
        public IActionResult Update([FromBody] JsonPatchDocument<AppUser> updates)
        {
            var userId = User.Identity.GetUserId();
            try
            {
                _customerBusiness.Update(updates, userId);
            }
            catch
            {
                return BadRequest("Something Bad Occured.");
            }
            return Ok("User updated Successfully...");
        }

    }
}
