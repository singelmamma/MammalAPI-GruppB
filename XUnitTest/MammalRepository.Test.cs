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

        [Fact]
        public void GetAllMammals_TwoTestMammals_EqualListsOfMammals()
        {
            // Arrange
            var contextMock = new Mock<IMammalRepository>();
            contextMock.Setup(m => m.GetAllMammals()).Returns(GetTestAllMammals());
            var logger = Mock.Of<ILogger<MammalRepository>>();

            var mammalRepository = new MammalRepository(contextMock.Object, logger);

            // Act
            var expected = mammalRepository.GetAllMammals();

            // Assert
           // Assert.Equal(2, expected.Result.MammalId);
        }

        private async Task<List<MammalDTO>> GetTestAllMammals()
        {
            List<MammalsDTO> items = new List<MammalsDTO>();
            items.Add(new MammalsDTO
            {
                MammalId = 1,
                Name = "Grey Whale",
                LatinName = "Latin name1",
                Length = 250,
                Weight = 30000
            });
            items.Add(new MammalsDTO
            {
                MammalId = 2,
                Name = "Pink Whale",
                LatinName = "Latin name2",
                Length = 750,
                Weight = 800000
            });

            return Task.Run(() => items);

           // return items;
        }
    }
}
