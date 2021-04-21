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
    public class AppleRepo
    {
        // a helper to make dbset queryable
        private Mock<DbSet<T>> GetMockDbSet<T>(IQueryable<T> entities) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(entities.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(entities.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(entities.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(entities.GetEnumerator());
            return mockSet;
        }

        [Test]
        public void AppleRepo_TotalConsumedWhenNoneConsumedIs_Zero()
        {
            // Mock the apple repository
            List<ApplesConsumed> applesConsumed = new List<ApplesConsumed>
            {
                // None consumed for this test, so empty.  Reuse it for each apple below (since it's empty)
                //new ApplesConsumed { AppleId = 1, FujiUserId = 2 }
            };

            List<Apple> apples = new List<Apple>
            {
                new Apple {Id = 1, VarietyName = "MacBook", ScientificName = "Galus Indictus", ApplesConsumeds = applesConsumed},
                new Apple {Id = 2, VarietyName = "iPhone", ScientificName = "Poculum Relego", ApplesConsumeds = applesConsumed},
                new Apple {Id = 3, VarietyName = "iWatch", ScientificName = "Inflo Fugitivus", ApplesConsumeds = applesConsumed}
            };

            Mock<DbSet<Apple>> mockApplesDbSet = GetMockDbSet(apples.AsQueryable());
            //mockApplesDbSet.Setup(a => a.Include("ApplesConsumeds")).Returns(mockApples.Object);

            Mock<FujiDbContext> mockContext = new Mock<FujiDbContext>();
            mockContext.Setup(ctx => ctx.Apples).Returns(mockApplesDbSet.Object);

            // Arrange
            IAppleRepository appleRepo = new AppleRepository(mockContext.Object);

            // Act
            int totalConsumed = appleRepo.GetTotalConsumed(apples.AsQueryable<Apple>());

            // Assert
            Assert.That(totalConsumed, Is.EqualTo(0));
        }

        [Test]
        public void AppleRepo_TotalConsumedWith10ApplesConsumedReturns_10()
        {
            // Stub data, including populating navigation properties
            List<Apple> apples = new List<Apple>
            {
                new Apple {Id = 1, VarietyName = "MacBook", ScientificName = "Galus Indictus", ApplesConsumeds = new List<ApplesConsumed>{
                        new ApplesConsumed { AppleId = 1, FujiUserId = 3, Count = 1},
                        new ApplesConsumed { AppleId = 1, FujiUserId = 2, Count = 1},
                        new ApplesConsumed { AppleId = 1, FujiUserId = 3, Count = 1}
                    }},
                new Apple {Id = 2, VarietyName = "iPhone", ScientificName = "Poculum Relego", ApplesConsumeds  = new List<ApplesConsumed>{
                        new ApplesConsumed { AppleId = 2, FujiUserId = 3, Count = 1},
                        new ApplesConsumed { AppleId = 2, FujiUserId = 2, Count = 1},
                        new ApplesConsumed { AppleId = 2, FujiUserId = 3, Count = 1}
                    }},
                new Apple {Id = 3, VarietyName = "iWatch", ScientificName = "Inflo Fugitivus", ApplesConsumeds  = new List<ApplesConsumed>{
                        new ApplesConsumed { AppleId = 3, FujiUserId = 5, Count = 1},
                        new ApplesConsumed { AppleId = 3, FujiUserId = 1, Count = 1},
                        new ApplesConsumed { AppleId = 3, FujiUserId = 1, Count = 1},
                        new ApplesConsumed { AppleId = 3, FujiUserId = 4, Count = 1}
                    }}
            };

            // Mock the apple repository
            Mock<DbSet<Apple>> mockApples = GetMockDbSet(apples.AsQueryable());
            //mockApples.Setup(a => a.Include("ApplesConsumeds")).Returns(mockApples.Object);   // cannot mock extension methods
            // See enabling Lazy loading so we don't have to use Include: https://docs.microsoft.com/en-us/ef/core/querying/related-data/lazy

            Mock<FujiDbContext> mockContext = new Mock<FujiDbContext>();
            mockContext.Setup(ctx => ctx.Apples).Returns(mockApples.Object);

            // Arrange
            IAppleRepository appleRepo = new AppleRepository(mockContext.Object);

            // Act
            int totalConsumed = appleRepo.GetTotalConsumed(apples.AsQueryable<Apple>());

            // Assert
            Assert.That(totalConsumed, Is.EqualTo(10));
        }

        // Can test other configurations, perhaps where one apple hasn't been eaten but the others have
    }
}
