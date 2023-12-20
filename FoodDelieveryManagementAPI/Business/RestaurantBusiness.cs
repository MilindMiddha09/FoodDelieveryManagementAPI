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
    public class RestaurantBusiness : IRestaurantBusiness
    {
        private ApiDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        public RestaurantBusiness(ApiDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public List<AppUser> GetRestaurants()
        {
            return _dbContext.UserDetails.ToList().FindAll(rest => rest.UserRole == UserType.Restaurant);
        }

        public bool DeleteRestaurant(int id)
        {
            var reqRestaurant = _dbContext.UserDetails.FirstOrDefault(restaurant => restaurant.ID == id);

            if (reqRestaurant != null)
            {
                _dbContext.UserDetails.Remove(reqRestaurant);
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
                    IdentityUserId = identityUser.Id,
                    Name = registerDetails.Name,
                    UserRole = UserType.Restaurant,
                    Address = registerDetails.Address,
                    ContactNo = registerDetails.ContactNo,
                };
                _dbContext.UserDetails.Add(newUser);
                _dbContext.SaveChanges();

                return true;
            }
            return false;
        }

        public List<MenuProduct> GetRestaurantMenu(int id)
        {
            return _dbContext.MenuProducts.ToList().FindAll(rest=>rest.Id == id);
        }

        public AppUser GetRestaurantDetailsById(int id)
        {
            return _dbContext.UserDetails.FirstOrDefault(rest => rest.ID == id);
        }

        public List<MenuProduct> GetMenu(string userId)
        {
            var restaurantId = _dbContext.UserDetails.FirstOrDefault(user => user.IdentityUser.Equals(userId)).ID;

            return _dbContext.MenuProducts.ToList().FindAll(rest => rest.RestaurantId == restaurantId);
        }

        public void Update(JsonPatchDocument<AppUser> updates, string userId)
        {
            var reqUser = _dbContext.UserDetails.FirstOrDefault(user => user.IdentityUserId == userId);

            updates.ApplyTo(reqUser);
            _dbContext.SaveChanges();
        }

    }
}
