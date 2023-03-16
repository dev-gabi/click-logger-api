using Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Dal
{
    public class AppContext :DbContext
    {

        public AppContext(DbContextOptions<AppContext> options): base(options) { }

        public DbSet<User> Users;
        public DbSet<LoginUserStats> LoginUserStats;
        public DbSet<LoginPageStats> LoginPageStats;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("Users");
           modelBuilder.Entity<LoginUserStats>().ToTable("LoginUserStats");
           modelBuilder.Entity<LoginPageStats>().ToTable("LoginPageStats");
           modelBuilder.Entity<UserStats_User>().ToView("UserStats_User").HasNoKey();
           

        }

    }
}
