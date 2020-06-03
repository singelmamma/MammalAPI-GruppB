using System;
using Xunit;
using System.Collections.Generic;
using MammalAPI.Models;
using MammalAPI.Controllers;
using MammalAPI.Services;
using Microsoft.AspNetCore.Mvc;
using MammalAPI.Context;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MammalAPI.DTO;

namespace XUnitTest
{
    public class HabitatControllerTest
    {
        [Fact]
        public async void GetAllHabitats_GetArrayOfAllHabitats()
        {
            // Arrange
            var profile = new MammalAPI.Configuration.Mapper();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            IMapper mapper = new Mapper(configuration);

            //Mock context
            var testHabitats = GetTestHabitats();
            var contextMock = new Mock<DBContext>();
            contextMock.Setup(h => h.Habitats).ReturnsDbSet(testHabitats);

            //Mock Repo
            var logger = Mock.Of<ILogger<HabitatRepository>>();
            var habitatRepoMock = new HabitatRepository(contextMock.Object, logger);

            //Mock IActionDescriptorCollectionProvider
            var actions = new List<ActionDescriptor>
            {
                new ActionDescriptor
                {
                    AttributeRouteInfo = new AttributeRouteInfo()
                    {
                        Template = "/test",
                    },
                    RouteValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                    {
                        { "action", "Test" },
                        { "controller", "Test" },
                    },
                }
            };
            var mockDescriptorProvider = new Mock<IActionDescriptorCollectionProvider>();
            mockDescriptorProvider.Setup(m => m.ActionDescriptors).Returns(new ActionDescriptorCollection(actions, 0));

            //Setup new controller based on mocks
            var controller = new HabitatController(habitatRepoMock, mapper, mockDescriptorProvider.Object);

            //Act
            var result = await controller.GetAllHabitats(false);
            var contentResult = result.Result as OkObjectResult;
            HabitatDTO[] dto = (HabitatDTO[])contentResult.Value;

            //Assert
            Assert.Equal(2, dto.Length);
        }

        [Theory]
        [InlineData("Test Habitat One", "Test Habitat One")]
        [InlineData("Test Habitat Two", "Test Habitat Two")]
        public async void GetHabitatByName_FetchHabitatBasedOnName_SameNameAsInputExpected(string inlineHabitatName, string expected)
        {
            // Arrange
            var profile = new MammalAPI.Configuration.Mapper();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            IMapper mapper = new Mapper(configuration);

            //Mock context
            var testHabitats = GetTestHabitats();
            var contextMock = new Mock<DBContext>();
            contextMock.Setup(h => h.Habitats).ReturnsDbSet(testHabitats);

            //Mock Repo
            var logger = Mock.Of<ILogger<HabitatRepository>>();
            var habitatRepoMock = new HabitatRepository(contextMock.Object, logger);

            //Mock IActionDescriptorCollectionProvider
            var actions = new List<ActionDescriptor>
            {
                new ActionDescriptor
                {
                    AttributeRouteInfo = new AttributeRouteInfo()
                    {
                        Template = "/test",
                    },
                    RouteValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                    {
                        { "action", "Test" },
                        { "controller", "Test" },
                    },
                }
            };
            var mockDescriptorProvider = new Mock<IActionDescriptorCollectionProvider>();
            mockDescriptorProvider.Setup(m => m.ActionDescriptors).Returns(new ActionDescriptorCollection(actions, 0));

            //Setup new controller based on mocks
            var controller = new HabitatController(habitatRepoMock, mapper, mockDescriptorProvider.Object);

            //Act
            var result = await controller.GetHabitatByName(inlineHabitatName, false);
            var contentResult = result as OkObjectResult;
            HabitatDTO dto = (HabitatDTO)contentResult.Value;

            //Assert
            Assert.Equal(expected, dto.Name);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public async void GetHabitatById_FetchHabitatBasedOnId_SameIdAsInputExpected(int inlineHabitatID, int expected)
        {
            // Arrange
            var profile = new MammalAPI.Configuration.Mapper();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            IMapper mapper = new Mapper(configuration);

            //Mock context
            var testHabitats = GetTestHabitats();
            var contextMock = new Mock<DBContext>();
            contextMock.Setup(h => h.Habitats).ReturnsDbSet(testHabitats);

            //Mock Repo
            var logger = Mock.Of<ILogger<HabitatRepository>>();
            var habitatRepoMock = new HabitatRepository(contextMock.Object, logger);

            //Mock IActionDescriptorCollectionProvider
            var actions = new List<ActionDescriptor>
            {
                new ActionDescriptor
                {
                    AttributeRouteInfo = new AttributeRouteInfo()
                    {
                        Template = "/test",
                    },
                    RouteValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                    {
                        { "action", "Test" },
                        { "controller", "Test" },
                    },
                }
            };
            var mockDescriptorProvider = new Mock<IActionDescriptorCollectionProvider>();
            mockDescriptorProvider.Setup(m => m.ActionDescriptors).Returns(new ActionDescriptorCollection(actions, 0));

            //Setup new controller based on mocks
            var controller = new HabitatController(habitatRepoMock, mapper, mockDescriptorProvider.Object);

            //Act
            var result = await controller.GetHabitatById(inlineHabitatID, false);
            var contentResult = result.Result as OkObjectResult;
            HabitatDTO dto = (HabitatDTO) contentResult.Value;

            //Assert
            Assert.Equal(expected, dto.HabitatID);
        }

        private List<Habitat> GetTestHabitats()
        {
            var sessions = new List<Habitat>();
            sessions.Add(new Habitat()
            {
                HabitatID = 1,
                Name = "Test Habitat One",

            });
            sessions.Add(new Habitat()
            {
                HabitatID = 2,
                Name = "Test Habitat Two",
            });
            return sessions;
        }
    }
}
