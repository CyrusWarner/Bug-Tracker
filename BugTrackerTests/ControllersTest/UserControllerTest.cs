using Bug_Tracker.Controllers;
using Bug_Tracker.Data;
using Bug_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BugTrackerTests
{
    [TestClass]
    public class UserControllerTest
    {
        private ApplicationDbContext _context;

        [TestInitialize]
        public void TestInitialize()
        {
        }




        [TestMethod]
        public void TestMethod1()
        {
            //IActionResult returnStatus;

            var options = new DbContextOptionsBuilder().UseInMemoryDatabase(databaseName: "BugTracker").Options;
            //var dbSet = new DbSet<User>();
            using (var context = new ApplicationDbContext(options))
            {
                context.Users.Add(new User { UserId = 1, FirstName = "Cyrus", LastName = "Warner", Email = "cyruswarner24@gmail.com", Password = "wasd" });
                context.SaveChanges();
            }

            // Arrange
            using (var context = new ApplicationDbContext(options))
            {
                UserController userController = new UserController(context);
                IActionResult returnStatus =  userController.Post(new User());

            }
            // Act

            // Assert
        }

    }
}
