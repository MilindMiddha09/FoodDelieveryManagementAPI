using FoodDelieveryManagementAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace FoodDelieveryManagementAPI.DataRepositories.Interfaces
{
    public interface IRestaurantDataRepo
    {
        List<AppUser> GetAllRestaurants();
        void AddRestaurant(AppUser user);
        void DeleteRestaurant(AppUser user);
        void Update(JsonPatchDocument<AppUser> updates, string userId);
    }
}