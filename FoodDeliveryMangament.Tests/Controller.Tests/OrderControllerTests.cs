using FoodDelieveryManagementAPI.Business.Interfaces;
using FoodDelieveryManagementAPI.Controllers;
using FoodDelieveryManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FoodDeliveryMangament.Tests.Controller.Tests
{
    [TestClass]
    public class OrderControllerTests
    {

        private Mock<IOrderBusiness> _orderBusiness;
        private OrderController _orderController;

        [TestInitialize]
        public void Initialize()
        {
            _orderBusiness = new Mock<IOrderBusiness>();
            _orderController = new OrderController(_orderBusiness.Object);
        }

        [TestMethod]
        public void UpdateHistory_InputOrderAndUserIds_Returns201Created()
        {

            _orderBusiness.Setup(x => x.UpdateHistory(It.IsAny<List<OrderProducts>>(), It.IsAny<int>(), It.IsAny<int>()));

            var order = new List<OrderProducts>()
            {
                new OrderProducts()
                {
                    Name = "Product1",
                    Price = 200
                },
                new OrderProducts()
                {
                    Name = "Product2",
                    Price = 200
                }
            };
            int customerId = 1;
            int restaurantId = 2;
            var result = _orderController.UpdateHistory(order, restaurantId, customerId) as StatusCodeResult;

            Assert.AreEqual(StatusCodes.Status201Created, result.StatusCode);

        }

        [TestMethod]
        public void UpdateHistory_InputOrderAndUserIds_Returns400BadRequest()
        {

            _orderBusiness.Setup(x => x.UpdateHistory(It.IsAny<List<OrderProducts>>(), It.IsAny<int>(), It.IsAny<int>())).Throws(new ArgumentException());

            var order = new List<OrderProducts>()
            {
                new OrderProducts()
                {
                    Name = "Product1",
                    Price = 200
                },
                new OrderProducts()
                {
                    Name = "Product2",
                    Price = 200
                }
            };
            int customerId = 1;
            int restaurantId = 2;
            var result = _orderController.UpdateHistory(order, restaurantId, customerId) as BadRequestObjectResult;

            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);

        }

        [TestMethod]
        public void UpdateHistory_InputOrderAndUserIds_Returns500InternalServerError()
        {

            _orderBusiness.Setup(x => x.UpdateHistory(It.IsAny<List<OrderProducts>>(), It.IsAny<int>(), It.IsAny<int>())).Throws(new Exception());

            var order = new List<OrderProducts>()
            {
                new OrderProducts()
                {
                    Name = "Product1",
                    Price = 200
                },
                new OrderProducts()
                {
                    Name = "Product2",
                    Price = 200
                }
            };
            int customerId = 1;
            int restaurantId = 2;
            var result = _orderController.UpdateHistory(order, restaurantId, customerId) as StatusCodeResult;

            Assert.AreEqual(StatusCodes.Status500InternalServerError, result.StatusCode);

        }
    }
}
