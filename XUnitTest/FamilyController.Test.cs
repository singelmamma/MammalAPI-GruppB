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
        /*https://docs.microsoft.com/en-us/aspnet/web-api/overview/testing-and-debugging/unit-testing-controllers-in-web-api#testing-actions-that-return-httpresponsemessage*/

        [Fact]
        public void GetReturnsFamilyById_ExpectCorrectReturn()
        {
            //Arrange
            var dtoItem = GetItem();

            var mockRepository = new Mock<IFamilyRepository>();
            mockRepository.Setup(x => x.GetFamilyById(1)).
                Returns(dtoItem);

            var controller = new FamilyController(mockRepository.Object);

            //Act
            var testResult = controller.GetFamilyById(1);

            //Assert
            Assert.NotNull(testResult);
            Assert.Equal(1, testResult.Id);
        }

        private async Task<IdNameDTO> GetItem()
        {
            var itemToBeReturned = new IdNameDTO { Id = 1, Name = "Phocidae" };

            return itemToBeReturned;
        }
    }
}
