using FoodDelieveryManagementAPI.Data;
using FoodDelieveryManagementAPI.Models;

namespace FoodDelieveryManagementAPI.DataRepositories
{
    public class OrderDataRepo : IOrderDataRepo
    {
        private readonly ApiDbContext _dbContext;

        public OrderDataRepo(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SaveOrder(Order newOrder)
        {
            _dbContext.Orders.Add(newOrder);
            _dbContext.SaveChanges();
        }

        public void SaveOrderedProduct(OrderProducts newOrderProduct)
        {
            _dbContext.OrderProducts.Add(newOrderProduct);
        }

        public int FindCurrentOrder(Order order)
        {
            return _dbContext.Orders.Find(order).Id;
        }
    }
}
