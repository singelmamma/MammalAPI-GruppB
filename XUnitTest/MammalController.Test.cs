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
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;

namespace XUnitTest
{
    public class MammalControllerTest
    {
        [Fact]
        public async void TestPost_AddMammal_Fails()
        {
            //var server = new TestServer(new WebHostBuilder()
            //    .UseStartup<Startup>());
            //var client = server.CreateClient();

            //IList<Mammal> mammals = new List<Mammal>();
            //var mammalContextMock = new Mock<DBContext>();
            //mammalContextMock.Setup(m => m.Mammals).ReturnsDbSet(mammals);

            //var logger = Mock.Of<ILogger<MammalRepository>>();
            //var mammalRepository = new MammalRepository(mammalContextMock.Object, logger);

            //var content = new StringContent("{Name: Willie, Length: 20.0, Weight: 100000, LatinName: Biggus Willus, Lifespan: 47, Family: null, MammalHabitats: null}");
            //HttpResponseMessage response = await client.PostAsync("http://localhost:5000/api/v1.0/mammal", content);
            //Assert.False(false);
        }
    }
}
