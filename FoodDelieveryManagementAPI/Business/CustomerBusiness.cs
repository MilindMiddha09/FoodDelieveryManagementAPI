using FoodDelieveryManagementAPI.Business.Interfaces;
using FoodDelieveryManagementAPI.Data;
using FoodDelieveryManagementAPI.Enum;
using FoodDelieveryManagementAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelieveryManagementAPI.Business
{
    public class CustomerBusiness : ICustomerBusiness
    {
        private ApiDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public CustomerBusiness(ApiDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        public List<AppUser> GetCustomerList()
        {
            var customerList = _dbContext.UserDetails.ToList().FindAll(customer => customer.UserRole == UserType.Customer);
            return customerList;
        }

        public bool DeleteCustomer(int id)
        {
            var reqCustomer = _dbContext.UserDetails.FirstOrDefault(customer => customer.ID == id);

            if (reqCustomer != null)
            {
                _dbContext.UserDetails.Remove(reqCustomer);
                _dbContext.SaveChanges();
                return true;
            }

            return false;
        }

        public async Task<bool> Register(Register registerDetails, string role)
        {
            var identityUser = new IdentityUser
            {   
                UserName = registerDetails.Email,
                Email = registerDetails.Email
            };

            var result1 = await _userManager.CreateAsync(identityUser, registerDetails.Password);
            var result2 = await _userManager.AddToRoleAsync(identityUser, role);

            if (result1.Succeeded && result2.Succeeded)
            {
                AppUser newUser = new AppUser {
                    IdentityUserId = identityUser.Id , 
                    UserRole = UserType.Customer, 
                    Name = registerDetails.Name,
                    Address = registerDetails.Address,
                    ContactNo = registerDetails.ContactNo,
                };
                _dbContext.UserDetails.Add(newUser);
                _dbContext.SaveChanges();

                return true;   
            }
            return false;
        }

        public void Update(JsonPatchDocument<AppUser> updates, string identityUserId)
        {
            var reqUser = _dbContext.UserDetails.FirstOrDefault(user => user.IdentityUserId.Equals(identityUserId));
            updates.ApplyTo(reqUser);
            _dbContext.SaveChanges();
        }
    }
}
