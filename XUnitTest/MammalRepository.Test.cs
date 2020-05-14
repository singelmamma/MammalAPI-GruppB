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

namespace XUnitTest
{
    public class MammalRepositoryTest
    {

        [Fact]
        public void GetMammalById_TwoTestMammals_GetIdTwo()
        {
            // Arrange
            var contextMock = new Mock<DBContext>();
            contextMock.Setup(m => m.Mammals).ReturnsDbSet(GetTestMammals());
            var logger = Mock.Of<ILogger<MammalRepository>>();

            var mammalRepository = new MammalRepository(contextMock.Object, logger);

            // Act
            var expected = mammalRepository.GetMammalById(2);

            // Assert
            Assert.Equal(2, expected.Result.MammalId);
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
