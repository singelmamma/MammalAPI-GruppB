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
            mammalRepo.Setup(r => r.GetAllMammals(It.IsAny<Boolean>(), It.IsAny<Boolean>())).Returns(Task.FromResult(mammals));
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

        [Fact]
        public async void GetHabitatByID_ShoulrReturn_anObjectAndAName()
        {

            // Arrange
            var profile = new MammalAPI.Configuration.Mapper();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            IMapper mapper = new Mapper(configuration);
            List<Mammal> mammals = new List<Mammal>();

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

        [Fact]
        public async void GetMammalByHabitatID_ShouldReturnMammal()
        {
            //Arrange
            var profile = new MammalAPI.Configuration.Mapper();
            var config = new MapperConfiguration(x => x.AddProfile(profile));
            IMapper mapper = new Mapper(config);


            //Mocking
            var mammal = GetTestMammals();
            var mockContext = new Mock<DBContext>();
            mockContext.Setup(z => z.Mammals).ReturnsDbSet(mammal);

            //Mocking repo
            var logger = Mock.Of<ILogger<MammalRepository>>();
            var repo = new MammalRepository(mockContext.Object, logger);


            //mocking IActionDescriptor
            var actions = new List<ActionDescriptor>();
            var mockDescriptor = new Mock<IActionDescriptorCollectionProvider>();
            mockDescriptor.Setup(x => x.ActionDescriptors).Returns(new ActionDescriptorCollection(actions, 0));

            // setting up controller
            var controller = new MammalsController(repo, mapper, mockDescriptor.Object);

            //Act
            var result = await controller.GetMammalsByHabitatId(1, false,false, true);
            var content = result.Result as OkObjectResult;
            var dto = (MammalDTO[])content.Value;

            //Assert
            Assert.Equal(2, dto.Length);

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
                Weight = 500,
                MammalHabitats = new List<MammalHabitat>
                {
                        new MammalHabitat
                        {
                            Habitat= new Habitat
                            {
                                HabitatID=1,
                                Name="pacific ocean",
                            }
                        }
                }

            });
            sessions.Add(new Mammal()
            {
                MammalId = 2,
                Name = "Test Mammal Two",
                LatinName = "Testus Testus",
                Length = 50,
                Lifespan = 200,
                Weight = 100,
                MammalHabitats = new List<MammalHabitat>
                {
                        new MammalHabitat
                        {
                            Habitat= new Habitat
                            {
                                HabitatID=1,
                                Name="pacific ocean",
                            }
                        }
                }
            });
            return sessions;
        }
    }
}

