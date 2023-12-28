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
    public class AdminBusiness : IAdminBusiness
    {   
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAdminDataRepo _adminDataRepo;
        public AdminBusiness(UserManager<IdentityUser> userManager, IAdminDataRepo adminDataRepo)
        {
            _adminDataRepo = adminDataRepo;
            _userManager = userManager;
        }

        public List<AppUser> GetAdminList()
        {
            return _adminDataRepo.GetAllAdminList();
        }

        public async Task<bool> DeleteAdmin(int id)
        {
            var reqAdmin = _adminDataRepo.GetAllAdminList().FirstOrDefault(user => user.ID == id);
            if (reqAdmin != null)
            {
                var identityId = reqAdmin.IdentityUserId;

                _adminDataRepo.RemoveAdmin(reqAdmin);

                var identityAdmin = await _userManager.FindByIdAsync(identityId);
                if (identityAdmin != null)
                {
                    await _userManager.DeleteAsync(identityAdmin);
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

            var ifUserExists = await _userManager.FindByEmailAsync(identityUser.Email);

            if(ifUserExists != null)
            {
                return false;
            }

            var result1 = await _userManager.CreateAsync(identityUser, registerDetails.Password);
            var result2 = await _userManager.AddToRoleAsync(identityUser, role);

            if (result1.Succeeded && result2.Succeeded)
            {
                AppUser newUser = new AppUser {
                    UserRole = UserType.Admin,
                    IdentityUserId = identityUser.Id,
                };
                _adminDataRepo.AddAdmin(newUser);
                return true;
            }
            return false;
        }

        public void Update(JsonPatchDocument<AppUser> updates, string userId)
        {
           _adminDataRepo.UpdateAdmin(updates, userId);
        }
    }
}
