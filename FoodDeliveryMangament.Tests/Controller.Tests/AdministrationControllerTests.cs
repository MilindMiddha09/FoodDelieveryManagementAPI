using FoodDelieveryManagementAPI.Business.Interfaces;
using FoodDelieveryManagementAPI.Controllers;
using FoodDelieveryManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FoodDeliveryMangament.Tests.Controller.Tests
{
    [TestClass]
    public class AdministrationControllerTests
    {
        private Mock<IAdministrationBusiness> _administrationBusiness;
        private AdministrationController _administrationController;

        [TestInitialize]
        public void Initialize()
        {
            _administrationBusiness = new Mock<IAdministrationBusiness>();
            _administrationController = new AdministrationController( _administrationBusiness.Object );
        }

        [TestMethod]
        public async Task CreateRole_RoleName_Return200Ok()
        {
            _administrationBusiness.Setup(x => x.CreateRole(It.IsAny<Usertype>()));

            var role = new Usertype()
            {
                RoleName = "Test"
            };

            var result = await _administrationController.CreateRole(role) as OkObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
        }

        [TestMethod]
        public async Task CreateRole_RoleName_Return500InternalServerError()
        {
            _administrationBusiness.Setup(x => x.CreateRole(It.IsAny<Usertype>())).Throws(new Exception());

            var role = new Usertype()
            {
                RoleName = "Test"
            };

            var result = await _administrationController.CreateRole(role) as StatusCodeResult;

            Assert.AreEqual(StatusCodes.Status500InternalServerError, result.StatusCode);
        }
    }
}
