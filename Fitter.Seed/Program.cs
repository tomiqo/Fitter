using System;
using Fitter.DAL;
using Fitter.DAL.Entity;
using Fitter.DAL.Enums;
using Microsoft.EntityFrameworkCore;
using Fitter.BL.Services;

namespace Fitter.Seed
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbContext = CreateDbContext())
            {
                ClearDatabase(dbContext);
                SeedData(dbContext);
            }
        }

        private static void SeedData(FitterDbContext dbContext)
        {
            var user = new User
            {
                FirstName = "Jon",
                LastName = "Snow",
                Email = "a",
                Password = "a",
            };
            var passwordHasher = new PasswordHasher(user.Password);
            user.Password = passwordHasher.GetHashedPassword();
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
        }

        private static void ClearDatabase(FitterDbContext dbContext)
        {
            dbContext.RemoveRange(dbContext.Attachments);
            dbContext.RemoveRange(dbContext.Comments);
            dbContext.RemoveRange(dbContext.Posts);
            dbContext.RemoveRange(dbContext.Teams);
            dbContext.RemoveRange(dbContext.Users);
            dbContext.RemoveRange(dbContext.UsersInTeams);
            dbContext.SaveChanges();
        }

        private static FitterDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FitterDbContext>();
            optionsBuilder.UseSqlServer(
                @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog = FitterDB;MultipleActiveResultSets = True;Integrated Security = True; ");
            return new FitterDbContext(optionsBuilder.Options);
        }
    }
}
