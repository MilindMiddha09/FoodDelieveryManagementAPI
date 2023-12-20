using FoodDelieveryManagementAPI.Models;
using System.Collections.Generic;

namespace FoodDelieveryManagementAPI.Business.Interfaces
{
    public interface IOrderBusiness
    {
        bool UpdateHistory(List<OrderProducts> order, int restaurantId, int customerId);
    }
}