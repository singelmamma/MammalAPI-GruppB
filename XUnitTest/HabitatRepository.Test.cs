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
    public class HabitatRepositoryTest
    {
        [Fact]
        public void GetAllHabitats_FourDifferentHabitats_4()
        {
            // Arrange
            var habitats = GetMoqHabitats();
            var contextMock = new Mock<DBContext>();
            contextMock.Setup(h => h.Habitats).ReturnsDbSet(habitats);
            var logger = Mock.Of<ILogger<HabitatRepository>>();

            var habitatRepository = new HabitatRepository(contextMock.Object, logger);

            // Act
            var testResult = habitatRepository.GetAllHabitats();

            // Assert
            Assert.Equal(4, testResult.Result.Count);
        }

        [Theory]
        [InlineData("North", "North")]
        [InlineData("East", "East")]
        [InlineData("West", "West")]
        [InlineData("South", "South")]
        public void GetHabitatByName_FourDifferentHabitats_HabitatNameExpected(string inlineHabitatName, string expected)
        {
            // Arrange
            var habitats = GetMoqHabitats();
            var contextMock = new Mock<DBContext>();
            contextMock.Setup(h => h.Habitats).ReturnsDbSet(habitats);
            var logger = Mock.Of<ILogger<HabitatRepository>>();

            var habitatRepository = new HabitatRepository(contextMock.Object, logger);

            // Act
            var testResult = habitatRepository.GetHabitatByName(inlineHabitatName);

            // Assert
            Assert.Equal(expected, testResult.Result.Name);
        }

        [Fact]
        public void GetHabitatByName_MisspelledHabitatName_ExceptionExpected()
        {
            // Arrange
            var habitatName = "Eastss";
            var habitats = GetMoqHabitats();
            var contextMock = new Mock<DBContext>();
            contextMock.Setup(h => h.Habitats).ReturnsDbSet(habitats);
            var logger = Mock.Of<ILogger<HabitatRepository>>();

            var habitatRepository = new HabitatRepository(contextMock.Object, logger);

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => habitatRepository.GetHabitatByName(habitatName));
        }

        [Theory]
        [InlineData(1, "North")]
        [InlineData(2, "East")]
        [InlineData(3, "South")]
        [InlineData(4, "West")]
        public void GetHabitatById_FourTestHabitats_HabitatIdExpected(int inlineHabitatId, string expected)
        {
            // Arrange
            var contextMock = new Mock<DBContext>();
            contextMock.Setup(h => h.Habitats).ReturnsDbSet(GetMoqHabitats());
            var logger = Mock.Of<ILogger<HabitatRepository>>();

            var habitatRepository = new HabitatRepository(contextMock.Object, logger);

            // Act
            var result = habitatRepository.GetHabitatById(inlineHabitatId);

            // Assert
            Assert.Equal(expected, result.Result.Name);

        }

        private IList<Habitat> GetMoqHabitats()
        {
            var habitats = new List<Habitat>();
            habitats.Add(new Habitat
            {
                HabitatID = 1,
                Name = "North",
                MammalHabitats = null
            });
            habitats.Add(new Habitat
            {
                HabitatID = 2,
                Name = "East",
                MammalHabitats = null
            });
            habitats.Add(new Habitat
            {
                HabitatID = 3,
                Name = "South",
                MammalHabitats = null
            });
            habitats.Add(new Habitat
            {
                HabitatID = 4,
                Name = "West",
                MammalHabitats = null
            });
            return habitats;
        }

    }
}
