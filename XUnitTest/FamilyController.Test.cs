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


        //Not working
        [Fact]
        public async void GetFamilyById()
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
            
            //Create new DTO and add it
            var dto = new FamilyDTO
            {
                Name = "test",
                FamilyID = 1
            };
            var bla = await controller.PostFamily(dto);

            // Act
            var result = await controller.GetFamilyById(1);

            //Assert
            //Assert.Equal(result, dtoResult.FamilyID);
        }
    }
}
