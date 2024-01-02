using FoodDelieveryManagementAPI.Business.Interfaces;
using FoodDelieveryManagementAPI.Controllers;
using FoodDelieveryManagementAPI.Models;
using FoodDeliveryMangament.Tests.MockData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json.Serialization;
using System.Security.Principal;

namespace FoodDeliveryMangament.Tests.Controller.Tests
{
    [TestClass]
    public class CustomerControllerTests
    {
        private Mock<ICustomerBusiness> _customerBusiness;
        private CustomerController _customerController;


        [TestInitialize]
        public void Initialize()
        {
            var httpContext = new DefaultHttpContext()
            {
                User = new System.Security.Claims.ClaimsPrincipal(new GenericIdentity("username"))
            };

            var actionContext = new ActionContext(httpContext, new Microsoft.AspNetCore.Routing.RouteData(),
                                                   new Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor());
            _customerBusiness = new Mock<ICustomerBusiness>();
            _customerController = new CustomerController(_customerBusiness.Object)
            {
                ControllerContext = new ControllerContext(actionContext)
            };
        }

        [TestMethod]
        public void GetCustomers_NoInput_Returns200Ok()
        {
            _customerBusiness.Setup(x => x.GetCustomerList()).Returns(MockData.MockData.customers);

            var result = _customerController.GetCustomers() as OkObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
        }

        [TestMethod]
        public void GetCustomers_NoInput_Returns500InternalServer()
        {
            _customerBusiness.Setup(x => x.GetCustomerList()).Throws(new Exception());

            var result = _customerController.GetCustomers() as StatusCodeResult;

            Assert.AreEqual(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [TestMethod]
        public void GetCustomers_NoInput_Returns404NotFound()
        {
            _customerBusiness.Setup(x => x.GetCustomerList()).Returns(new List<AppUser>());

            var result = _customerController.GetCustomers() as StatusCodeResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [TestMethod]
        public void Update_UpdatedUser_Returns200Ok()
        {
            _customerBusiness.Setup(x => x.Update(It.IsAny<JsonPatchDocument<AppUser>>(), It.IsAny<string>()));

            var operation = new Operation<AppUser>("replace", "/Name", "", "NewName");
            var updatedUser = new JsonPatchDocument<AppUser>
                (
                    new List<Operation<AppUser>>() { operation },
                    new DefaultContractResolver()
                );

            var result = _customerController.Update(updatedUser) as OkObjectResult;

            Assert.AreEqual(result.StatusCode, StatusCodes.Status200OK);
        }

        [TestMethod]
        public void Update_UpdatedUser_ReturnsBadRequest()
        {
            _customerBusiness.Setup(x => x.Update(It.IsAny<JsonPatchDocument<AppUser>>(), It.IsAny<string>())).Throws(new Exception());

            var operation = new Operation<AppUser>("replace", "/Name", "", "NewName");
            var updatedUser = new JsonPatchDocument<AppUser>
                (
                    new List<Operation<AppUser>>() { operation },
                    new DefaultContractResolver()
                );

            var result = _customerController.Update(updatedUser) as BadRequestObjectResult;

            Assert.AreEqual(result.StatusCode, StatusCodes.Status400BadRequest);
        }

        [TestMethod]
        public async Task Delete_InputId_Returns200Ok()
        {
            _customerBusiness.Setup(x => x.DeleteCustomer(It.IsAny<int>()));
            int id = 1;
            var result = await _customerController.DeleteCustomer(id) as OkObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
        }

        [TestMethod]
        public async Task Delete_InputId_Returns404NotFound()
        {
            _customerBusiness.Setup(x => x.DeleteCustomer(It.IsAny<int>())).Throws(new ArgumentException());
            int id = 1;
            var result = await _customerController.DeleteCustomer(id) as StatusCodeResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [TestMethod]
        public async Task Delete_InputId_Returns500InternalServer()
        {
            _customerBusiness.Setup(x => x.DeleteCustomer(It.IsAny<int>())).Throws(new Exception());
            int id = 1;
            var result = await _customerController.DeleteCustomer(id) as StatusCodeResult;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, result.StatusCode);
        }
        [TestMethod]
        public async Task Register_RegisterData_Returns201Created()
        {
            _customerBusiness.Setup(x => x.Register(It.IsAny<RegisterDetails>(), It.IsAny<string>()));
            var registerDetails = new RegisterDetails()
            {
                Name = "NewCustomer",
                Email = "newCustomer@gmail.com",
                Password = "Password@123",
                ContactNo = 2143532,
                Address = "Customer1 Address"

            };

            var result = await _customerController.Register(registerDetails) as StatusCodeResult;
            Assert.AreEqual(StatusCodes.Status201Created, result.StatusCode);
        }

        [TestMethod]
        public async Task Register_RegisterData_ThrowsArgumentException_ReturnsBadRequest()
        {
            _customerBusiness.Setup(x => x.Register(It.IsAny<RegisterDetails>(), It.IsAny<string>())).Throws(new ArgumentException());
            var registerDetails = new RegisterDetails()
            {
                Name = "NewCustomer",
                Email = "newCustomer@gmail.com",
                Password = "Password@123",
                ContactNo = 2143532,
                Address = "Customer1 Address"
            };

            var result = await _customerController.Register(registerDetails) as BadRequestObjectResult;
            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
        }

        [TestMethod]
        public async Task Register_RegisterData_ThrowsInvalidOperationException_ReturnsBadRequest()
        {
            _customerBusiness.Setup(x => x.Register(It.IsAny<RegisterDetails>(), It.IsAny<string>())).Throws(new InvalidOperationException());
            var registerDetails = new RegisterDetails()
            {
                Name = "NewCustomer",
                Email = "newCustomer@gmail.com",
                Password = "Password@123",
                ContactNo = 2143532,
                Address = "Customer1 Address"
            };

            var result = await _customerController.Register(registerDetails) as BadRequestObjectResult;
            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
        }

        [TestMethod]
        public async Task Register_RegisterData_Returns500InternalServer()
        {
            _customerBusiness.Setup(x => x.Register(It.IsAny<RegisterDetails>(), It.IsAny<string>())).Throws(new Exception());
            var registerDetails = new RegisterDetails()
            {
                Name = "NewCustomer",
                Email = "newCustomer@gmail.com",
                Password = "Password@123",
                ContactNo = 2143532,
                Address = "Customer1 Address"
            };

            var result = await _customerController.Register(registerDetails) as StatusCodeResult;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

    }
}
