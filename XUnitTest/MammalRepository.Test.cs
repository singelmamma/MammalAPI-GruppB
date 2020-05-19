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
using MammalAPI.DTO;
using System.Threading.Tasks;

namespace XUnitTest
{
    public class MammalRepositoryTest
    {

        [Fact]
        public void GetMammalById_TwoTestMammals_GetIdTwo()
        {
            // Arrange
            var contextMock = new Mock<DBContext>();
            contextMock.Setup(m => m.Mammals).ReturnsDbSet(GetTestMammals());
            var logger = Mock.Of<ILogger<MammalRepository>>();

            var mammalRepository = new MammalRepository(contextMock.Object, logger);

            // Act
            var expected = mammalRepository.GetMammalById(2);

            // Assert
            //Assert.Equal(2, expected.Result.MammalId);
        }


        private List<Mammal> GetTestMammals()
        {
            var sessions = new List<Mammal>();
            sessions.Add(new Mammal()
            {
                MammalId = 1,
                Name = "Test Mammal One",
                LatinName = "Bapa latin apa"
            });
            sessions.Add(new Mammal()
            {
                MammalId = 2,
                Name = "Test Mammal Two",
                LatinName = "Latin kanske med"
            });
            return sessions;
        }

        [Fact]
        public async void GetAllMammals_TwoTestMammals_EqualListsOfMammals()
        {
            // Arrange
            IList<Mammal> mammals = GenerateMammals();

            var contextMock = new Mock<DBContext>();
            contextMock.Setup(m => m.Mammals).ReturnsDbSet(mammals);

            var logger = Mock.Of<ILogger<MammalRepository>>();
            var mammalRepository = new MammalRepository(contextMock.Object, logger);

            // Act
            List<MammalsDTO> expectedDTO = GenerateMammalsDTO();
            var actualGetAll = await mammalRepository.GetAllMammals();

            // Assert
            Assert.NotNull(actualGetAll);          
            Assert.Equal(typeof(List<MammalsDTO>), actualGetAll.GetType());

            Assert.True(expectedDTO[0].MammalId.Equals(actualGetAll[0].MammalId));
            Assert.True(expectedDTO[0].Name.Equals(actualGetAll[0].Name));
            Assert.True(expectedDTO[0].LatinName.Equals(actualGetAll[0].LatinName));
            Assert.True(expectedDTO[0].Length.Equals(actualGetAll[0].Length));
            Assert.True(expectedDTO[0].Weight.Equals(actualGetAll[0].Weight));
        }

        private static IList<Mammal> GenerateMammals()
        {
            return new List<Mammal>
            {
                new Mammal
                {
                    MammalId = 1,
                    Name = "Grey Whale",
                    Length = 250,
                    Weight = 30000,
                    LatinName = "Latin name1",
                    Lifespan = 100,
                    Family = new Family
                    {
                        FamilyId = 1,
                        Name = "Hoppoloppo"
                    },
                    MammalHabitats = new List<MammalHabitat>()
                    {
                        new MammalHabitat
                        {
                            HabitatId = 1,
                            MammalId = 1
                        }
                    }                    
                }
            };
        }

        private static List<MammalsDTO> GenerateMammalsDTO()
        {
            List<MammalsDTO> items = new List<MammalsDTO>();
            items.Add(new MammalsDTO
            {
                MammalId = 1,
                Name = "Grey Whale",
                LatinName = "Latin name1",
                Length = 250,
                Weight = 30000
            });

            return items;
        }
    }
}
