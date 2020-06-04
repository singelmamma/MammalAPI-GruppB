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
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MammalAPI.DTO;

namespace XUnitTest
{
    public class MammalControllerTest
    {

        [Fact]
        public async void PostMammal_Should_SaveOneMammal()
        {
            // Arrange
            var profile = new MammalAPI.Configuration.Mapper();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            IMapper mapper = new Mapper(configuration);
            List<Mammal> mammals = new List<Mammal>();

            //Mock Repo
            var mammalRepo = new Mock<IMammalRepository>();
            mammalRepo.Setup(r => r.Add<Mammal>(It.IsAny<Mammal>()));
            mammalRepo.Setup(r => r.GetAllMammals(It.IsAny<Boolean>(),It.IsAny<Boolean>())).Returns(Task.FromResult(mammals));
            mammalRepo.Setup(r => r.Save()).Returns(Task.FromResult(true));

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
                },
            };
            var mockDescriptorProvider = new Mock<IActionDescriptorCollectionProvider>();
            mockDescriptorProvider.Setup(m => m.ActionDescriptors).Returns(new ActionDescriptorCollection(actions, 0));

            //Setup new controller based on mocks
            var controller = new MammalsController(mammalRepo.Object, mapper, mockDescriptorProvider.Object);

            //Create new DTO
            var dto = new MammalDTO
            {
                Name = "test",
                MammalID = 1
            };


            // Act
            var result = await controller.PostMammal(dto);

            // Assert
            var r = result.Result as CreatedResult;
            var dtoResult = (MammalDTO)r.Value;
            Assert.Equal("test", dtoResult.Name);
        }

        [Theory]
        [InlineData("Test Mammal One", "Test Mammal One")]
        [InlineData("Test Mammal Two", "Test Mammal Two")]
        public async void GetHabitatByName_FetchMammalBasedOnNamer_SameNameAsInputExpected(string inlineMammalName, string expected)
        {
            // Arrange
            var profile = new MammalAPI.Configuration.Mapper();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            IMapper mapper = new Mapper(configuration);

            //Mock context
            var testMammals = GetTestMammals();
            var contextMock = new Mock<DBContext>();
            contextMock.Setup(m => m.Mammals).ReturnsDbSet(testMammals);

            //Mock Repo
            var logger = Mock.Of<ILogger<MammalRepository>>();
            var mammalRepoMock = new MammalRepository(contextMock.Object, logger);

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
            var controller = new MammalsController(mammalRepoMock, mapper, mockDescriptorProvider.Object);

            //Act
            var result = await controller.GetMammalByName(inlineMammalName, false);
            var contentResult = result as OkObjectResult;
            MammalDTO dto = (MammalDTO)contentResult.Value;

            //Assert
            Assert.Equal(expected, dto.Name);
        }

        private List<Mammal> GetTestMammals()
        {
            var sessions = new List<Mammal>();
            sessions.Add(new Mammal()
            {
                MammalId = 1,
                Name = "Test Mammal One",
                LatinName = "Testidae",
                Length = 100,
                Lifespan = 38,
                Weight = 500

            });
            sessions.Add(new Mammal()
            {
                MammalId = 2,
                Name = "Test Mammal Two",
                LatinName = "Testus Testus",
                Length = 50,
                Lifespan = 200,
                Weight = 100
            });
            return sessions;
        }
    }
}

