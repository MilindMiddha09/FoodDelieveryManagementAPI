using FoodDelieveryManagementAPI.Models;

namespace FoodDelieveryManagementAPI.DataRepositories
{
    public interface IOrderDataRepo
    {
        int FindCurrentOrder(Order order);
        void SaveOrder(Order newOrder);
        void SaveOrderedProduct(OrderProducts newOrderProduct);
    }
}