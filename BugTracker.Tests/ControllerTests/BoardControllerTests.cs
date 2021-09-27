using Bug_Tracker.Controllers;
using Bug_Tracker.Interfaces;
using Bug_Tracker.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BugTracker.Tests
{
    public class BoardControllerTests
    {
        private readonly Mock<IBoardRepository> repositoryStub = new Mock<IBoardRepository>();

        private readonly Random rand = new Random();
        [Fact]
        public async Task GetBoard_WithUnexistingItem_ShouldReturnNotFound()
        {
            // Arrange Any mocks variables and inputs
            repositoryStub.Setup(repo => repo.GetBoard(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((UserBoard)null);

            //var controllerstub = new Mock<BoardController>();

            var controller = new BoardController(repositoryStub.Object);
            // Act Execute the test and perform action we are testing
            var result = await controller.GetBoard(It.IsAny<int>(), (It.IsAny<int>()));

            // Assert Verfiy whatever we are testing
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetBoard_WithExistingItem_ReturnsExpectedBoard()
        {
            // Arrange
            var expectedItem = CreateRandomUserBoard();

            repositoryStub.Setup(repo => repo.GetBoard(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(expectedItem);

            var controller = new BoardController(repositoryStub.Object);
            // Act

            var actionResult = await controller.GetBoard(It.IsAny<int>(), It.IsAny<int>());
            var okResult = actionResult as OkObjectResult;
            var actualUserBoard = okResult.Value as UserBoard;
            // Assert

            actualUserBoard.Should().BeEquivalentTo(
                expectedItem,
                options => options.ComparingByMembers<UserBoard>());

        }

        [Fact]
        public async Task GetAllBoards_WithExistingItems_ReturnsAllBoards()
        {
            // Arrange
            var expectedBoards = new[] { CreateRandomUserBoard(), CreateRandomUserBoard(), CreateRandomUserBoard() };

            repositoryStub.Setup(repo => repo.GetAllBoards(It.IsAny<int>()))
                .ReturnsAsync(expectedBoards);

            var controller = new BoardController(repositoryStub.Object);

            // Act

            var actionResult = await controller.GetAllBoards(It.IsAny<int>());
            var okResult = actionResult as ObjectResult;
            var actualUserBoards = okResult.Value;

            // Assert
            actualUserBoards.Should().BeEquivalentTo(
                expectedBoards,
                options => options.ComparingByMembers<UserBoard>());
        }

        [Fact]
        public async Task AddNewBoard_WithBoardToCreate_ReturnsCreatedItem()
        {
            // Arrange
            Board boardToCreate = new()
            {
                Title = "Test Board",
                Description = "This is a test",

            };

            var controller = new BoardController(repositoryStub.Object);

            // Act

            var actionResult = await controller.AddNewBoard(boardToCreate);
            var okResult = actionResult as OkObjectResult;
            // Assert
            okResult.StatusCode.Should().Be(200);

        }

        private UserBoard CreateRandomUserBoard()
        {
            return new()
            {
                UserId = It.IsAny<int>(),
                BoardId = It.IsAny<int>(),
                InviteAccepted = true,
                RolesId = rand.Next(4),

            };
        }
            
    }
}
