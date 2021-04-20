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

namespace Fuji.Tests
{
    public class HomeControllerTests
    {

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
    }
}
