using Xunit;
using MammalAPI.Models;
using MammalAPI.Controllers;
using MammalAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;

namespace XUnitTest
{
    public class MammalControllerTest
    {
        [Fact]
        public async Task GetMamalById_ReturnsMammalWithIdOne()
        {
            // Arrange
            var responseObject = GetTestMammals();

            var mockRepository = new Mock<IMammalRepository>();
            mockRepository.Setup(m => m.GetMammalById(1))
                .Returns(GetTestMammals());

            var controller = new MammalController(mockRepository.Object);

            // Act
            Task<IActionResult> result = controller.GetMammalById(1);
            var contentResult = await result as OkObjectResult;
            var expected = new Mammal 
            {
                MammalId = 1,
                Name = "Test Mammal One",
                LatinName = "Bapa latin apa"
            };

            // Assert
            Assert.NotNull(result);
            
            Mammal actual = contentResult.Value as Mammal;
            Assert.Equal(expected.MammalId, actual.MammalId);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.LatinName, actual.LatinName);
        }

        private async Task<Mammal> GetTestMammals()
        {
            var output = new Mammal
            {
                MammalId = 1,
                Name = "Test Mammal One",
                LatinName = "Bapa latin apa"
            };

            return output;
        }
    }
}
