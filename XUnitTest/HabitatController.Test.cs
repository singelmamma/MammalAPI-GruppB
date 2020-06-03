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

//        [Fact]
//        public async void GetHabitatById_ShouldReturnOneHabitat()
//        {
//            //arrange
//            IList<Habitat> habitat = GenerateHabitet();
//            var habitatMockContext = new Mock<DBContext>();
//            habitatMockContext.Setup(h => h.Habitats).ReturnsDbSet(habitat);
//            var logger = Mock.Of<ILogger<HabitatRepository>>();
//            var habitatRepo = new HabitatRepository(habitatMockContext.Object, logger);

//            //act
//            var actual = await habitatRepo.GetHabitatById(1);

//            //assert
//            Assert.Equal(1, actual.Id);


//        }

//        [Fact]
//        public async void GetAllHabitat_ShouldReturnAllHabitat()
//        {
//            //arrange
//            IList<Habitat> habitat = GenerateHabitet();
//            var habitatMockContext = new Mock<DBContext>();
//            habitatMockContext.Setup(h => h.Habitats).ReturnsDbSet(habitat);

//            var logger = Mock.Of < ILogger< HabitatRepository>>();
//            var habitatRepo = new HabitatRepository(habitatMockContext.Object, logger);

//            //act
//            var actual = await habitatRepo.GetAllHabitats();

//            //assert
//            Assert.True(actual.Count > 0);

//        }
//        public static IList<Habitat> GenerateHabitet()
//        {
//            return new List<Habitat>
//            {
//                new Habitat
//                {
//                    HabitatID=1,
//                    Name="Pacific Ocean",
//                }

//            };

//        }
//    }
//}
