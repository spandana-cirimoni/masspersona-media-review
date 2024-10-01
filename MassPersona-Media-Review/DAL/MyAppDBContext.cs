using System;
using System.ComponentModel.DataAnnotations;
using MassPersonaMediaReview.Models;
using Microsoft.EntityFrameworkCore;

namespace MassPersonaMediaReview.DAL
{
    public class MyAppDBContext : DbContext{
        public MyAppDBContext(DbContextOptions<MyAppDBContext> options) : base(options) {
        }

        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>()
                .Property(r => r.Category)
                .HasConversion<string>();
        }
    }
}