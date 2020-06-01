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
using MammalAPI.DTO;
using System.Threading.Tasks;

namespace XUnitTest
{
    public class MammalRepositoryTest
    {

        [Theory]
        [InlineData(1, "Test Mammal One")]
        [InlineData(2, "Test Mammal Two")]
        public void GetMammalById_MammalNameExpected(int inlineMammalId, string expected)
        {
            // Arrange
            var contextMock = new Mock<DBContext>();
            contextMock.Setup(m => m.Mammals).ReturnsDbSet(GetTestMammals());
            var logger = Mock.Of<ILogger<MammalRepository>>();

            var mammalRepository = new MammalRepository(contextMock.Object, logger);

            // Act
            var result = mammalRepository.GetMammalById(inlineMammalId);

            // Assert
            Assert.Equal(expected, result.Result.Name);
        }

        private List<Mammal> GetTestMammals()
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
