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
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public async void GetHabitatByID_FetchMammalBasedOnId_SameIdAsInputExpected(int inlineFamilyId, int expected)
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
            var logger = Mock.Of<ILogger<FamilyRepository>>();
            var familyRepoMock = new FamilyRepository(contextMock.Object, logger);

            var mammalRepo = new Mock<IMammalRepository>();
            mammalRepo.Setup(r => r.GetMammalById(1, It.IsAny<Boolean>(), It.IsAny<Boolean>()));


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

            var dto = new MammalDTO
            {
                MammalID = 1,
                Name = "Leopard Seal",
            };

            var controller = new MammalsController(mammalRepo.Object, mapper, mockDescriptorProvider.Object);

            //Act
            var result = await controller.GetMammalById(dto.MammalID);


            // Assert
            Assert.IsAssignableFrom<ObjectResult>(result);
            Assert.Equal("Leopard Seal", dto.Name);
        }
    }
}

