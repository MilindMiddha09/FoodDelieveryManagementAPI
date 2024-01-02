using FoodDelieveryManagementAPI.Business.Interfaces;
using FoodDelieveryManagementAPI.DataRepositories.Interfaces;
using FoodDelieveryManagementAPI.Models;
using System;
using System.Linq;

namespace FoodDelieveryManagementAPI.Business
{
    public class MenuBusiness : IMenuBusiness
    {
        private readonly IMenuDataRepo _dataRepo;
        private readonly IRestaurantDataRepo _restaurantDataRepo;
        public MenuBusiness(IMenuDataRepo dataRepo, IRestaurantDataRepo restaurantDataRepo) { 
            _dataRepo = dataRepo;
            _restaurantDataRepo = restaurantDataRepo;
        }
        public void UpdateMenu(MenuProduct product, string userId)
        {
            if(userId == null)
            {
                throw new ArgumentException();
            }
            var Id = _restaurantDataRepo.GetAllRestaurants().FirstOrDefault(u => u.IdentityUserId.Equals(userId)).ID;
            product.RestaurantId = Id;
            _dataRepo.AddProduct(product);
            return;         
        }
    }
}
