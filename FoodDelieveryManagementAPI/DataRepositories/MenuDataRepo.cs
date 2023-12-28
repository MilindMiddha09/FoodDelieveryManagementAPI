using FoodDelieveryManagementAPI.Data;
using FoodDelieveryManagementAPI.DataRepositories.Interfaces;
using FoodDelieveryManagementAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace FoodDelieveryManagementAPI.DataRepositories
{
    public class MenuDataRepo : IMenuDataRepo
    {
        private readonly ApiDbContext _dbContext;

        public MenuDataRepo(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<MenuProduct> GetMenuProducts()
        {
            return _dbContext.MenuProducts.ToList();
        }

        public void AddProduct(MenuProduct product)
        {
            _dbContext.MenuProducts.Add(product);
            _dbContext.SaveChanges();
        }

        public List<MenuProduct> GetRestaurantMenu(int id)
        {
            return _dbContext.MenuProducts.ToList().FindAll(rest => rest.RestaurantId == id);
        }
    }
}
