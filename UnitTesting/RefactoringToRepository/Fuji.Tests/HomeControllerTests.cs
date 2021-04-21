using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using Fuji.Data.Abstract;
using Fuji.Models;
using Fuji.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Fuji.Tests
{
    public class HomeControllerTests
    {
        // Would be easy to parameterize this with the username, email or id of the Identity user
        private static HomeController GetHomeControllerWithLoggedInUser()
        {
            // Mock the apple repository
            Mock<IAppleRepository> mockAppleRepo = new Mock<IAppleRepository>();
            mockAppleRepo.Setup(m => m.GetAll()).Returns(new Apple[]
                {
                    new Apple {Id = 1, VarietyName = "MacBook", ScientificName = "Galus Indictus"},
                    new Apple {Id = 2, VarietyName = "iPhone", ScientificName = "Poculum Relego"},
                    new Apple {Id = 3, VarietyName = "iWatch", ScientificName = "Inflo Fugitivus"}
                }.AsQueryable<Apple>());

            // Mock a user store, which the user manager needs to access the data layer, "contains methods for adding, removing and retrieving user claims."
            var mockStore = new Mock<IUserStore<IdentityUser>>();
            mockStore.Setup(x => x.FindByIdAsync("aabbcc", CancellationToken.None))
                .ReturnsAsync(new IdentityUser()
                {
                    UserName = "test@email.com",
                    Id = "aabbcc"
                });

            // Mock the user manager, only so far as it returns one valid user (can change this to return user not found for other tests)
            Mock<UserManager<IdentityUser>> mockUserManager = new Mock<UserManager<IdentityUser>>(mockStore.Object, null, null, null, null, null, null, null, null);

            /*    var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, "1"),
                }));
            */
            // Set it up enough so it doesn't return null, i.e. just some user
            mockUserManager.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(
                new IdentityUser
                {
                    Id = "aabbcc",
                    Email = "test@email.com"
                });

            // Mock the HttpContext since quite a bit of functionality comes from it
            Mock<HttpContext> mockContext = new Mock<HttpContext>();
            mockContext.SetupGet(ctx => ctx.User.Identity.Name).Returns("test@email.com");

            HomeController controller = new HomeController(null, mockUserManager.Object, null, mockAppleRepo.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockContext.Object
                }
            };

            return controller;
        }

        [Test]
        public async Task HomeController_IndexViewModelHasListOfApples()
        {
            // Mock the apple repository
            Mock<IAppleRepository> mockAppleRepo = new Mock<IAppleRepository>();
            mockAppleRepo.Setup(m => m.GetAll()).Returns(new Apple[]
                {
                    new Apple {Id = 1, VarietyName = "MacBook", ScientificName = "Galus Indictus"},
                    new Apple {Id = 2, VarietyName = "iPhone", ScientificName = "Poculum Relego"},
                    new Apple {Id = 3, VarietyName = "iWatch", ScientificName = "Inflo Fugitivus"}
                }.AsQueryable<Apple>());

            // Mock a user store, which the user manager needs to access the data layer, "contains methods for adding, removing and retrieving user claims."
            var mockStore = new Mock<IUserStore<IdentityUser>>();
            mockStore.Setup(x => x.FindByIdAsync("aabbcc", CancellationToken.None))
                .ReturnsAsync(new IdentityUser()
                {
                    UserName = "test@email.com",
                    Id = "aabbcc"
                });

            // Mock the user manager, only so far as it returns one valid user (can change this to return user not found for other tests)
            Mock<UserManager<IdentityUser>> mockUserManager = new Mock<UserManager<IdentityUser>>(mockStore.Object, null, null, null, null, null, null, null, null);
            // Set it up enough so it doesn't return null, i.e. just some user
            mockUserManager.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(
                new IdentityUser
                {
                    Id = "aabbcc",
                    Email = "test@email.com"
                });

            //Arrange
            HomeController controller = new HomeController(null, mockUserManager.Object, null, mockAppleRepo.Object);

            //Act
            IActionResult result = await controller.Index();
            MainPageVM vm = (result as ViewResult).ViewData.Model as MainPageVM;

            //Assert
            Assert.That(vm.Apples.Count(), Is.EqualTo(3));
            Assert.That(vm.Apples.ElementAt(0).Id, Is.EqualTo(1));
            Assert.That(vm.Apples.ElementAt(0).VarietyName, Is.EqualTo("MacBook"));
            Assert.That(vm.Apples.ElementAt(0).ScientificName, Is.EqualTo("Galus Indictus"));
        }

        [Test]
        public async Task HomeController_LoggedInUserIsSetInVM()
        {
            HomeController controller = GetHomeControllerWithLoggedInUser();

            IActionResult result = await controller.Index();
            MainPageVM vm = (result as ViewResult).ViewData.Model as MainPageVM;

            string username = controller.User.Identity.Name;        // <-- this one works because we set it up with: mockContext.SetupGet(ctx => ctx.User.Identity.Name).Returns()
            //bool isAuthenticated = controller.User.Identity.IsAuthenticated;   // <-- can similarly set up this one the same way

            // This isn't testing any functionality of the controller actually, just making sure we have our mock (stub in this case)
            // set up properly
            //Assert.That(isAuthenticated, Is.True);
            Assert.That(username, Is.EqualTo("test@email.com"));
        }

        // Can test if an action method exists and that it returns the correct type.  Useful for ensuring an api
        // endpoint exists and hasn't been modified from the specified return type.

        [Test]
        public void HomeController_HasPrivacyEndpoint()
        {
            HomeController controller = GetHomeControllerWithLoggedInUser();

            var result = controller.Privacy();

            Assert.That(result, Is.TypeOf<ViewResult>());
        }
    }
}
