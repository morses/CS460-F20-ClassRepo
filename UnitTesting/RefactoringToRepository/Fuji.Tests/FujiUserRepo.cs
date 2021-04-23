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
    public class FujiUserRepo
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

        private Mock<DbSet<FujiUser>> _mockFujiUsersDbSet;
        private Mock<FujiDbContext> _mockContext;
        private List<FujiUser> _users;

        [SetUp]
        public void Setup()
        {
            // Stub data, including populating navigation properties
            _users = new List<FujiUser>
            {
                new FujiUser {Id = 1, ApplesConsumeds = new List<ApplesConsumed>()},
                new FujiUser {Id = 2, ApplesConsumeds  = new List<ApplesConsumed>{
                        new ApplesConsumed { AppleId = 1, FujiUserId = 2, Count = 1},
                        new ApplesConsumed { AppleId = 2, FujiUserId = 2, Count = 1},
                        new ApplesConsumed { AppleId = 3, FujiUserId = 2, Count = 1}
                    }},
                new FujiUser {Id = 3, ApplesConsumeds  = new List<ApplesConsumed>{
                        new ApplesConsumed { AppleId = 1, FujiUserId = 3, Count = 1},
                        new ApplesConsumed { AppleId = 3, FujiUserId = 3, Count = 1},
                        new ApplesConsumed { AppleId = 2, FujiUserId = 3, Count = 1},
                        new ApplesConsumed { AppleId = 3, FujiUserId = 3, Count = 1}
                    }}
            };

            // Mock the user repository
            _mockFujiUsersDbSet = GetMockDbSet(_users.AsQueryable());
            // fake the Find method (for a single integer value passed in -- a pain since the signature for Find is (params object[] keyValues)
            _mockFujiUsersDbSet.Setup(a => a.Find(It.IsAny<object[]>())).Returns((object[] x) => {
                int id = (int)x[0];
                return _users.Where(u => u.Id == id).First();
            });

            _mockContext = new Mock<FujiDbContext>();
            // fake the property getter and Set function used to get the DbSet
            _mockContext.Setup(ctx => ctx.FujiUsers).Returns(_mockFujiUsersDbSet.Object);
            _mockContext.Setup(ctx => ctx.Set<FujiUser>()).Returns(_mockFujiUsersDbSet.Object);
        }

        [Test]
        public void FujiUserRepo_NotFoundUserReturnsCountOf_0()
        {

        }

        [Test]
        public void FujiUserRepo_UserWhoHasntEatenAnyApplesReturnsCountOf_0()
        {
            // Arrange
            IFujiUserRepository fujiUserRepo = new FujiUserRepository(_mockContext.Object);

            // Act
            Apple[] apples = new Apple[] {
                new Apple {Id = 1, VarietyName = "MacBook", ScientificName = "Galus Indictus"},
                new Apple {Id = 2, VarietyName = "iPhone", ScientificName = "Poculum Relego" },
                new Apple {Id = 3, VarietyName = "iWatch", ScientificName = "Inflo Fugitivus"}
            };
            Dictionary<Apple, int> result = fujiUserRepo.GetCountOfSpecificApplesEaten(apples, new FujiUser { Id = 1 });

            // Assert
            Assert.That(result[apples[0]], Is.EqualTo(0));
            Assert.That(result[apples[1]], Is.EqualTo(0));
            Assert.That(result[apples[2]], Is.EqualTo(0));
        }

        [Test]
        public void FujiUserRepo_UserWhoseEaten3DifferentApplesReturnsCountOf_3()
        {
            // Arrange
            IFujiUserRepository fujiUserRepo = new FujiUserRepository(_mockContext.Object);

            // Act
            Apple[] apples = new Apple[] {
                new Apple {Id = 1, VarietyName = "MacBook", ScientificName = "Galus Indictus"},
                new Apple {Id = 2, VarietyName = "iPhone", ScientificName = "Poculum Relego" },
                new Apple {Id = 3, VarietyName = "iWatch", ScientificName = "Inflo Fugitivus"}
            };
            Dictionary<Apple, int> result = fujiUserRepo.GetCountOfSpecificApplesEaten(apples, new FujiUser { Id = 2 });

            // Assert
            Assert.That(result[apples[0]], Is.EqualTo(1));
            Assert.That(result[apples[1]], Is.EqualTo(1));
            Assert.That(result[apples[2]], Is.EqualTo(1));
        }

        [Test]
        public void FujiUserRepo_AskingForWhichApplesEatenWhenSupplyingOnlySpecificApplesCorrectlyFiltersList()
        {
            // Arrange
            // (not good to have logic in the arrange here, i.e. the _users.Where to select which user we're setting it up for
            // Could have hard-coded it in as element #2 of the list but that is fragile since we might edit that stub data 
            // in the future and it could break the test for no good reason.

            IFujiUserRepository fujiUserRepo = new FujiUserRepository(_mockContext.Object);

            // Act
            Apple[] apples = new Apple[] {
                new Apple {Id = 1, VarietyName = "MacBook", ScientificName = "Galus Indictus"},
                //new Apple {Id = 2, VarietyName = "iPhone", ScientificName = "Poculum Relego" },
                new Apple {Id = 3, VarietyName = "iWatch", ScientificName = "Inflo Fugitivus"}
            };
            Dictionary<Apple, int> result = fujiUserRepo.GetCountOfSpecificApplesEaten(apples, new FujiUser { Id = 2 });

            // Assert
            Assert.That(result.Count == 2);
            Assert.That(result[apples[0]], Is.EqualTo(1));
            Assert.That(result[apples[1]], Is.EqualTo(1));
        }

    }
}
