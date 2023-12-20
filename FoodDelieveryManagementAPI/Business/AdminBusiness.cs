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
    public class AdminBusiness : IAdminBusiness
    {   
        private ApiDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        public AdminBusiness(ApiDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public List<AppUser> GetAdminList()
        {
            return _dbContext.UserDetails.ToList().FindAll(admin => admin.UserRole == UserType.Admin);
        }

        public bool DeleteAdmin(int id)
        {
            var reqAdmin = _dbContext.UserDetails.FirstOrDefault(admin => admin.ID == id);
            if (reqAdmin != null)
            {
                _dbContext.UserDetails.Remove(reqAdmin);
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
                AppUser newUser = new AppUser { IdentityUserId = identityUser.Id ,
                    UserRole = UserType.Admin,
                };
                _dbContext.UserDetails.Add(newUser);
                _dbContext.SaveChanges();

                return true;
            }
            return false;
        }

        public void Update(JsonPatchDocument<AppUser> updates, string userId)
        {
            var reqUser = _dbContext.UserDetails.FirstOrDefault(user => user.IdentityUserId == userId);

            updates.ApplyTo(reqUser);
            _dbContext.SaveChanges();
        }
    }
}
