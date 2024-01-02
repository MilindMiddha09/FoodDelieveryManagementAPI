using FoodDelieveryManagementAPI.Business.Interfaces;
using FoodDelieveryManagementAPI.Controllers;
using FoodDelieveryManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Principal;

namespace FoodDeliveryMangament.Tests.Controller.Tests
{
    [TestClass]
    public class MenuControllerTests
    {
        private Mock<IMenuBusiness> _menuBusiness;
        private MenuController _menuController;

        [TestInitialize]
        public void Initialize()
        {
            var httpContext = new DefaultHttpContext()
            {
                User = new System.Security.Claims.ClaimsPrincipal(new GenericIdentity("username"))
            };

            var actionContext = new ActionContext(httpContext, new Microsoft.AspNetCore.Routing.RouteData(),
                                                   new Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor());
            _menuBusiness = new Mock<IMenuBusiness>();
            _menuController = new MenuController(_menuBusiness.Object)
            {
                ControllerContext = new ControllerContext(actionContext)
            };
        }

        [TestMethod]
        public void UpdateMenu_MenuProductData_Returns201Created()
        {
            _menuBusiness.Setup(x => x.UpdateMenu(It.IsAny<MenuProduct>(), It.IsAny<string>()));

            var item = new MenuProduct()
            {
                Name = "Product 1",
                Price = 140
            };

            var result = _menuController.UpdateMenu(item) as StatusCodeResult;

            Assert.AreEqual(StatusCodes.Status201Created, result.StatusCode);
        }

        [TestMethod]
        public void UpdateMenu_MenuProductData_Returns400BadRequest()
        {
            _menuBusiness.Setup(x => x.UpdateMenu(It.IsAny<MenuProduct>(), It.IsAny<string>())).Throws(new ArgumentException());

            var item = new MenuProduct()
            {
                Name = "Product 1",
                Price = 140
            };

            var result = _menuController.UpdateMenu(item) as BadRequestObjectResult;

            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
        }

        [TestMethod]
        public void UpdateMenu_MenuProductData_Returns500InternalServerError()
        {
            _menuBusiness.Setup(x => x.UpdateMenu(It.IsAny<MenuProduct>(), It.IsAny<string>())).Throws(new Exception());

            var item = new MenuProduct()
            {
                Name = "Product 1",
                Price = 140
            };

            var result = _menuController.UpdateMenu(item) as StatusCodeResult;

            Assert.AreEqual(StatusCodes.Status500InternalServerError, result.StatusCode);
        }
    }
}
