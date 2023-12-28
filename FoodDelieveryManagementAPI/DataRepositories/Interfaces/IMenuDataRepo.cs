using FoodDelieveryManagementAPI.Models;
using System.Collections.Generic;

namespace FoodDelieveryManagementAPI.DataRepositories.Interfaces
{
    public interface IMenuDataRepo
    {
        List<MenuProduct> GetMenuProducts();
        void AddProduct(MenuProduct product);
        List<MenuProduct> GetRestaurantMenu(int id);
    }
}