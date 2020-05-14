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
    public class FamilyRepositoryTest
    {
        [Fact]
        public void GetFamilyByName_OneTestFamily_ExpectedNotNull()
        {
            // Arrange
            var contextMock = new Mock<DBContext>();
            contextMock.Setup(f => f.Families).ReturnsDbSet(GetTestFamilies());
            var logger = Mock.Of<ILogger<FamilyRepository>>();
            var familyRepository = new FamilyRepository(contextMock.Object, logger);
            var name = "Test Family One";
            var result = GetTestFamilies()[0];


            // Act
            var expected = familyRepository.GetFamilyByName(name);

            // Assert
            Assert.NotNull(expected.Result);
            Assert.Equal(result.Name, expected.Result.Name);
        }


        [Fact]
        public void GetFamilyById()
        {
            // Arrange
            var contextMock = new Mock<DBContext>();
            contextMock.Setup(f => f.Families).ReturnsDbSet(GetTestFamilies());
            var logger = Mock.Of<ILogger<FamilyRepository>>();
            var familyRepository = new FamilyRepository(contextMock.Object, logger);
            var id = 2;

            var result = GetTestFamilies()[1];


            // Act
            var expected = familyRepository.GetFamilyById(id);

            // Assert
            Assert.NotNull(expected.Result);
            Assert.Equal(result.Name, expected.Result.Name);
        }

        //public void GetAllFamilies()
        //{

        //}

        private List<Family> GetTestFamilies()
        {
            var sessions = new List<Family>();
            sessions.Add(new Family()
            {
                FamilyId = 1,
                Name = "Test Family One",

            });
            sessions.Add(new Family()
            {
                FamilyId = 2,
                Name = "Test Family Two",
            });
            return sessions;
        }
    }
}
