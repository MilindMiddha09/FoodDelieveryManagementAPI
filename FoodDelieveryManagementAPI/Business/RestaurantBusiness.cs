using FoodDelieveryManagementAPI.Business.Interfaces;
using FoodDelieveryManagementAPI.DataRepositories.Interfaces;
using FoodDelieveryManagementAPI.Enum;
using FoodDelieveryManagementAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelieveryManagementAPI.Business
{
    public class RestaurantBusiness : IRestaurantBusiness
    {
        
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IRestaurantDataRepo _dataRepo;
        private readonly IMenuDataRepo _menuDataRepo;
        public RestaurantBusiness(UserManager<IdentityUser> userManager, IRestaurantDataRepo dataRepo, IMenuDataRepo menuDataRepo)
        {
            _dataRepo = dataRepo;
            _userManager = userManager;
            _menuDataRepo = menuDataRepo;
        }

        public List<AppUser> GetRestaurants()
        {
            return _dataRepo.GetAllRestaurants();
        }

        public async Task DeleteRestaurant(int id)
        {
            var reqRestaurant = _dataRepo.GetAllRestaurants().FirstOrDefault(restaurant => restaurant.ID == id);

            if (reqRestaurant != null)
            {
                var identityId = reqRestaurant.IdentityUserId;
                var identityRestaurant = await _userManager.FindByIdAsync(identityId);

                _dataRepo.DeleteRestaurant(reqRestaurant);
                await _userManager.DeleteAsync(identityRestaurant);
                return;
            }

            throw new ArgumentException();
        }

        public async Task Register(RegisterDetails registerDetails, string role)
        {
            var identityUser = new IdentityUser
            {   
                UserName = registerDetails.Email,
                Email = registerDetails.Email
            };
            var ifUserExists = await _userManager.FindByEmailAsync(identityUser.Email);

            if (ifUserExists != null)
            {
                throw new ArgumentException("User Already Exists..");
            }

            var result1 = await _userManager.CreateAsync(identityUser, registerDetails.Password);
            var result2 = await _userManager.AddToRoleAsync(identityUser, role);

            if (result1.Succeeded && result2.Succeeded)
            {
                AppUser newUser = new AppUser { 
                    IdentityUserId = identityUser.Id,
                    Name = registerDetails.Name,
                    UserRole = UserRole.Restaurant,
                    Address = registerDetails.Address,
                    ContactNo = registerDetails.ContactNo,
                };
                _dataRepo.AddRestaurant(newUser);

                return;
            }
            throw new InvalidOperationException("Something Bad has Occured.");
        }

        public List<MenuProduct> GetRestaurantMenu(int id)
        {
            return _menuDataRepo.GetRestaurantMenu(id);
        }

        public AppUser GetRestaurantDetailsById(int id)
        {
            return _dataRepo.GetAllRestaurants().FirstOrDefault(rest => rest.ID == id);
        }

        public void Update(JsonPatchDocument<AppUser> updates, string userId)
        {
            _dataRepo.Update(updates, userId);
        }

    }
}
