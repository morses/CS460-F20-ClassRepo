using NUnit.Framework;
using GetStarted.Models;

namespace GetStarted.Tests.Tests
{

    [TestFixture]
    public class Assignment_Verify
    {
        [Test]
        public void Assignment_DefaultIs_NotValid()
        {
            // Arrange 
            Assignment a = new Assignment();
            // Act
         //   ModelValidator mv = new ModelValidator(a);
            // Assert
            Assert.That(a.Title,Is.Not.Null);
            Assert.That(mv.Valid, Is.False);
        }


    }
}