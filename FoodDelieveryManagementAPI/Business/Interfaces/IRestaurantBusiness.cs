using FoodDelieveryManagementAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodDelieveryManagementAPI.Business.Interfaces
{
    public interface IRestaurantBusiness
    {
        List<AppUser> GetRestaurants();
        Task DeleteRestaurant(int id);
        Task Register(RegisterDetails registerDetails, string role);
        List<MenuProduct> GetRestaurantMenu(int id);

        AppUser GetRestaurantDetailsById(int id);
        void Update(JsonPatchDocument<AppUser> updates, string userId);
    }
}