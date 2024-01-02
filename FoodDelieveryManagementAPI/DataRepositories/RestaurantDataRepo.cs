using FoodDelieveryManagementAPI.Data;
using FoodDelieveryManagementAPI.DataRepositories.Interfaces;
using FoodDelieveryManagementAPI.Enum;
using FoodDelieveryManagementAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Linq;

namespace FoodDelieveryManagementAPI.DataRepositories
{
    public class RestaurantDataRepo : IRestaurantDataRepo
    {
        private readonly ApiDbContext _dbContext;

        public RestaurantDataRepo(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<AppUser> GetAllRestaurants()
        {
            return _dbContext.UserDetails.ToList().FindAll(user => user.UserRole == UserRole.Restaurant);
        }

        public void AddRestaurant(AppUser user)
        {
            _dbContext.UserDetails.Add(user);
            _dbContext.SaveChanges();
        }

        public void DeleteRestaurant(AppUser user)
        {
            _dbContext.UserDetails.Remove(user);
            _dbContext.SaveChanges();
        }

        public void Update(JsonPatchDocument<AppUser> updates, string userId)
        {
            var reqUser = _dbContext.UserDetails.FirstOrDefault(user => user.IdentityUserId == userId);

            updates.ApplyTo(reqUser);
            _dbContext.SaveChanges();
        }
    }
}
