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
using Moq;
using System.Threading.Tasks;
using System.Net.Http;

namespace XUnitTest
{
    public class ControllerTest
    {
        [Fact]
        public void GetReturnsMammal()
        {
            
        }

        [Fact]
        public async Task Test1()
        {
            // Arrange
            var mockRepo = new Mock<IMammalRepository>();
            mockRepo.Setup(repo => repo.GetAllMammals())
                    .ReturnsAsync(GetTestSessions());
            var controller = new MammalController(mockRepo.Object);

            // Act
            var result = await controller.Get();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IList<Mammal>>(
                        viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }
        
        private List<Mammal> GetTestSessions()
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
