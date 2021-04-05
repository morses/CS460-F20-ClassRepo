using NUnit.Framework;
using GetStarted.Utility;

namespace GetStarted.Tests.Tests
{
    public class Capitalize
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Capitalize_LowercaseStringLongerThan1LettersReturn_Success()
        {
            // Arrange
            string input = "hello";
            // Act
            string output = StringUtilities.Capitalize(input);
            // Assert
            Assert.AreEqual("Hello",output);
            // or
            Assert.That(output, Is.EqualTo("Hello"));
        }

        [Test]
        public void Capitalize_StringWith1Letter_Works()
        {
            // Arrange
            string input = "h";
            // Act
            string output = StringUtilities.Capitalize(input);
            // Assert
            Assert.AreEqual("H", output);
            // or
            Assert.That(output, Is.EqualTo("H"));
        }

        [Test]
        public void Capitalize_EmptyStringReturns_EmptyString()
        {
            // Arrange
            string input = "";
            // Act
            string output = StringUtilities.Capitalize(input);
            // Assert
            Assert.AreEqual("", output);
            // or
            Assert.That(output, Is.EqualTo(""));
        }
    }
}