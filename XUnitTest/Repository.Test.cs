using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using MammalAPI;
using MammalAPI.Models;
using MammalAPI.Controllers;
using MammalAPI.Services;
using Microsoft.AspNetCore.Mvc;
using MammalAPI.Context;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using System.Threading.Tasks;

namespace XUnitTest
{
    public class RepositoryTest
    {
        [Fact]
        public void AddMammalWithRepositoryMethod_Add()
        {
            // Arrange
            var mammals = TestMammals();
            var dbContext = new Mock<DBContext>();
            var logger = Mock.Of<ILogger<Repository>>();

            var contextMock = new Mock<IRepository>();
            contextMock.Setup(x => x.Add(new Mammal {}));

            var repoMock = new Repository(dbContext.Object, logger);
            

            // Act
            repoMock.Add(new Mammal 
            {
                MammalId = 3,
                Name = "Big Willy",
                LatinName = "Biggus Willus"
            });

            var saved = repoMock.Save();

            var actual = TestMammals();

            // Assert
            // Assert.Equal(3, actual.Count);
            Assert.True(saved.Result);
        }

        private List<Mammal> TestMammals()
        {
            var sessions = new List<Mammal>();
            sessions.Add(new Mammal()
            {
                MammalId = 1,
                Name = "Test Mammal One",
                LatinName = "Bapa latin apa"
            });
            sessions.Add(new Mammal()
            {
                MammalId = 2,
                Name = "Test Mammal Two",
                LatinName = "Latin kanske med"
            });

            return sessions;
        }
    }
}
