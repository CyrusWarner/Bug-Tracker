﻿using Bug_Tracker.Controllers;
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
        public async Task GetBoardIssues_WithUnexistingIssues_ShouldReturnEmptyArray()
        {
            var expcetedIssues = new[] { CreateRandomIssue(), CreateRandomIssue(), CreateRandomIssue() };
            // Arrange Any mocks variables and inputs
            repositoryStub.Setup(repo => repo.GetBoardIssues(It.IsAny<int>()))
                .ReturnsAsync(expcetedIssues);

            var controller = new IssuesController(repositoryStub.Object);

            // Act Execute the test and perform action we are testing
            var result = await controller.GetBoardIssues(It.IsAny<int>());
            var okResult = result as ObjectResult;
            var actualBoardIssues = okResult.Value;

            // Assert Verfiy whatever we are testing
            actualBoardIssues.Should().BeEquivalentTo(
                expcetedIssues,
                options => options.ComparingByMembers<Issues>()
                );
        }


        [Fact]
        public async Task AddNewIssue_WithisValidBeingTrue_ShouldReturnOkResult()
        {
            // Arrange
            Issues issueToCreate = CreateRandomIssue();

            repositoryStub.Setup(repo => repo.AddNewIssue(issueToCreate))
                .ReturnsAsync(true);

            var controller = new IssuesController(repositoryStub.Object);

            // Act
            var result = await controller.AddNewIssue(issueToCreate);

            // Assert
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

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task UpdateIssue_WithisValidBeingTrue_ShouldReturnOkResult()
        {
            // Arrange
            Issues issueToUpdate = CreateRandomIssue();

            repositoryStub.Setup(repo => repo.UpdateIssue(It.IsAny<int>(), issueToUpdate))
                .ReturnsAsync(true);

            var controller = new IssuesController(repositoryStub.Object);

            // Act
            var result = await controller.UpdateIssue(It.IsAny<int>(), issueToUpdate);

            // Assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task UpdateIssue_WithisValidBeingFalse_ShouldReturnBadRequest()
        {
            // Arrange
            Issues issueToUpdate = CreateRandomIssue();

            repositoryStub.Setup(repo => repo.UpdateIssue(It.IsAny<int>(), issueToUpdate))
                .ReturnsAsync(false);

            var controller = new IssuesController(repositoryStub.Object);

            // Act
            var result = await controller.UpdateIssue(It.IsAny<int>(), issueToUpdate);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task DeleteIssue_WithisValidBeingTrue_ShouldReturnOkResult()
        {
            // Arrange

            repositoryStub.Setup(repo => repo.DeleteIssue(It.IsAny<int>()))
                .ReturnsAsync(true);

            var controller = new IssuesController(repositoryStub.Object);

            // Act
            var result = await controller.DeleteIssue(It.IsAny<int>());

            // Assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task DeleteIssue_WithisValidBeingFalse_ShouldReturnBadRequest()
        {
            // Arrange

            repositoryStub.Setup(repo => repo.DeleteIssue(It.IsAny<int>()))
                .ReturnsAsync(false);

            var controller = new IssuesController(repositoryStub.Object);

            // Act
            var result = await controller.DeleteIssue(It.IsAny<int>());

            // Assert
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
