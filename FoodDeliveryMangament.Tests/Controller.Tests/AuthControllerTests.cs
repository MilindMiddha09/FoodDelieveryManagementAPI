using FoodDelieveryManagementAPI.Business.Interfaces;
using FoodDelieveryManagementAPI.Controllers;
using FoodDelieveryManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FoodDeliveryMangament.Tests.Controller.Tests
{
    [TestClass]
    public class AuthControllerTests
    {
        private Mock<IAuthBusiness> _authBusiness;
        private AuthController _authController;

        [TestInitialize]
        public void Initialize()
        {
            _authBusiness = new Mock<IAuthBusiness>();
            _authController = new AuthController(_authBusiness.Object);
        }

        [TestMethod]
        public async Task Login_LoginDetails_Returns200Ok()
        {
            _authBusiness.Setup(x => x.Login(It.IsAny<LoginDetails>()));

            var loginDetails = new LoginDetails()
            {
                Email = "testemail@gmail.com",
                Password = "TestPassword@123"
            };

            var result = await _authController.Login(loginDetails) as OkObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
        }

        [TestMethod]
        public async Task Login_InvalidLoginDetails_Returns400BadRequest()
        {
            _authBusiness.Setup(x => x.Login(It.IsAny<LoginDetails>())).Throws(new ArgumentException());

            var loginDetails = new LoginDetails()
            {
                Email = "testemail@gmail.com",
                Password = "TestPassword@123"
            };

            var result = await _authController.Login(loginDetails) as BadRequestObjectResult;

            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
        }

        [TestMethod]
        public async Task Login_InvalidLoginDetails_Returns500InternalServerError()
        {
            _authBusiness.Setup(x => x.Login(It.IsAny<LoginDetails>())).Throws(new Exception());

            var loginDetails = new LoginDetails()
            {
                Email = "testemail@gmail.com",
                Password = "TestPassword@123"
            };

            var result = await _authController.Login(loginDetails) as StatusCodeResult;

            Assert.AreEqual(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [TestMethod]
        public async Task LogOut_NoInput_Returns200Ok()
        {
            _authBusiness.Setup(x => x.Logout());
            var result = await _authController.Logout() as OkObjectResult;

            Assert.AreEqual(result.StatusCode, StatusCodes.Status200OK);
        }

        [TestMethod]
        public async Task LogOut_NoInput_Returns500InternalServerError()
        {
            _authBusiness.Setup(x => x.Logout()).Throws(new Exception());
            var result = await _authController.Logout() as StatusCodeResult;

            Assert.AreEqual(result.StatusCode, StatusCodes.Status500InternalServerError);
        }

    }
}
