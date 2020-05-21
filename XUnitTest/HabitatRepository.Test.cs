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

        [Fact]
        public void GetHabitatByName_FourDifferentHabitats_NameIsEast()
        {
            // Arrange
            var habitatName = "East";
            var habitats = GetMoqHabitats();
            var contextMock = new Mock<DBContext>();
            contextMock.Setup(h => h.Habitats).ReturnsDbSet(habitats);
            var logger = Mock.Of<ILogger<HabitatRepository>>();
            
            var habitatRepository = new HabitatRepository(contextMock.Object, logger);

            // Act
            var testResult = habitatRepository.GetHabitatByName(habitatName);

            // Assert
            Assert.Equal(habitatName, testResult.Result.Name);
        }

        [Fact]
        public void GetHabitatByName_MisspelledHabitatName_Exception()
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
        
        public void GetHabitatById_FourTestHabitats_GetIdOne()
        {
            // Arrange
            var contextMock = new Mock<DBContext>();
            contextMock.Setup(h => h.Habitats).ReturnsDbSet(GetMoqHabitats());
            var logger = Mock.Of<ILogger<HabitatRepository>>();

            var habitatRepository = new HabitatRepository(contextMock.Object, logger);

            // Act
            var expected = habitatRepository.GetHabitatById(1);

            // Assert
            Assert.Equal(1, expected.Result.Id);

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
