using AutoMapper;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusalaSoft.Transpotation.BusinessServices.Contract;
using MusalaSoft.Transpotation.Domain.Models;
using MusalaSoft.Transpotation.Web.Controllers;

namespace MusalaSoft.Transpotation.UnitTest.ControllerTest
{
    public class DroneControllerTest
    {
        [Fact]
        public void GetAvailableDronesTest()
        {
            // Arrange
            int count = 5;
            var fakeAvailableDrones = A.CollectionOfDummy<Drone>(count).AsEnumerable();
            var droneService = A.Fake<IDroneService>();
            var mapper = A.Fake<IMapper>();
            var logger = A.Fake<ILogger<DroneController>>();
            A.CallTo(() => droneService.GetAvailableDrones()).Returns(fakeAvailableDrones.ToList());
            var controller = new DroneController(droneService, mapper, logger);

            // Call
            var result = controller.GetAvailableDrones();

            // Assert
            Assert.IsType<JsonResult>(result);
            Assert.NotNull(result);
        }
    }
}
