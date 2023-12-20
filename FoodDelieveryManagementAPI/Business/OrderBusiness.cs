using FoodDelieveryManagementAPI.Business.Interfaces;
using FoodDelieveryManagementAPI.Data;
using FoodDelieveryManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodDelieveryManagementAPI.Business
{
    public class OrderBusiness : IOrderBusiness
    {
        private readonly ApiDbContext _dbContext;
        public OrderBusiness(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
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

            _dbContext.Orders.Add(newOrder);
            _dbContext.SaveChanges();

            var id = _dbContext.Orders.Max(order => order.Id);

            foreach(var orderProduct in order) {
                orderProduct.OrderId = id;
                _dbContext.OrderProducts.Add(orderProduct);
            }

            _dbContext.SaveChanges();

            return true;
        }
    }
}
