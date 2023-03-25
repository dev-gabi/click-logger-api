using Entities;
using Entities.DbEntities;
using Entities.UiEntities;
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
        public DbSet<LoginUserStatsWithUserName> LoginUserStatsWithUserName;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<LoginUserStats>().ToTable("LoginUserStats");                                                                                      //see comment below
            modelBuilder.Entity<LoginUserStatsWithUserName>().ToFunction("SelectLoginUserStatsByUserNameFunction");     //see comment below
            modelBuilder.Entity<LoginPageStats>().ToTable("LoginPageStats");
            modelBuilder.Entity<UserStats_User>().ToView("UserStats_User").HasNoKey();
            modelBuilder.Entity<PageStatsWithUserName>().ToView("SelectPageStatsWithUserName");
        }
        //comment
        //if LoginUserStatsWithUserName is derived from LoginUserStats 
        //an exception will be thrown when try to use SelectLoginUserStatsByUserName procedure
        //derived entity can't directly be mapped to a function, tha't is why both LoginUserStats and LoginUserStatsWithUserName implements IUserStats instead of inheritance
    }
}
