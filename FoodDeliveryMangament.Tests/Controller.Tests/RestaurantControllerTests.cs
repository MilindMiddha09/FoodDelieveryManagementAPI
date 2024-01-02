using FoodDelieveryManagementAPI.Business;
using FoodDelieveryManagementAPI.Business.Interfaces;
using FoodDelieveryManagementAPI.Controllers;
using FoodDelieveryManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliveryMangament.Tests.Controller.Tests
{
    [TestClass]
    public class RestaurantControllerTests
    {
        private Mock<IRestaurantBusiness> _restaurantBusiness;
        private RestaurantController _restaurantController;

        [TestInitialize]
        public void Initialize()
        {
            var httpContext = new DefaultHttpContext()
            {
                User = new System.Security.Claims.ClaimsPrincipal(new GenericIdentity("username"))
            };

            var actionContext = new ActionContext(httpContext, new Microsoft.AspNetCore.Routing.RouteData(),
                                                   new Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor());
            _restaurantBusiness = new Mock<IRestaurantBusiness>();
            _restaurantController = new RestaurantController(_restaurantBusiness.Object)
            {
                ControllerContext = new ControllerContext(actionContext)
            };
        }

        [TestMethod]
        public void GetRestaurants_NoInput_Returns200Ok()
        {
            _restaurantBusiness.Setup(x => x.GetRestaurants()).Returns(MockData.MockData.restaurants);

            var result = _restaurantController.GetRestaurants() as OkObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
        }

        [TestMethod]
        public void GetRestaurants_NoInput_Returns404NotFound()
        {
            _restaurantBusiness.Setup(x => x.GetRestaurants()).Returns(new List<AppUser>());

            var result = _restaurantController.GetRestaurants() as StatusCodeResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [TestMethod]
        public void GetRestaurants_NoInput_Returns500InternalServer()
        {
            _restaurantBusiness.Setup(x => x.GetRestaurants()).Throws(new Exception());

            var result = _restaurantController.GetRestaurants() as StatusCodeResult;

            Assert.AreEqual(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [TestMethod]
        public async Task DeleteRestaurant_InputId_Returns200Ok()
        {
            _restaurantBusiness.Setup(x => x.DeleteRestaurant(It.IsAny<int>()));
            int id = 1;
            var result = await _restaurantController.DeleteRestaurant(id) as OkObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
        }

        [TestMethod]
        public async Task DeleteRestaurant_InputId_Returns404NotFound()
        {
            _restaurantBusiness.Setup(x => x.DeleteRestaurant(It.IsAny<int>())).Throws(new ArgumentException());
            int id = 1;
            var result = await _restaurantController.DeleteRestaurant(id) as StatusCodeResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [TestMethod]
        public async Task DeleteRestaurant_InputId_Returns500InternalServer()
        {
            _restaurantBusiness.Setup(x => x.DeleteRestaurant(It.IsAny<int>())).Throws(new Exception());
            int id = 1;
            var result = await _restaurantController.DeleteRestaurant(id) as StatusCodeResult;

            Assert.AreEqual(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [TestMethod]
        public async Task Register_RegisterData_Returns201Created()
        {
            _restaurantBusiness.Setup(x => x.Register(It.IsAny<RegisterDetails>(), It.IsAny<string>()));
            var registerDetails = new RegisterDetails()
            {
                Name = "NewRestaurant",
                Email = "newRestaurant@gmail.com",
                Password = "Password@123",
                ContactNo = 2143532,
                Address = "Restaurant1 Address"

            };

            var result = await _restaurantController.RegisterAsRestaurant(registerDetails) as StatusCodeResult;
            Assert.AreEqual(StatusCodes.Status201Created, result.StatusCode);
        }

        [TestMethod]
        public async Task Register_RegisterData_ThrowsArgumentException_Returns400BadRequest()
        {
            _restaurantBusiness.Setup(x => x.Register(It.IsAny<RegisterDetails>(), It.IsAny<string>())).Throws(new ArgumentException());
            var registerDetails = new RegisterDetails()
            {
                Name = "NewRestaurant",
                Email = "newRestaurant@gmail.com",
                Password = "Password@123",
                ContactNo = 2143532,
                Address = "Restaurant1 Address"

            };

            var result = await _restaurantController.RegisterAsRestaurant(registerDetails) as BadRequestObjectResult;
            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
        }

        [TestMethod]
        public async Task Register_RegisterData_ThrowsInvalidOperationException_Returns400BadRequest()
        {
            _restaurantBusiness.Setup(x => x.Register(It.IsAny<RegisterDetails>(), It.IsAny<string>())).Throws(new InvalidOperationException());
            var registerDetails = new RegisterDetails()
            {
                Name = "NewRestaurant",
                Email = "newRestaurant@gmail.com",
                Password = "Password@123",
                ContactNo = 2143532,
                Address = "Restaurant1 Address"

            };

            var result = await _restaurantController.RegisterAsRestaurant(registerDetails) as BadRequestObjectResult;
            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
        }

        [TestMethod]
        public async Task Register_RegisterData_Returns500InternalServer()
        {
            _restaurantBusiness.Setup(x => x.Register(It.IsAny<RegisterDetails>(), It.IsAny<string>())).Throws(new Exception());
            var registerDetails = new RegisterDetails()
            {
                Name = "NewRestaurant",
                Email = "newRestaurant@gmail.com",
                Password = "Password@123",
                ContactNo = 2143532,
                Address = "Restaurant1 Address"

            };

            var result = await _restaurantController.RegisterAsRestaurant(registerDetails) as StatusCodeResult;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [TestMethod]
        public void GetRestaurantMenu_InputId_Returns200Ok()
        {
            _restaurantBusiness.Setup(x => x.GetRestaurantMenu(It.IsAny<int>())).Returns(MockData.MockData.menu);
            int id = 1;
            var result = _restaurantController.GetRestaurantMenu(id) as OkObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
        }

        [TestMethod]
        public void GetRestaurantMenu_InputId_Returns404NotFound()
        {
            _restaurantBusiness.Setup(x => x.GetRestaurantMenu(It.IsAny<int>())).Returns(new List<MenuProduct>());
            int id = 1;
            var result = _restaurantController.GetRestaurantMenu(id) as StatusCodeResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [TestMethod]    
        public void GetRestaurantMenu_InputId_Returns500InternalServerError()
        {
            _restaurantBusiness.Setup(x => x.GetRestaurantMenu(It.IsAny<int>())).Throws(new Exception());
            int id = 1;
            var result = _restaurantController.GetRestaurantMenu(id) as StatusCodeResult;

            Assert.AreEqual(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [TestMethod]
        public void GetRestaurantDetailsById_InputId_Returns200Ok()
        {
            _restaurantBusiness.Setup(x => x.GetRestaurantDetailsById(It.IsAny<int>())).Returns(MockData.MockData.mockRestaurantData);
            int id = 1;
            var result = _restaurantController.GetRestaurantDetailsById(id) as OkObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
        }

        [TestMethod]
        public void GetRestaurantDetailsById_InputId_Returns404NotFound()
        {
            _restaurantBusiness.Setup(x => x.GetRestaurantDetailsById(It.IsAny<int>())).Returns(value : null);
            int id = 1;
            var result = _restaurantController.GetRestaurantDetailsById(id) as StatusCodeResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [TestMethod]
        public void GetRestaurantDetailsById_InputId_Returns500InternalServer()
        {
            _restaurantBusiness.Setup(x => x.GetRestaurantDetailsById(It.IsAny<int>())).Throws(new Exception());
            int id = 1;
            var result = _restaurantController.GetRestaurantDetailsById(id) as StatusCodeResult;

            Assert.AreEqual(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [TestMethod]
        public void Update_UpdatedUserData_Returns200Ok()
        {
            _restaurantBusiness.Setup(x => x.Update(It.IsAny<JsonPatchDocument<AppUser>>(), It.IsAny<string>()));

            var operation = new Operation<AppUser>("replace", "/Name", "", "NewName");
            var updatedUser = new JsonPatchDocument<AppUser>
                (
                    new List<Operation<AppUser>>() { operation },
                    new DefaultContractResolver()
                );

            var result = _restaurantController.Update(updatedUser) as OkObjectResult;

            Assert.AreEqual(result.StatusCode, StatusCodes.Status200OK);
        }

        [TestMethod]
        public void Update_UpdatedUserData_Returns400BadRequest() {
            _restaurantBusiness.Setup(x => x.Update(It.IsAny<JsonPatchDocument<AppUser>>(), It.IsAny<string>())).Throws(new Exception());

            var operation = new Operation<AppUser>("replace", "/Name", "", "NewName");
            var updatedUser = new JsonPatchDocument<AppUser>
                (
                    new List<Operation<AppUser>>() { operation },
                    new DefaultContractResolver()
                );

            var result = _restaurantController.Update(updatedUser) as BadRequestObjectResult;

            Assert.AreEqual(result.StatusCode, StatusCodes.Status400BadRequest);
        }
    }
}
