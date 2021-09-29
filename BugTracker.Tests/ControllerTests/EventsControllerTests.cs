using Bug_Tracker.Controllers;
using Bug_Tracker.Interfaces;
using Bug_Tracker.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BugTracker.Tests.ControllerTests
{
    public class EventsControllerTests
    {
        private readonly Mock<IEventsRepository> repositoryStub = new Mock<IEventsRepository>();

        [Fact]
        public async Task GetBoardEvents_withEvents_returnsAllEvents()
        {

            // Arrange
            var expectedEvents = new[] { CreateRandomEvent(), CreateRandomEvent(), CreateRandomEvent() };
            repositoryStub.Setup(repo => repo.GetBoardEvents(It.IsAny<int>()))
                .ReturnsAsync(expectedEvents);

            var controller = new EventsController(repositoryStub.Object);

            // Act
            var actionResult = await controller.GetBoardEvents(It.IsAny<int>());
            var okResult = actionResult as ObjectResult;
            var actualBoardEvents = okResult.Value;

            // Assert
            actualBoardEvents.Should().BeEquivalentTo(
                expectedEvents,
                options => options.ComparingByMembers<Events>());
        }

        [Fact]
        public async Task AddNewEvent_withIsValidBeingTrue_ShouldReturnOkResult()
        {
            // Arrange
            var eventToAdd = CreateRandomEvent();
            repositoryStub.Setup(repo => repo.AddNewEvent(eventToAdd))
                .ReturnsAsync(true);

            // Act
            var controller = new EventsController(repositoryStub.Object);
            var result = await controller.AddNewEvent(eventToAdd);

            // Assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task AddNewEvent_withIsValidBeingFalse_ShouldReturnBadRequest()
        {
            // Arrange
            var eventToAdd = CreateRandomEvent();
            repositoryStub.Setup(repo => repo.AddNewEvent(eventToAdd))
                .ReturnsAsync(false);

            // Act
            var controller = new EventsController(repositoryStub.Object);
            var result = await controller.AddNewEvent(eventToAdd);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task DeleteEvent_withIsValidBeingTrue_ShouldReturnOkResult()
        {
            // Arrange
            repositoryStub.Setup(repo => repo.DeleteEvent(It.IsAny<int>()))
                .ReturnsAsync(true);

            // Act
            var controller = new EventsController(repositoryStub.Object);
            var result = await controller.DeleteEvent(It.IsAny<int>());

            // Assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task DeleteEvent_withIsValidBeingFalse_ShouldReturnBadRequest()
        {
            // Arrange
            repositoryStub.Setup(repo => repo.DeleteEvent(It.IsAny<int>()))
                .ReturnsAsync(false);

            // Act
            var controller = new EventsController(repositoryStub.Object);
            var result = await controller.DeleteEvent(It.IsAny<int>());

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        private Events CreateRandomEvent()
        {
            return new()
            {
                Title = "This is a test event",
                Date = "9/29/2021",
                Assignee = "testemail@gmail.com",
                BoardId = It.IsAny<int>(),
            };
        }
    }

    
}
