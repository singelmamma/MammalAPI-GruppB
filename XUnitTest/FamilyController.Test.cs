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
    public class FamilyControllerTest
    {
        [Fact]
        public async Task GetReturnsFamilyById_ExpectCorrectIdAsync()
        {
            //Arrange
            var responseObject = GetItem();

            var mockRepository = new Mock<IFamilyRepository>();
            mockRepository.Setup(x => x.GetFamilyById(1))
                .Returns(responseObject);

            var controller = new FamilyController(mockRepository.Object);

            //Act          
            Task<IActionResult> actionResult = controller.GetFamilyById(1);
            var contentResult = await actionResult as OkObjectResult;
            var expectedObject = new IdNameDTO
            {
                Id = 1,
                Name = "Phocidae"
            };

            //Assert
            Assert.NotNull(contentResult);
            Assert.Equal(200, contentResult.StatusCode);

            IdNameDTO actualObject = contentResult.Value as IdNameDTO;
            Assert.Equal(expectedObject.Id, actualObject.Id);
            Assert.Equal(expectedObject.Name, actualObject.Name);
        }

        private async Task<IdNameDTO> GetItem()
        {
            var itemToReturn = new IdNameDTO
            {
                Id = 1,
                Name = "Phocidae"
            };

            return itemToReturn;
        }
    }
}
