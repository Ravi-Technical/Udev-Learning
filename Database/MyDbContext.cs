using Microsoft.EntityFrameworkCore;
using System;
using Udemy_Backend.Models;
using Udemy_Backend.Models.Admin;
using Udemy_Backend.Models.AdminModel;
using Udemy_Backend.Models.UI;

namespace Udemy_Backend.Database
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<OtpVerification> OtpVerifications { get; set; }
        public DbSet<AddCourseModel> Courses { get; set; }
        public DbSet<CourseCategories> Categories {get;set;}
        public DbSet<HomeSlider_Domain_Model> HomeSlider { get; set; }
        public DbSet<UISearch> UISearches { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>(e =>
            {
                e.HasKey(x=>x.Id);
                e.Property(x => x.Name).IsRequired().HasMaxLength(100);
                e.Property(x=>x.CountryCode).IsRequired().HasMaxLength(10);
                e.HasIndex(x=>x.CountryCode).IsUnique();
            });
            modelBuilder.Entity<State>(s =>
            {
               s.HasKey(x=>x.Id);
               s.Property(x=>x.StateName).IsRequired().HasMaxLength(100);
               s.Property(x=>x.StateCode).IsRequired().HasMaxLength(10);
               s.HasIndex(x=>x.StateCode).IsUnique();
               s.HasOne(x=>x.Country).WithMany(s=>s.State).HasForeignKey(x=>x.CountryId).OnDelete(DeleteBehavior.Cascade);
            });
        }

    }
}
