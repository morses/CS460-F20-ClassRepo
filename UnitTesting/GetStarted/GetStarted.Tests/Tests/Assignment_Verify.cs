using NUnit.Framework;
using GetStarted.Models;
using System;

namespace GetStarted.Tests.Tests
{

    [TestFixture]
    public class Assignment_Verify
    {
        private static Assignment MakeValidAssignment()
        {
            return new Assignment
            {
                Id = 1,
                Title = "Homework 1",
                Description = "A description of the homework assignment",
                Priority = 3,
                Course = "CS 123",
                DueDate = DateTime.UtcNow + new TimeSpan(48, 0, 0),
                Completed = false,
                Submitted = false
            };
        }

        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void Assignment_DefaultIs_NotValid()
        {
            // Arrange 
            Assignment a = new Assignment();
            // Act
            ModelValidator mv = new ModelValidator(a);
            // Assert
            //Assert.That(a.Title,Is.Null);
            Assert.That(mv.Valid, Is.False);
        }

        [Test]
        public void Assignment_RequiredNullablePropertiesAreNullMeansIs_NotValid()
        {
            // Arrange
            Assignment a = new Assignment
            {
                Title = null,
                Description = null,
                Course = null
            };
            // Act
            ModelValidator mv = new ModelValidator(a);
            // Assert
            // Was one reason why it failed that the Title property didn't validate?
            Assert.That(mv.ContainsFailureFor("Title"), Is.True);
            Assert.That(mv.ContainsFailureFor("Description"), Is.True);
            Assert.That(mv.ContainsFailureFor("Course"), Is.True);
            // Did it fail? (this is a more general test so should go last)
            Assert.That(mv.Valid, Is.False);
        }

        [Test]
        public void Assignment_AllRequiredPropertiesAreNonNullAndValidValuesIs_Valid()
        {
            // Arrange
            Assignment a = MakeValidAssignment();
            // Act
            ModelValidator mv = new ModelValidator(a);
            // Assert
            Assert.That(mv.Valid, Is.True);
        }

        // ************* Priority ************
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void Assignment_PriorityWithinRangeIs_Valid(int p)
        {
            // Arrange
            Assignment a = MakeValidAssignment();
            a.Priority = p;
            // Act
            ModelValidator mv = new ModelValidator(a);
            // Assert
            Assert.That(mv.Valid, Is.True);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(6)]
        [TestCase(7+6)]
        [TestCase(7)]
        [TestCase(8)]
        public void Assignment_PriorityOutsideOfRangeIs_NotValid(int p)
        {
            // Arrange
            Assignment a = MakeValidAssignment();
            a.Priority = p;
            // Act
            ModelValidator mv = new ModelValidator(a);
            // Assert
            Assert.That(mv.ContainsFailureFor("Priority"), Is.True);
            Assert.That(mv.Valid, Is.False);
        }

        // ************* Course ************
        // Should fail validation: null, "", "   ", "hello", "laksdjflasdj", "cs 450", "CS 4", "CS444", "cs", "a", "anth 456h", "AST 123b", "541-843-7882", "MTH 354-876-7733"

        // Should pass validation: "A 345", "AB 345", "ABC 234", "ABCD 292", ...
        [Test]
        public void Assignment_EmptyStringCourseNameIs_NotValid()
        {
            Assignment a = MakeValidAssignment();
            a.Course = "";

            ModelValidator mv = new ModelValidator(a);

            Assert.That(mv.ContainsFailureFor("Course"), Is.True);
            Assert.That(mv.Valid, Is.False);
        }

        [TestCase("CS 123")]
        [TestCase("BA 345")]
        [TestCase("A 101")]
        [TestCase("ANTH 277")]
        [TestCase("MTH 354")]
        public void Assignment_CorrectBaseCourseNamesAre_Valid(string course)
        {
            Assignment a = MakeValidAssignment();
            a.Course = course;

            ModelValidator mv = new ModelValidator(a);

            Assert.That(mv.ContainsFailureFor("Course"), Is.False);
            Assert.That(mv.Valid, Is.True);
        }

        [TestCase("  CS 123")]
        [TestCase("23BA 345")]
        [TestCase(",.A 101")]
        [TestCase(" ANTH 277")]
        [TestCase("/MTH 354")]
        public void Assignment_PrefixOfCorrectBaseCourseNamesAre_NotValid(string course)
        {
            Assignment a = MakeValidAssignment();
            a.Course = course;

            ModelValidator mv = new ModelValidator(a);

            Assert.That(mv.ContainsFailureFor("Course"), Is.True);
            Assert.That(mv.Valid, Is.False);
        }

        [TestCase("cs 123")]
        [TestCase("ba 345")]
        [TestCase("a 101")]
        [TestCase("anth 277")]
        [TestCase("mth 354")]
        public void Assignment_LowercaseCourseDepartmentsAre_NotValid(string course)
        {
            Assignment a = MakeValidAssignment();
            a.Course = course;

            ModelValidator mv = new ModelValidator(a);

            Assert.That(mv.ContainsFailureFor("Course"), Is.True);
            Assert.That(mv.Valid, Is.False);
        }

        [TestCase("CS 1")]
        [TestCase("BA 32")]
        [TestCase("A ")]
        [TestCase("ANTH 0")]
        public void Assignment_CoursesWithLessThan3DigitsAre_NotValid(string course)
        {
            Assignment a = MakeValidAssignment();
            a.Course = course;

            ModelValidator mv = new ModelValidator(a);

            Assert.That(mv.ContainsFailureFor("Course"), Is.True);
            Assert.That(mv.Valid, Is.False);
        }

        [TestCase("CS 1234")]
        [TestCase("BA 23423")]
        [TestCase("A 79198221982")]
        [TestCase("ANTH 692453")]
        [TestCase("ANTH 692L 453")]
        public void Assignment_CoursesWithMoreThan3DigitsAre_NotValid(string course)
        {
            Assignment a = MakeValidAssignment();
            a.Course = course;

            ModelValidator mv = new ModelValidator(a);

            Assert.That(mv.Valid, Is.False);
        }

    }
}