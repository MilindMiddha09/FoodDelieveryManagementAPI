using FoodDelieveryManagementAPI.Business.Interfaces;
using FoodDelieveryManagementAPI.Data;
using FoodDelieveryManagementAPI.DataRepositories;
using FoodDelieveryManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodDelieveryManagementAPI.Business
{
    public class OrderBusiness : IOrderBusiness
    {
        private readonly IOrderDataRepo _dataRepo;
        public OrderBusiness(IOrderDataRepo dataRepo)
        {
            _dataRepo = dataRepo;
        }
        public bool UpdateHistory(List<OrderProducts> order, int restaurantId, int customerId)
        {
            int totalAmount = 0;

            foreach (var item in order)
            {
                totalAmount += item.Price;
            }
            if (order ==  null )
            {
                return false;
            }

            var newOrder = new Order()
            {
                OrderTime = DateTime.Now,
                CustomerId = customerId,
                RestaurantId = restaurantId,
                TotalAmount = totalAmount
            };

            _dataRepo.SaveOrder(newOrder);

            var id = _dataRepo.FindCurrentOrder(newOrder);

            foreach(var orderProduct in order) {
                orderProduct.OrderId = id;
                _dataRepo.SaveOrderedProduct(orderProduct);
            }
            return true;
        }
    }
}
