using Booking.API.Controllers;
using Booking.API.Entities;
using Booking.API.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Booking.UnitTests
{
    public class BookingsControllerTests
    {
        [Fact]
        public async Task GetEvents_WithNonExistingItem_ReturnNotFoundAsync()
        {
            // Arrange
            var repositoryStub = new Mock<IEventRepository>();
            repositoryStub.Setup(repo => repo.GetEvents(It.IsAny<string>())).ReturnsAsync((Event)null);
            var controller = new BookingsController(repositoryStub.Object);

            // Act - testing with invalid event Id
            var result = await controller.GetEventById("testId5466");

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task DeleteEvent_WithValidItem_ReturnSuccess()
        {
            // Arrange
            var item = CreateEvent();
            var repositoryStub = new Mock<IEventRepository>();
            repositoryStub.Setup(repo => repo.DeleteEvent(It.IsAny<string>())).ReturnsAsync(true);
            var controller = new BookingsController(repositoryStub.Object);

            // Act 
            var result = await controller.DeleteEventById("123456");
            var OkObjectResult = (OkObjectResult)result;
            var dto = (bool)OkObjectResult.Value;

            // Assert
            dto.Should().BeTrue();
        }

        [Fact]
        public async Task GetEvents_WithExistingItem_ReturResultAsync()
        {
            var expectedEvent = CreateEvent();
            var repositoryStub = new Mock<IEventRepository>();
            repositoryStub.Setup(repo => repo.GetEvents(It.IsAny<string>())).ReturnsAsync(expectedEvent);
            var controller = new BookingsController(repositoryStub.Object);

            // testing with invalid event Id
            var result = await controller.GetEventById("123456");
            var OkObjectResult = (OkObjectResult)result.Result;
            var dto = OkObjectResult.Value;

            dto.Should().BeEquivalentTo(expectedEvent);
        }

        // TDD

        [Fact]
        public async Task GetEventsByCategory_WithValidItem_ReturnSuccess()
        {
            // Arrange
            var repositoryStub = new Mock<IEventRepository>();
            var items = CreateEvents().FindAll(x => x.Category == "Music");
            repositoryStub.Setup(repo => repo.GetEventsByCategory(It.IsAny<string>())).ReturnsAsync(items);
            var controller = new BookingsController(repositoryStub.Object);

            // Act 
            var result = await controller.GetEventsByCategory("Music");
            var OkObjectResult = (OkObjectResult)result.Result;
            var dtos = OkObjectResult.Value;

            // Assert -  There should be 2 records under the music category.
            dtos.Should().BeEquivalentTo(items);
        }

        #region Helpers

        private Event CreateEvent()
        {
            var eventRecords = new Event
            {
                Id = "123456",
                EventName = "Rahman Show",
                Category = "Music",
                Price = 5000,
                Seats = 4,
                Summary = "Music festival"
            };
            return eventRecords;
        }

        private List<Event> CreateEvents()
        {
            var eventRecords = new List<Event>()
            {
                new Event()
                {
                    Id = "123451",
                    EventName = "Juraasic World",
                    Category = "Movies",
                    Price = 200,
                    Seats = 2,
                    Summary = "action movie"
                },
                new Event()
                {
                    Id = "123457",
                    EventName = "Tomorrowland",
                    Category = "Music",
                    Price = 15000,
                    Seats = 10,
                    Summary = "Music festival"
                },
                new Event()
                {
                    Id = "123458",
                    EventName = "Leh Ladakh ride",
                    Category = "Driving",
                    Price = 27000,
                    Seats = 2,
                    Summary = "leh ladakh ride"
                },
                new Event()
                {
                    Id = "123459",
                    EventName = "Shayded",
                    Category = "Music",
                    Price = 2000,
                    Seats = 1,
                    Summary = "Music festival"
                }
            };
            return eventRecords;
        }

        #endregion
    }
}