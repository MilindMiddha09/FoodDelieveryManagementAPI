using FoodDelieveryManagementAPI.Business.Interfaces;
using FoodDelieveryManagementAPI.Controllers;
using FoodDelieveryManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json.Serialization;
using System.Security.Principal;

namespace FoodDeliveryMangament.Tests.Controller.Tests
{
    [TestClass]
    public class AdminControllerTests
    {
        private Mock<IAdminBusiness> _adminBusiness;
        private AdminController _adminController;

        [TestInitialize]
        public void Initialize()
        {
            _adminBusiness = new Mock<IAdminBusiness>();

            var httpContext = new DefaultHttpContext()
            {
                User = new System.Security.Claims.ClaimsPrincipal(new GenericIdentity("username"))
            };

            var actionContext = new ActionContext(httpContext, new Microsoft.AspNetCore.Routing.RouteData(),
                                                   new Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor());

            _adminController = new AdminController(_adminBusiness.Object)
            { 
                ControllerContext = new ControllerContext(actionContext)
            };

        }

        [TestMethod]
        public void GetAdmin_NoInput_Returns200Ok()
        {
            _adminBusiness.Setup(x => x.GetAdminList()).Returns(MockData.MockData.admins);

            var result = _adminController.GetAdmins() as OkObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK,result.StatusCode);
        }

        [TestMethod]
        public void GetAdmin_NoInput_Return500InternalServer()
        {
            _adminBusiness.Setup(x => x.GetAdminList()).Throws(new Exception());

            var result = _adminController.GetAdmins() as StatusCodeResult;

            Assert.AreEqual(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [TestMethod]
        public void GetAdmin_NoInput_Returns404NotFound()
        {
            _adminBusiness.Setup(x => x.GetAdminList()).Returns(new List<AppUser>());

            var result = _adminController.GetAdmins() as NotFoundObjectResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [TestMethod]
        public void Update_UpdatedUser_Returns200Ok()
        {
            _adminBusiness.Setup(x => x.Update(It.IsAny<JsonPatchDocument<AppUser>>(), It.IsAny<string>()));

            var operation = new Operation<AppUser>("replace", "/Name", "","NewName");
            var updatedUser = new JsonPatchDocument<AppUser>
                (
                    new List<Operation<AppUser>>() { operation },
                    new DefaultContractResolver()
                );

            var result = _adminController.Update(updatedUser) as OkObjectResult;

            Assert.AreEqual(result.StatusCode,StatusCodes.Status200OK);
        }

        [TestMethod]
        public void Update_UpdatedUser_ReturnsBadRequest()
        {
            _adminBusiness.Setup(x => x.Update(It.IsAny<JsonPatchDocument<AppUser>>(), It.IsAny<string>())).Throws(new Exception());

            var operation = new Operation<AppUser>("replace", "/Name", "", "NewName");
            var updatedUser = new JsonPatchDocument<AppUser>
                (
                    new List<Operation<AppUser>>() { operation },
                    new DefaultContractResolver()
                );

            var result = _adminController.Update(updatedUser) as BadRequestObjectResult;

            Assert.AreEqual(result.StatusCode, StatusCodes.Status400BadRequest);
        }

        [TestMethod]
        public async Task Register_RegisterData_Returns201Created()
        {
            _adminBusiness.Setup(x => x.Register(It.IsAny<RegisterDetails>(), It.IsAny<string>()));
            var registerDetails = new RegisterDetails()
            {
                Name = "NewAdmin",
                Email = "newAdmin@gmail.com",
                Password = "Password@123"
            };

            var result = await _adminController.RegisterAsAdmin(registerDetails) as StatusCodeResult;
            Assert.AreEqual(StatusCodes.Status201Created, result.StatusCode);
        }

        [TestMethod]
        public async Task Register_RegisterData_ThrowsArgumentException_ReturnsBadRequest()
        {
            _adminBusiness.Setup(x => x.Register(It.IsAny<RegisterDetails>(), It.IsAny<string>())).Throws(new ArgumentException());
            var registerDetails = new RegisterDetails()
            {
                Name = "NewAdmin",
                Email = "newAdmin@gmail.com",
                Password = "Password@123"
            };

            var result = await _adminController.RegisterAsAdmin(registerDetails) as BadRequestObjectResult;
            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
        }

        [TestMethod]
        public async Task Register_RegisterData_ThrowsInvalidOperationException_ReturnsBadRequest()
        {
            _adminBusiness.Setup(x => x.Register(It.IsAny<RegisterDetails>(), It.IsAny<string>())).Throws(new InvalidOperationException());
            var registerDetails = new RegisterDetails()
            {
                Name = "NewAdmin",
                Email = "newAdmin@gmail.com",
                Password = "Password@123"
            };

            var result = await _adminController.RegisterAsAdmin(registerDetails) as BadRequestObjectResult;
            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
        }

        [TestMethod]
        public async Task Register_Register_Returns500InternalServer()
        {
            _adminBusiness.Setup(x => x.Register(It.IsAny<RegisterDetails>(), It.IsAny<string>())).Throws(new Exception());
            var registerDetails = new RegisterDetails()
            {
                Name = "NewAdmin",
                Email = "newAdmin@gmail.com",
                Password = "Password@123"
            };

            var result = await _adminController.RegisterAsAdmin(registerDetails) as StatusCodeResult;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [TestMethod]
        public async Task Delete_InputId_Returns200Ok()
        {
            _adminBusiness.Setup(x => x.DeleteAdmin(It.IsAny<int>()));
            int id = 1;
            var result = await _adminController.DeleteAdmin(id) as OkObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
        }

        [TestMethod]
        public async Task Delete_InputId_Returns404NotFound()
        {
            _adminBusiness.Setup(x => x.DeleteAdmin(It.IsAny<int>())).Throws(new ArgumentException());
            int id = 1;
            var result = await _adminController.DeleteAdmin(id) as StatusCodeResult;

            Assert.AreEqual(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [TestMethod]
        public async Task Delete_InputId_Returns500InternalServer()
        {
            _adminBusiness.Setup(x => x.DeleteAdmin(It.IsAny<int>())).Throws(new Exception());
            int id = 1;
            var result = await _adminController.DeleteAdmin(id) as StatusCodeResult;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, result.StatusCode);
        }
    }
}
