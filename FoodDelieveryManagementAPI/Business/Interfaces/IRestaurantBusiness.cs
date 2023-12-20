using FoodDelieveryManagementAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodDelieveryManagementAPI.Business.Interfaces
{
    public interface IRestaurantBusiness
    {
        List<AppUser> GetRestaurants();
        bool DeleteRestaurant(int id);
        Task<bool> Register(Register registerDetails, string role);
        List<MenuProduct> GetRestaurantMenu(int id);

        AppUser GetRestaurantDetailsById(int id);

        List<MenuProduct> GetMenu(string userId);
        void Update(JsonPatchDocument<AppUser> updates, string userId);
    }
}