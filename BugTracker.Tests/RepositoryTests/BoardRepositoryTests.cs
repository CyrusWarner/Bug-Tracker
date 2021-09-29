using Bug_Tracker.Data;
using Bug_Tracker.Models;
using Bug_Tracker.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BugTracker.Tests.RepositoryTests
{
    public class BoardRepositoryTests
    {
        private readonly ApplicationDbContext context;

        private readonly Random rand = new Random();

        public BoardRepositoryTests()
        {
            DbContextOptionsBuilder dbOptions = new DbContextOptionsBuilder()
                .UseInMemoryDatabase(
                Guid.NewGuid().ToString()
                );

            context = new ApplicationDbContext(dbOptions.Options);

        }

        [Fact]
        public async Task GetBoard_WhenGivenABoardIdAndUserId_ShouldReturnAUserBoard()
        {
            // Arrange
            int userId = rand.Next(500);
            var board = new Board()
            {
                Title = "Test board for GetBoard method",
                Description = "This is a test board",
            };
            context.Boards.Add(board);
            await context.SaveChangesAsync();

            var UserBoard = new UserBoard()
            {
                UserId = userId,
                BoardId = board.BoardId,
                InviteAccepted = true,
                RolesId = 3,
            };
            context.UserBoard.Add(UserBoard);
            await context.SaveChangesAsync();

            var boardRepo = new BoardRepository(context);

            // Act
            UserBoard result = await boardRepo.GetBoard(board.BoardId, userId);

            // Assert
            result.Should().NotBeNull();
        }
        [Fact]
        public async Task GetAllBoards_WhenGivenAUserIdAndInviteAcceptedIsTrue_ShouldReturnAllOfAUsersBoards()
        {
            // Arrange
            int userId = rand.Next(500);
            var board = new Board()
            {
                Title = "Test board for GetAllBoards method",
                Description = "This is a test board",
            };
            context.Boards.Add(board);
            await context.SaveChangesAsync();

            var userBoards = new List<UserBoard>()
            {
                new() {UserId = userId, BoardId = board.BoardId,InviteAccepted = true, RolesId = 3 },
            };
            context.UserBoard.AddRange(userBoards);
            await context.SaveChangesAsync();

            var boardRepo = new BoardRepository(context);

            // Act
            var result = await boardRepo.GetAllBoards(userId);
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task AddNewBoard_WhenGivenAValidBoard_ShouldReturnTheNewBoard()
        {
            // Arrange
            var boardRepo = new BoardRepository(context);
            var board = new Board()
            {
                Title = "Test Board",
                Description = "This is a test board",
            };

            // Act
            Board result = await boardRepo.AddNewBoard(board);

            // Assert
            result.Should().BeEquivalentTo(
                board,
                options => options.ComparingByMembers<Board>());
        }

    }

}
