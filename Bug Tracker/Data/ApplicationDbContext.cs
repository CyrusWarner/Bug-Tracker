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
        public DbSet <Notes> Notes { get; set; }
        public DbSet<Events> Events { get; set; }
        public DbSet <User> Users { get; set; }
        public DbSet <Roles> Roles { get; set; }
        public DbSet <UserBoard> UserBoard { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserBoard>()
                .HasKey(ur => new { ur.UserId, ur.BoardId });
            modelBuilder.Entity<UserBoard>()
                .HasOne(Ur => Ur.User)
                .WithMany(ur => ur.Boards)
                .HasForeignKey(ur => ur.UserId);
            modelBuilder.Entity<UserBoard>()
                .HasOne(ur => ur.Board)
                .WithMany(ur => ur.Users)
                .HasForeignKey(ur => ur.BoardId);
                
        }
    }
}
