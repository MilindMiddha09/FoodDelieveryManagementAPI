using FoodDelieveryManagementAPI.Business.Interfaces;
using FoodDelieveryManagementAPI.DataRepositories.Interfaces;
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
        private readonly ICustomerDataRepo _dataRepo;
        private readonly UserManager<IdentityUser> _userManager;

        public CustomerBusiness(ICustomerDataRepo dataRepo, UserManager<IdentityUser> userManager)
        {
            _dataRepo = dataRepo;
            _userManager = userManager;
        }
        public List<AppUser> GetCustomerList()
        {
            return _dataRepo.GetAllCustomers();
        }

        public async Task<bool> DeleteCustomer(int id)
        {
            var reqCustomer = _dataRepo.GetAllCustomers().FirstOrDefault(customer => customer.ID == id);

            if (reqCustomer != null)
            {
                var identityId = reqCustomer.IdentityUserId;
                _dataRepo.DeleteCustomer(reqCustomer);

                var identityCustomer = await _userManager.FindByIdAsync(identityId);
                if (identityCustomer != null)
                {
                    await _userManager.DeleteAsync(identityCustomer);
                }

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

            var ifUserExists = _userManager.FindByEmailAsync(identityUser.Email);

            if (ifUserExists != null)
            {
                return false;
            }

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
                
                _dataRepo.AddCustomer(newUser);

                return true;   
            }
            return false;
        }

        public void Update(JsonPatchDocument<AppUser> updates, string identityUserId)
        {
            _dataRepo.UpdateCustomer(updates, identityUserId);
        }
    }
}
