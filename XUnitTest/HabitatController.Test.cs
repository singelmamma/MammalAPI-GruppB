//using System;
//using Xunit;
//using System.Collections.Generic;
//using System.Linq;
//using MammalAPI;
//using MammalAPI.Models;
//using MammalAPI.Controllers;
//using MammalAPI.Services;
//using Microsoft.AspNetCore.Mvc;
//using MammalAPI.Context;
//using Microsoft.Extensions.Logging;
//using Moq;
//using Moq.EntityFrameworkCore;

//namespace XUnitTest
//{
//    public class HabitatControllerTest
//    {
//        [Fact]
//        public async void GetHabitatByName_ShouldReturnOneHabitat()
//        {
//            //arrange
//            IList<Habitat> habitat = GenerateHabitet();
//            var habitatContextMock = new Mock<DBContext>();
//            habitatContextMock.Setup(e => e.Habitats).ReturnsDbSet(habitat);

//            var logger = Mock.Of<ILogger<HabitatRepository>>();
//            var habitatRepo = new HabitatRepository(habitatContextMock.Object, logger);

//            //act
//            var actual = await habitatRepo.GetHabitatByName(habitat[0].Name);

//            //assert
//            Assert.Equal(habitat[0].Name, actual.Name);
//        }

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
