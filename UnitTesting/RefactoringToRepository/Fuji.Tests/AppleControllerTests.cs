using NUnit.Framework;
using Moq;
using Fuji.Data.Abstract;
using Fuji.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Fuji.Data;
using Fuji.Data.Concrete;

namespace Fuji.Tests
{
    public class AppleControllerTests
    {

        // Tests we could have written with the infrastructure we've built to this point

        [Test]
        public void AppleController_Ate_AteEndpointExistsAndHasCorrectSignatureReturns_Json()
        {
            Assert.Fail();
        }

        [Test]
        public void AppleController_Ate_UserNotLoggedInEatAppleReturnsSuccess_False()
        {
            Assert.Fail();
        }

        [Test]
        public void AppleController_Ate_LoggedInUserEatsNonExistentAppleReturnsSuccess_False()
        {
            Assert.Fail();
        }

        [Test]
        public void AppleController_Ate_LoggedInUserEatsAppleReturnsSuccess_True()
        {
            Assert.Fail();
        }

        [Test]
        public void AppleController_Eaten_LoggedInUserWithAccountReturnsSuccess_True()
        {
            Assert.Fail();
        }

        [Test]
        public void AppleController_Eaten_LoggedInUserWithAccountWhoHasntEatenAnyApplesReturns_EmptyListOfApples()
        {
            Assert.Fail();
        }

        [Test]
        public void AppleController_Eaten_LoggedInUserWithAccountWhoHasEaten3ApplesReturns_ListOfApplesWithCorrect3()
        {
            Assert.Fail();
        }

        //...
    }
}
