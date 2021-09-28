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
    public class IssuesControllerTests
    {
        private readonly Mock<IIssuesRepository> repositoryStub = new Mock<IIssuesRepository>();

        private readonly Random rand = new Random();

        [Fact]
        public async Task AddNewIssue_WithisValidBeingTrue_ShouldReturnStatusCode200()
        {
            // Arrange
            Issues issueToCreate = CreateRandomIssue();

            repositoryStub.Setup(repo => repo.AddNewIssue(issueToCreate))
                .ReturnsAsync(true);

            var controller = new IssuesController(repositoryStub.Object);

            // Act
            var result = await controller.AddNewIssue(issueToCreate);

            //
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task AddNewIssue_WithisValidBeingFalse_ShouldReturnBadRequest()
        {
            // Arrange
            Issues issueToCreate = CreateRandomIssue();

            repositoryStub.Setup(repo => repo.AddNewIssue(issueToCreate))
                .ReturnsAsync(false);

            var controller = new IssuesController(repositoryStub.Object);

            // Act
            var result = await controller.AddNewIssue(issueToCreate);

            //
            result.Should().BeOfType<BadRequestResult>();
        }

        private Issues CreateRandomIssue()
        {
            return new()
            {
                Title = "Test Issue",
                Description = "This is a test",
                isCompleted = false,
                BoardId = rand.Next(1000),
                UserId = rand.Next(1000),
            };

        }
    }
}
