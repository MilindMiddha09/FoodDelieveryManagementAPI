using FoodDelieveryManagementAPI.Business.Interfaces;
using FoodDelieveryManagementAPI.Data;
using FoodDelieveryManagementAPI.Models;
using System.Linq;

namespace FoodDelieveryManagementAPI.Business
{
    public class MenuBusiness : IMenuBusiness
    {
        private readonly ApiDbContext _dbContext;
        public MenuBusiness(ApiDbContext dbContext) { 
            _dbContext = dbContext;
        }
        public bool UpdateMenu(MenuProduct product, string userId)
        {
            var Id = _dbContext.UserDetails.FirstOrDefault(u => u.IdentityUserId.Equals(userId)).ID;
            product.RestaurantId = Id;
            _dbContext.MenuProducts.Add(product);
            _dbContext.SaveChanges();
            return true;         
        }
    }
}
