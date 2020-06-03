using System;
using Xunit;
using System.Collections.Generic;
using MammalAPI.Models;
using MammalAPI.Controllers;
using MammalAPI.Services;
using Microsoft.AspNetCore.Mvc;
using MammalAPI.DTO;
using System.Threading.Tasks;
using Moq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Web;
using MammalAPI.Context;
using Microsoft.Extensions.Logging;
using Moq.EntityFrameworkCore;

namespace XUnitTest
{
    public class FamilyControllerTest
    {
        [Fact]
        public async void PostFamilyTest()
        {
            // Arrange
            var profile = new MammalAPI.Configuration.Mapper();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            IMapper mapper = new Mapper(configuration);

                //Mock Repo
            var familyRepoMock = new Mock<IFamilyRepository>();
            familyRepoMock.Setup(r => r.Add<Family>(It.IsAny<Family>()));
            familyRepoMock.Setup(r => r.GetAllFamilies(It.IsAny<Boolean>())).Returns(Task.FromResult(new Family[1]));
            familyRepoMock.Setup(r => r.Save()).Returns(Task.FromResult(true));

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
            var controller = new FamilyController(familyRepoMock.Object, mapper, mockDescriptorProvider.Object);
            
                //Create new DTO
            var dto = new FamilyDTO
            {
                Name = "test",
                FamilyID = 1
            };


            // Act
            var result = await controller.PostFamily(dto);

            // Assert
            var r = result.Result as CreatedResult;
            var dtoResult = (FamilyDTO)r.Value;
            Assert.Equal("test", dtoResult.Name);
        }


        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public async void GetFamilyById_FetchFamilyBasedOnId_SameIdAsInputExpected(int inlineFamilyID, int expected)
        {
            // Arrange
            var profile = new MammalAPI.Configuration.Mapper();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            IMapper mapper = new Mapper(configuration);

            //Mock context
            var testFamilies = GetTestFamilies();
            var contextMock = new Mock<DBContext>();
            contextMock.Setup(f => f.Families).ReturnsDbSet(testFamilies);

            //Mock Repo
            var logger = Mock.Of<ILogger<FamilyRepository>>();
            var familyRepoMock = new FamilyRepository(contextMock.Object, logger);

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
            var controller = new FamilyController(familyRepoMock, mapper, mockDescriptorProvider.Object);
            
            //Act
            var result = await controller.GetFamilyById(inlineFamilyID, false);
            var contentResult = result as OkObjectResult;
            FamilyDTO dto = (FamilyDTO) contentResult.Value;

            //Assert
            Assert.Equal(expected, dto.FamilyID);
        }

        [Theory]
        [InlineData("Test Family One", "Test Family One")]
        [InlineData("Test Family Two", "Test Family Two")]
        public async void GetFamilyByName_FetchFamilyBasedOnName_SameNameAsInputExpected(string inlineFamilyName, string expected)
        {
            // Arrange
            var profile = new MammalAPI.Configuration.Mapper();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            IMapper mapper = new Mapper(configuration);

            //Mock context
            var testFamilies = GetTestFamilies();
            var contextMock = new Mock<DBContext>();
            contextMock.Setup(f => f.Families).ReturnsDbSet(testFamilies);

            //Mock Repo
            var logger = Mock.Of<ILogger<FamilyRepository>>();
            var familyRepoMock = new FamilyRepository(contextMock.Object, logger);

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
            var controller = new FamilyController(familyRepoMock, mapper, mockDescriptorProvider.Object);

            //Act
            var result = await controller.GetFamilyByName(inlineFamilyName, false);
            var contentResult = result as OkObjectResult;
            FamilyDTO dto = (FamilyDTO)contentResult.Value;

            //Assert
            Assert.Equal(expected, dto.Name);
        }

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
