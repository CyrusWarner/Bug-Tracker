using Bug_Tracker.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug_Tracker.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet <Board> Boards { get; set; }
        public DbSet <Issues> Issues { get; set; }
        public DbSet<Notes> Notes { get; set; }

        public DbSet <User> Users { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
