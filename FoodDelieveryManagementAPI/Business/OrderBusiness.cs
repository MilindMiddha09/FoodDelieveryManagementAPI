using FoodDelieveryManagementAPI.Business.Interfaces;
using FoodDelieveryManagementAPI.DataRepositories;
using FoodDelieveryManagementAPI.Models;
using System;
using System.Collections.Generic;

namespace FoodDelieveryManagementAPI.Business
{
    public class OrderBusiness : IOrderBusiness
    {
        private readonly IOrderDataRepo _dataRepo;
        public OrderBusiness(IOrderDataRepo dataRepo)
        {
            _dataRepo = dataRepo;
        }
        public void UpdateHistory(List<OrderProducts> order, int restaurantId, int customerId)
        { 
            if (order ==  null)
            {
                throw new ArgumentNullException();
            }
            int totalAmount = 0;

            foreach (var item in order)
            {
                totalAmount += item.Price;
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
            return;
        }
    }
}
